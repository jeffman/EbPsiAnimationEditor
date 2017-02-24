using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EbPsiAnimationEditor.EB;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor
{
    public partial class FrameEditor : UserControl
    {
        public delegate void FrameChangedEventHandler(object sender, FrameChangedEventArgs e);
        public event FrameChangedEventHandler FrameChanged;

        Animation anim = null;

        int FrameIndex = -1;
        public Frame Frame
        {
            get
            {
                if (FrameIndex == -1)
                    return null;

                return anim.Frames[FrameIndex];
            }
        }

        Bitmap[] tileCache = null;

        List<TileChange> tileChanges = new List<TileChange>();
        Dictionary<Frame, Stack<IList<TileChange>>> undoStacks = new Dictionary<Frame, Stack<IList<TileChange>>>();

        Frame copiedFrame = null;

        int currentTile = -1;
        public int CurrentTile
        {
            get { return currentTile; }
            set
            {
                int oldTile = currentTile;
                currentTile = value;
                if (Frame == null) return;

                RefreshCanvas();
            }
        }

        int prevDrawX = -1;
        int prevDrawY = -1;

        Pen gridPen = new Pen(Color.FromArgb(170, 140, 50));
        public Color GridColor
        {
            get {  return gridPen.Color; }
            set
            {
                gridPen = new Pen(value);
                if (Frame == null) return;

                RefreshCanvas();
            }
        }

        SolidBrush tileBrush = new SolidBrush(Color.FromArgb(128, 150, 130, 40));
        public Color TileColor
        {
            get { return tileBrush.Color; }
            set
            {
                tileBrush = new SolidBrush(value);
                if (Frame == null) return;

                RefreshCanvas();
            }
        }

        SolidBrush disableBrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128));

        int zoom = 1;
        public int Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (Frame == null) return;

                RefreshCanvas();
            }
        }

        bool grid = false;
        public bool Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                if (Frame == null) return;

                RefreshCanvas();
            }
        }

        public Frame CurrentFrame { get { return Frame; } }

        public int FrameWidth { get { if (Frame == null) return 0; return (Frame.Width * 8 * zoom) + (grid ? Frame.Width + 1 : 0); } }
        public int FrameHeight { get { if (Frame == null) return 0; return (Frame.Height * 8 * zoom) + (grid ? Frame.Height + 1 : 0); } }

        public int TileWidth { get { if (Frame == null) return 0; return (32 * 8 * zoom) + (grid ? 33 : 0); } }
        public int TileHeight { get { if (Frame == null) return 0; return (8 * 8 * zoom) + (grid ? 9 : 0); } }

        public FrameEditor()
        {
            InitializeComponent();

            frameBox.MouseDown += new MouseEventHandler(frameBox_MouseMove);
        }

        public void BeginEdit(Animation anim, int FrameIndex)
        {
            if (anim == null || FrameIndex < 0 || FrameIndex >= anim.Frames.Count)
                return;

            tileChanges.Clear();

            this.anim = anim;
            this.FrameIndex = FrameIndex;

            UpdateTileCache();

            prevDrawX = -1;
            prevDrawY = -1;
            currentTile = 0;

            RefreshCanvas();

            if (!undoStacks.ContainsKey(Frame))
                undoStacks.Add(Frame, new Stack<IList<TileChange>>());
        }

        public void UpdateEdit(Animation anim)
        {
            this.anim = anim;
            UpdateTileCache();
        }

        public void EndEdit()
        {
            tileChanges.Clear();

            tileCache = null;

            this.anim = null;
            this.FrameIndex = -1;

            RefreshCanvas();
            undoStacks.Clear();
        }

        public void UpdateTileCache()
        {
            if (anim == null) return;

            tileCache = new Bitmap[256];

            for (int i = 0; i < 256; i++)
                tileCache[i] = Render.Tile(anim.Tileset.Bitmaps[i], anim.Palette);

            RefreshCanvas();
        }

        public void RefreshCanvas()
        {
            frameBox.Width = FrameWidth;
            frameBox.Height = FrameHeight;

            tileBox.Width = TileWidth;
            tileBox.Height = TileHeight;

            frameBox.Refresh();
            tileBox.Refresh();
        }

        private void DrawGridlines(Graphics g, int width, int height, int size)
        {
            g.PixelOffsetMode = PixelOffsetMode.Default;

            // Vertical lines
            for (int x = 0; x <= width; x += size)
                g.DrawLine(gridPen, x, 0, x, height);

            // Horizontal lines
            for (int y = 0; y <= height; y += size)
                g.DrawLine(gridPen, 0, y, width, y);
        }

        protected virtual void OnFrameChanged(FrameChangedEventArgs e)
        {
            var handler = FrameChanged;
            if (handler != null)
                handler(this, e);
        }

        private void frameBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Frame == null) return;

            int tileOffset = 8 * zoom;
            if (grid) tileOffset++;

            int drawX = e.X / tileOffset;
            int drawY = e.Y / tileOffset;

            if (drawX >= Frame.Width) drawX = Frame.Width - 1;
            if (drawX < 0) drawX = 0;

            if (drawY >= Frame.Height) drawY = Frame.Height - 1;
            if (drawY < 0) drawY = 0;

            if (e.Button == MouseButtons.Left)
            {
                if (drawX != prevDrawX || drawY != prevDrawY)
                {
                    int oldTile = Frame.Tiles[drawX, drawY];
                    if (oldTile != CurrentTile)
                    {

                        Frame.Tiles[drawX, drawY] = CurrentTile;

                        tileChanges.Add(new TileChange(drawX, drawY, oldTile, CurrentTile));

                        prevDrawX = drawX;
                        prevDrawY = drawY;

                        RefreshCanvas();
                    }
                }
            }

            else if (e.Button == MouseButtons.Right)
            {
                CurrentTile = Frame.Tiles[drawX, drawY];
            }
        }

        private void frameBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Frame == null) return;

            // If the changes list is empty, don't bother doing anything
            if (tileChanges.Count == 0)
                return;

            // Copy the tile changes into a new list for the event handler
            var newList = new List<TileChange>();
            newList.AddRange(tileChanges);
            OnFrameChanged(new FrameChangedEventArgs(newList));

            // Make another list for the undo stack
            newList = new List<TileChange>();
            newList.AddRange(tileChanges);
            undoStacks[Frame].Push(newList);

            tileChanges.Clear();
        }

        private void tileBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Frame == null) return;

            int tileOffset = 8 * zoom;
            if (grid) tileOffset++;

            int tileX = e.X / tileOffset;
            int tileY = e.Y / tileOffset;

            if (tileX >= 32) tileX = 31;
            if (tileX < 0) tileX = 0;

            if (tileY >= 8) tileY = 7;
            if (tileY < 0) tileY = 0;

            int tileNum = tileY + (tileX * 8);

            if (e.Button == MouseButtons.Left)
            {
                CurrentTile = tileNum;
            }
        }

        private void frameBox_Paint(object sender, PaintEventArgs e)
        {
            if (Frame == null) return;

            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            int tileOffset = 8 * zoom;
            if (grid) tileOffset++;

            int tileStart = grid ? 1 : 0;

            // Draw the Frame
            for (int tileY = 0; tileY < Frame.Height; tileY++)
            {
                for (int tileX = 0; tileX < Frame.Width; tileX++)
                {
                    g.DrawImage(tileCache[Frame.Tiles[tileX, tileY]],
                        tileStart + tileX * tileOffset,
                        tileStart + tileY * tileOffset,
                        8 * zoom,
                        8 * zoom);
                }
            }

            if (grid)
                DrawGridlines(g, FrameWidth, FrameHeight, tileOffset);

            // Draw the disable mask if applicable
            if (!this.Enabled)
            {
                g.FillRectangle(disableBrush, 0, 0, FrameWidth, FrameHeight);
            }
        }

        private void tileBox_Paint(object sender, PaintEventArgs e)
        {
            if (Frame == null) return;

            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            int tileOffset = 8 * zoom;
            if (grid) tileOffset++;

            int tileStart = grid ? 1 : 0;

            // Draw the tileset
            for (int tileY = 0; tileY < 8; tileY++)
            {
                for (int tileX = 0; tileX < 32; tileX++)
                {
                    g.DrawImage(tileCache[tileY + (tileX * 8)],
                        tileStart + tileX * tileOffset,
                        tileStart + tileY * tileOffset,
                        8 * zoom,
                        8 * zoom);
                }
            }

            // Draw the tile marker
            g.FillRectangle(tileBrush,
                tileStart + (currentTile >> 3) * tileOffset,
                tileStart + (currentTile & 7) * tileOffset,
                8 * zoom,
                8 * zoom);

            if (grid)
                DrawGridlines(g, FrameWidth, FrameHeight, tileOffset);
            
            // Draw the disable mask if applicable
            if (!this.Enabled)
            {
                g.FillRectangle(disableBrush, 0, 0, TileWidth, TileHeight);
            }
        }

        public bool Undo()
        {
            if (Frame == null) return false;

            // Get the most recent list of changes
            if (undoStacks[Frame].Count == 0) return false;

            var changes = undoStacks[Frame].Pop();

            // Revert the changes
            for (int i = changes.Count - 1; i >= 0; i--) // We should go in reverse order
            {
                var change = changes[i];
                Frame.Tiles[change.TileX, change.TileY] = change.OldTile;
            }

            OnFrameChanged(null);
            RefreshCanvas();

            return true;
        }

        public void Copy()
        {
            if (Frame == null) return;

            copiedFrame = Frame;
        }

        public void Paste()
        {
            if (Frame == null) return;
            if (copiedFrame == null) return;

            var changes = new List<TileChange>();
            for (int y = 0; y < Frame.Height; y++)
            {
                for (int x = 0; x < Frame.Width; x++)
                {
                    int oldTile = Frame.Tiles[x, y];
                    Frame.Tiles[x, y] = copiedFrame.Tiles[x, y];
                    changes.Add(new TileChange(x, y, oldTile, copiedFrame.Tiles[x, y]));
                }
            }

            OnFrameChanged(new FrameChangedEventArgs(changes));

            var newList = new List<TileChange>();
            newList.AddRange(changes);
            undoStacks[Frame].Push(newList);

            RefreshCanvas();
        }

        public override void Refresh()
        {
            RefreshCanvas();
            base.Refresh();
        }
    }

    public class FrameChangedEventArgs : EventArgs
    {
        public IList<TileChange> Changes { get; private set; }

        public FrameChangedEventArgs(IList<TileChange> tileChanges)
        {
            Changes = tileChanges;
        }
    }

    public struct TileChange
    {
        public readonly int TileX;
        public readonly int TileY;
        public readonly int OldTile;
        public readonly int NewTile;

        public TileChange(int tileX, int tileY, int oldTile, int newTile)
        {
            TileX = tileX;
            TileY = tileY;
            OldTile = oldTile;
            NewTile = newTile;
        }
    }
}
