using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using EbPsiAnimationEditor.EB;
using EbPsiAnimationEditor.Tools;

namespace EbPsiAnimationEditor
{
    public partial class AnimatedGifBox : Form
    {
        byte[] encodedImage = null;
        Image animatedImage = null;
        Animation anim;

        bool encoding = false;

        public AnimatedGifBox(Animation anim)
        {
            InitializeComponent();

            this.anim = anim;
        }

        private async void previewButton_Click(object sender, EventArgs e)
        {
            previewButton.Text = "Encoding...";
            previewButton.Enabled = false;
            frameDuration.Enabled = false;
            saveButton.Enabled = false;
            cancelButton.Enabled = false;
            encoding = true;

            await Task.Run(() => EncodeImage());

            previewButton.Text = "Preview";
            previewButton.Enabled = true;
            frameDuration.Enabled = true;
            saveButton.Enabled = true;
            cancelButton.Enabled = true;
            encoding = false;

            previewBox.Image = animatedImage;
        }

        private void EncodeImage()
        {
            // Get the frame bitmaps
            var frames = new Bitmap[anim.Frames.Count];

            for (int i = 0; i < frames.Length; i++)
                frames[i] = anim.RenderFrameIndexed( i);

            encodedImage = AnimatedGif.EncodeFrames(frames, (int)frameDuration.Value);

            var ms = new MemoryStream(encodedImage);
            animatedImage = Image.FromStream(ms);
        }

        private void AnimatedGifBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (encoding)
                e.Cancel = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveGifDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveGifDialog.FileName, encodedImage);
                this.Close();
            }
        }
    }
}
