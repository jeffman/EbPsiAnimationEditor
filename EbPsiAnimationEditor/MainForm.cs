using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using EbPsiAnimationEditor.EB;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor
{
    public partial class MainForm : Form
    {
        AppConfig appConfig = null;

        EBRom rom = null;

        string romPath = null;
        string romConfigPath = null;

        bool changesMade = false;

        ControlState controlState;
        ControlState ControlState
        {
            get { return controlState; }
            set
            {
                controlState = value;
                EnableControls(value);
            }
        }

        List<Bitmap>[] thumbsCache = null;
        ImageList currentThumbs;
        Size thumbSize = new Size(64, 64);

        Progress<string> updateStatus;
        Progress<int> updateProgress;

        private int AnimationIndex { get { return animationList.SelectedIndex; } }
        private int FrameIndex
        {
            get
            {
                if (frameList.SelectedIndices.Count == 0) return -1;
                return frameList.SelectedIndices[0];
            }
        }

        public MainForm()
        {
            InitializeComponent();
            ListViewHelper.EnableDoubleBuffer(frameList);

            currentThumbs = new ImageList();
            currentThumbs.ImageSize = thumbSize;
            frameList.LargeImageList = currentThumbs;

            this.MouseWheel += new MouseEventHandler(MainForm_MouseWheel);

            ControlState = ControlState.Closed;
        }

        void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                    zoomIn_Click(null, null);

                else if (e.Delta < 0)
                    zoomOut_Click(null, null);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updateStatus = new Progress<string>(s => statusLabel.Text = s);
            updateProgress = new Progress<int>(i => progressBar.Value = i);

            // Look for the app config file
            string configPath = "settings.json";

            if (File.Exists(configPath))
            {
                bool configSuccess = false;

                try
                {
                    appConfig = AppConfig.FromFile(configPath);
                    configSuccess = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem loading the application settings. Reason: " + ex.Message,
                        "Error loading ROM config",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                if (!configSuccess || appConfig == null)
                {
                    DialogResult result = MessageBox.Show("There was an error loading the application settings. Overwriting with a default configuration.",
                        "Error loading config file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    // Create a default config and overwrite the file
                    appConfig = AppConfig.GenerateDefaults();
                    appConfig.Save(configPath);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("A settings file was not found. Creating a default one now.",
                    "Config file not found",
                    MessageBoxButtons.OK);

                // Create a default config
                appConfig = AppConfig.GenerateDefaults();
                appConfig.Save(configPath);
            }

            // Apply the settings
            gridMenu.Checked = appConfig.Grid;
            frameEditor.Zoom = appConfig.Zoom;
            frameEditor.GridColor = appConfig.GridColor;
            frameEditor.TileColor = appConfig.TileColor;
        }

        private string GetConfigPath(string romPath)
        {
            return Path.Combine(
                Path.GetDirectoryName(romPath),
                Path.GetFileNameWithoutExtension(romPath) + ".romconfig");
        }

        private void githubMenu_Click(object sender, EventArgs e)
        {
            Process.Start("http://github.com/jeffman");
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private bool LoadRom(string filePath)
        {
            // Get the ROM file
            byte[] romBytes = File.ReadAllBytes(filePath);

            string verifyReason;
            bool verify = EBRom.Verify(romBytes, out verifyReason);

            if (!verify)
            {
                MessageBox.Show("There was a problem loading the ROM file. Reason: " +
                    verifyReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // Get the config file
            RomConfig romConfig = null;
            var configPath = GetConfigPath(filePath);

            if (File.Exists(configPath))
            {
                bool configSuccess = false;

                try
                {
                    romConfig = RomConfig.FromFile(configPath);
                    configSuccess = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was a problem loading the ROM config. Reason: " + ex.Message,
                        "Error loading ROM config",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                if (!configSuccess)
                {
                    DialogResult result = MessageBox.Show("There was an error loading the ROM config file. Overwrite it with a default configuration?",
                        "Error loading config file",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel)
                        return false;

                    // Create a default config and overwrite the file
                    romConfig = RomConfig.GenerateDefaults();
                    romConfig.Save(configPath);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("A config file was not found for the ROM you selected. Would you like to create a default one now?",
                    "Config file not found",
                    MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                    return false;

                // Create a default config
                romConfig = RomConfig.GenerateDefaults();
                romConfig.Save(configPath);
            }

            // Read the ROM data
            rom = EBRom.FromArray(romBytes, romConfig);
            try
            {
                rom.ReadAll(updateStatus, updateProgress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The ROM could not be parsed." + Environment.NewLine +
                    "Reason: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            romPath = filePath;
            romConfigPath = configPath;
            return true;
        }

        private bool SaveRom(string filePath)
        {
            try
            {
                rom.WriteAll(updateStatus, updateProgress, appConfig.FastCompress, appConfig.Multithreaded);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the ROM. Reason: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            rom.Save(filePath);
            changesMade = false;
            return true;
        }

        private void CloseRom()
        {
            ControlState = ControlState.Closed;

            frameEditor.EndEdit();
            frameList.Clear();
            animationList.Items.Clear();

            rom = null;
            romPath = null;
            romConfigPath = null;
            changesMade = false;
            thumbsCache = null;
            currentThumbs.Images.Clear();

            GC.Collect();
        }

        private async Task<DialogResult> ClosingRom()
        {
            DialogResult result = DialogResult.No;

            if (changesMade)
            {
                result = MessageBox.Show("Your ROM has unsaved changes. Save before closing?",
                    "Unsaved changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel)
                    return result;

                if (result == DialogResult.Yes)
                {
                    ControlState = ControlState.Busy;

                    progressBar.Value = 0;
                    progressBar.Visible = true;
                    statusLabel.Text = "Saving ROM";

                    bool saveResult = await Task.Run<bool>(() => { return SaveRom(romPath); });

                    progressBar.Value = 0;
                    progressBar.Visible = false;
                    statusLabel.Text = "";

                    ControlState = ControlState.Open;

                    if (!saveResult)
                        return DialogResult.Cancel;
                }
            }

            CloseRom();
            return result;
        }

        private void EnableControls(ControlState controlState)
        {
            switch (controlState)
            {
                case ControlState.Closed:
                    openMenu.Enabled = 
                        settingsMenu.Enabled =
                        gridMenu.Enabled =
                        zoomIn.Enabled =
                        zoomOut.Enabled =
                        true;

                    saveMenu.Enabled =
                        closeMenu.Enabled =
                        importMenu.Enabled =
                        exportMenu.Enabled =
                        romConfigMenu.Enabled =
                        animationList.Enabled =
                        frameList.Enabled =
                        frameEditor.Enabled =
                        undoMenu.Enabled =
                        copyMenu.Enabled =
                        copyImageMenu.Enabled =
                        exportGifMenu.Enabled =
                        editTilesetsMenu.Enabled =
                        pasteMenu.Enabled =
                        editParametersButton.Enabled =
                        insertFrame.Enabled =
                        deleteFrame.Enabled =
                        moveFrameDown.Enabled =
                        moveFrameUp.Enabled =
                        false;

                    break;

                case ControlState.Open:
                    openMenu.Enabled =
                        saveMenu.Enabled =
                        closeMenu.Enabled =
                        importMenu.Enabled =
                        exportMenu.Enabled =
                        romConfigMenu.Enabled =
                        settingsMenu.Enabled =
                        animationList.Enabled =
                        frameList.Enabled =
                        frameEditor.Enabled =
                        undoMenu.Enabled =
                        copyMenu.Enabled =
                        copyImageMenu.Enabled =
                        exportGifMenu.Enabled =
                        editTilesetsMenu.Enabled =
                        pasteMenu.Enabled =
                        gridMenu.Enabled =
                        zoomIn.Enabled =
                        zoomOut.Enabled =
                        editParametersButton.Enabled =
                        insertFrame.Enabled =
                        deleteFrame.Enabled =
                        moveFrameDown.Enabled =
                        moveFrameUp.Enabled =
                        true;

                    break;

                case ControlState.Busy:
                    openMenu.Enabled =
                        saveMenu.Enabled =
                        closeMenu.Enabled =
                        importMenu.Enabled =
                        exportMenu.Enabled =
                        romConfigMenu.Enabled =
                        settingsMenu.Enabled =
                        animationList.Enabled =
                        frameList.Enabled =
                        frameEditor.Enabled =
                        undoMenu.Enabled =
                        copyMenu.Enabled =
                        copyImageMenu.Enabled =
                        exportGifMenu.Enabled =
                        editTilesetsMenu.Enabled =
                        pasteMenu.Enabled =
                        gridMenu.Enabled =
                        zoomIn.Enabled =
                        zoomOut.Enabled =
                        editParametersButton.Enabled =
                        insertFrame.Enabled =
                        deleteFrame.Enabled =
                        moveFrameDown.Enabled =
                        moveFrameUp.Enabled =
                        false;

                    break;

                default:
                    break;
            }

            frameEditor.RefreshCanvas();
        }

        private void LoadAnimationNames()
        {
            animationList.BeginUpdate();

            animationList.Items.Clear();
            animationList.Items.AddRange(rom.Config.AnimationNames.Select((n, i) =>
                String.Format("[{0:D2}] {1}", i, n)).ToArray());

            animationList.EndUpdate();
        }

        private void LoadAnimation(int animationIndex)
        {
            frameList.BeginUpdate();
            frameList.Clear();

            currentThumbs.Images.Clear();
            currentThumbs.Images.AddRange(thumbsCache[animationIndex].ToArray());

            Animation anim = rom.Animations[animationIndex];
            for (int i = 0; i < anim.Frames.Count; i++)
                frameList.Items.Add(i.ToString(), i);

            frameList.EndUpdate();
            frameList.Items[0].Selected = true;
        }

        private void UpdateAllThumbs(IProgress<int> progress)
        {
            thumbsCache = new List<Bitmap>[rom.Animations.Length];

            for (int animationIndex = 0; animationIndex < rom.Animations.Length; animationIndex++)
            {
                thumbsCache[animationIndex] = new List<Bitmap>();
                UpdateAnimationThumbs(animationIndex);

                if (progress != null)
                    progress.Report(animationIndex * 100 / (rom.Animations.Length - 1));
            }

            frameList.Invoke(() =>
            {
                if (AnimationIndex >= 0)
                {
                    frameList.BeginUpdate();
                    currentThumbs.Images.Clear();
                    currentThumbs.Images.AddRange(thumbsCache[AnimationIndex].ToArray());
                    frameList.EndUpdate();
                }
            });
        }

        private void UpdateAnimationThumbs(int animationIndex)
        {
            Animation anim = rom.Animations[animationIndex];
            thumbsCache[animationIndex].Clear();
            for (int i = 0; i < anim.Frames.Count; i++)
            {
                thumbsCache[animationIndex].Add(GenerateThumb(animationIndex, i));
            }

            frameList.Invoke(() =>
            {
                if (animationIndex == AnimationIndex)
                {
                    frameList.BeginUpdate();
                    currentThumbs.Images.Clear();
                    currentThumbs.Images.AddRange(thumbsCache[animationIndex].ToArray());
                    frameList.EndUpdate();
                }
            });
        }

        private void UpdateThumb(int animationIndex, int frameIndex)
        {
            Bitmap newThumb = GenerateThumb(animationIndex, frameIndex);
            thumbsCache[animationIndex][frameIndex] = newThumb;

            if (animationIndex == AnimationIndex)
            {
                frameList.BeginUpdate();
                currentThumbs.Images[frameIndex] = newThumb;
                frameList.EndUpdate();
            }
        }

        private Bitmap GenerateThumb(int animationIndex, int frameIndex)
        {
            Bitmap frame = rom.Animations[animationIndex].RenderFrame(frameIndex);
            Bitmap thumb = new Bitmap(thumbSize.Width, thumbSize.Height, frame.PixelFormat);

            using (Graphics g = Graphics.FromImage(thumb))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(frame, 0, 0, thumbSize.Width, thumbSize.Height);
            }

            return thumb;
        }

        private void SwapThumbs(int animationIndex, int a, int b)
        {
            Bitmap tempBitmap = thumbsCache[animationIndex][a];
            thumbsCache[animationIndex][a] = thumbsCache[animationIndex][b];
            thumbsCache[animationIndex][b] = tempBitmap;

            if (animationIndex == AnimationIndex)
            {
                frameList.BeginUpdate();

                Image tempImage = currentThumbs.Images[a];
                currentThumbs.Images[a] = currentThumbs.Images[b];
                currentThumbs.Images[b] = tempImage;

                frameList.EndUpdate();
            }
        }

        private void InsertThumb(Bitmap thumb, int animationIndex, int frameIndex)
        {
            thumbsCache[animationIndex].Insert(frameIndex, thumb);

            if (animationIndex == AnimationIndex)
            {
                // .NET's ImageList doesn't let you insert new images...
                frameList.BeginUpdate();

                // Add the new thumb to the end
                currentThumbs.Images.Add(thumb);

                // Bubble it down to frameIndex
                for (int i = currentThumbs.Images.Count - 1; i > frameIndex; i--)
                {
                    Image temp = currentThumbs.Images[i];
                    currentThumbs.Images[i] = currentThumbs.Images[i - 1];
                    currentThumbs.Images[i - 1] = temp;
                }

                frameList.EndUpdate();
            }
        }

        private void DeleteThumb(int animationIndex, int frameIndex)
        {
            thumbsCache[animationIndex].RemoveAt(frameIndex);

            if (animationIndex == AnimationIndex)
            {
                frameList.BeginUpdate();
                currentThumbs.Images.RemoveAt(frameIndex);
                frameList.EndUpdate();
            }
        }

        private async void openMenu_Click(object sender, EventArgs e)
        {
            if (openRom.ShowDialog() == DialogResult.OK)
            {
                ControlState prev = ControlState;
                ControlState = ControlState.Busy;
                
                progressBar.Visible = true;

                if (prev == ControlState.Open)
                    frameEditor.EndEdit();

                bool openResult = await Task.Run<bool>(() => { return LoadRom(openRom.FileName); });

                statusLabel.Text = "";

                if (openResult)
                {
                    statusLabel.Text = "Generating thumbnail cache";
                    await Task.Run(() => UpdateAllThumbs(updateProgress));
                    statusLabel.Text = "";

                    LoadAnimationNames();
                    animationList.SelectedIndex = 0;

                    ControlState = ControlState.Open;
                }
                else
                    ControlState = prev;

                progressBar.Visible = false;
            }
        }

        private async void saveMenu_Click(object sender, EventArgs e)
        {
            ControlState = ControlState.Busy;

            progressBar.Value = 0;
            progressBar.Visible = true;
            statusLabel.Text = "Saving ROM";

            bool saveResult = await Task.Run<bool>(() => { return SaveRom(romPath); });

            progressBar.Value = 0;
            progressBar.Visible = false;
            statusLabel.Text = "";

            ControlState = ControlState.Open;
        }

        private async void closeMenu_Click(object sender, EventArgs e)
        {
            await ClosingRom();
        }

        private void animationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAnimation(animationList.SelectedIndex);
            GC.Collect();
        }

        private void frameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;

            if (animationIndex == -1 || frameIndex == -1)
                return;

            var info = rom.Animations[animationIndex];
            var frame = info.Frames[frameIndex];
            var palette = info.Palette;

            frameEditor.BeginEdit(rom.Animations[animationIndex], frameIndex);
        }

        private void moveFrameUp_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;

            if (animationIndex == -1 || frameIndex == -1) return;

            // Don't move if it's already at the start of the list
            if (frameIndex == 0) return;

            changesMade = true;
            var frames = rom.Animations[animationIndex].Frames;

            ControlState = ControlState.Busy;

            // Swap the frame data
            var tempFrame = frames[frameIndex];
            frames.RemoveAt(frameIndex);
            frames.Insert(frameIndex - 1, tempFrame);

            // Swap the thumbnails
            SwapThumbs(animationIndex, frameIndex - 1, frameIndex);

            frameList.Refresh();
            frameList.Items[frameIndex].Selected = false;
            frameList.Items[frameIndex - 1].Selected = true;

            ControlState = ControlState.Open;
        }

        private void moveFrameDown_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;

            if (animationIndex == -1 || frameIndex == -1) return;

            // Don't move if it's already at the end of the list
            if (frameIndex == frameList.Items.Count - 1) return;

            changesMade = true;
            var frames = rom.Animations[animationIndex].Frames;

            ControlState = ControlState.Busy;

            // Swap the frame data
            var tempFrame = frames[frameIndex];
            frames.RemoveAt(frameIndex);
            frames.Insert(frameIndex + 1, tempFrame);

            // Swap the thumbnails
            SwapThumbs(animationIndex, frameIndex, frameIndex + 1);

            frameList.Refresh();
            frameList.Items[frameIndex].Selected = false;
            frameList.Items[frameIndex + 1].Selected = true;

            ControlState = ControlState.Open;
        }

        private void insertFrame_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;

            if (animationIndex == -1 || frameIndex == -1) return;

            // Ensure that we haven't hit the max number of frames
            if (rom.Animations[animationIndex].Frames.Count == 256) return;

            changesMade = true;
            var frames = rom.Animations[animationIndex].Frames;

            ControlState = ControlState.Busy;

            // Create a new frame
            var newFrame = new Frame();
            frames.Insert(frameIndex, newFrame);

            // Create a new thumbnail
            Bitmap thumb = GenerateThumb(animationIndex, frameIndex);
            InsertThumb(thumb, animationIndex, frameIndex);

            LoadAnimation(animationIndex);
            frameList.Items[frameIndex].Selected = true;

            ControlState = ControlState.Open;
        }

        private void deleteFrame_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;
            
            if (animationIndex == -1 || frameIndex == -1) return;

            // Ensure that we haven't hit the min number of frames
            if (rom.Animations[animationIndex].Frames.Count == 1) return;

            changesMade = true;
            var frames = rom.Animations[animationIndex].Frames;

            ControlState = ControlState.Busy;

            // Delete the frame
            frames.RemoveAt(frameIndex);

            // Delete the thumbnail
            DeleteThumb(animationIndex, frameIndex);

            LoadAnimation(animationIndex);

            if (frameIndex < frames.Count - 1)
                frameList.Items[frameIndex].Selected = true;
            else
                frameList.Items[frameIndex - 1].Selected = true;

            ControlState = ControlState.Open;
        }
        
        

        private void gridMenu_CheckedChanged(object sender, EventArgs e)
        {
            frameEditor.Grid = gridMenu.Checked;
            appConfig.Grid = gridMenu.Checked;
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            if (frameEditor.Zoom == 4) return;
            frameEditor.Zoom++;
            appConfig.Zoom = frameEditor.Zoom;
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            if (frameEditor.Zoom == 1) return;
            frameEditor.Zoom--;
            appConfig.Zoom = frameEditor.Zoom;
        }

        private void frameEditor_FrameChanged(object sender, FrameChangedEventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;

            if (animationIndex == -1 || frameIndex == -1) return;

            // Update the thumbnail
            UpdateThumb(animationIndex, frameIndex);
            frameList.Refresh();

            changesMade = true;
        }

        private void undoMenu_Click(object sender, EventArgs e)
        {
            frameEditor.Undo();
        }

        private void copyImageMenu_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            int frameIndex = FrameIndex;

            if (animationIndex == -1 || frameIndex == -1) return;

            Bitmap render = rom.Animations[animationIndex].RenderFrame(frameIndex);
            Clipboard.SetImage(render);
        }

        private async void editParametersButton_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;

            if (animationIndex == -1) return;

            // Make a copy of the animation info in case the user clicks Cancel
            var infoCopy = new Animation(rom.Animations[animationIndex]);
            var editForm = new ParameterBox(infoCopy, rom.Tilesets);

            if (editForm.ShowDialog() == DialogResult.Cancel)
                return;

            changesMade = true;

            // Copy back
            rom.Animations[animationIndex] = infoCopy;

            // Generate new thumbs
            ControlState = ControlState.Busy;
            progressBar.Value = 0;
            progressBar.Visible = true;
            statusLabel.Text = "Generating thumbs";

            await Task.Run(() => UpdateAnimationThumbs(animationIndex));
            frameList.Refresh();

            statusLabel.Text = "";
            progressBar.Visible = false;
            ControlState = ControlState.Open;

            // Update the tileset for the frame editor
            frameEditor.UpdateEdit(infoCopy);
        }

        private void copyMenu_Click(object sender, EventArgs e)
        {
            frameEditor.Copy();
        }

        private void pasteMenu_Click(object sender, EventArgs e)
        {
            frameEditor.Paste();
        }

        /* Due to the horrendous consequences of attempting to run asynchronous callback
           code on a FormClosing event, we will defer everything to a dummy form */
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var closingBox = new ClosingBox(ClosingRom);
            if (closingBox.ShowDialog() == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            // Make a copy of the app config
            var configCopy = appConfig.CreateCopy();
            var settingsBox = new SettingsBox(configCopy);

            if (settingsBox.ShowDialog() == DialogResult.Cancel)
                return;

            // Copy back and save
            appConfig = configCopy;
            appConfig.Save("settings.json");

            // Apply
            frameEditor.GridColor = appConfig.GridColor;
            frameEditor.TileColor = appConfig.TileColor;
            frameEditor.RefreshCanvas();
        }

        private void romConfigMenu_Click(object sender, EventArgs e)
        {
            // Make a copy of the rom config
            var configCopy = rom.Config.CreateCopy();
            var settingsBox = new RomConfigBox(configCopy, rom.Rom.Length);

            if (settingsBox.ShowDialog() == DialogResult.Cancel)
                return;

            // Copy back and save
            rom.Config = configCopy;
            rom.Config.Save(romConfigPath);

            // Apply
            int animationIndex = AnimationIndex;
            LoadAnimationNames();
            animationList.SelectedIndex = animationIndex;
        }

        private void exportGifMenu_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            if (animationIndex == -1) return;

            var animatedGifBox = new AnimatedGifBox(rom.Animations[animationIndex]);
            animatedGifBox.ShowDialog();
        }

        private void editTilesetsMenu_Click(object sender, EventArgs e)
        {
            var tilesetBox = new TilesetBox(rom.Animations, rom.Tilesets);
            if (tilesetBox.ShowDialog() == DialogResult.Cancel)
                return;

            changesMade = true;
        }

        private bool ImportAnimation(byte[] data, out Animation anim)
        {
            try
            {
                anim = new Animation(data, rom.Tilesets);
            }
            catch
            {
                // TODO: relay error message somehow
                anim = null;
                return false;
            }

            return true;
        }

        private void importMenu_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            if (animationIndex == -1) return;

            if (MessageBox.Show("Warning: importing will overwrite the current animation. Continue?",
                "Import",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (importDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] data = File.ReadAllBytes(importDialog.FileName);

                    Animation anim;

                    bool importResult = ImportAnimation(data, out anim);

                    if (!importResult)
                    {
                        MessageBox.Show("There was an error importing the animation.", "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    rom.Animations[animationIndex] = anim;

                    changesMade = true;

                    UpdateAnimationThumbs(animationIndex);
                    LoadAnimation(animationIndex);
                }
            }
        }

        private void exportMenu_Click(object sender, EventArgs e)
        {
            int animationIndex = AnimationIndex;
            if (animationIndex == -1) return;

            if (exportDialog.ShowDialog() == DialogResult.OK)
            {
                // Generate the animation file
                byte[] data = rom.Animations[animationIndex].Export();
                File.WriteAllBytes(exportDialog.FileName, data);
            }
        }
    }

    enum ControlState
    {
        Closed,
        Open,
        Busy
    }
}
