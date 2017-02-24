using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.EB;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor
{
    public partial class ParameterBox : Form
    {
        Animation anim;
        List<Tileset> tilesets;

        Label[] paletteLabels = new Label[4];

        public ParameterBox(Animation anim, List<Tileset> tilesets)
        {
            InitializeComponent();

            // Draw the palette labels
            for (int i = 0; i < 4; i++)
            {
                var label = new Label();
                label.AutoSize = false;
                label.Size = new Size(32, 32);
                label.Left = paletteMarker.Left + 1 + (i * 32);
                label.Top = paletteMarker.Top + 1;
                label.Tag = i;
                this.Controls.Add(label);
                label.BringToFront();
                label.Click += new EventHandler(palette_Click);

                paletteLabels[i] = label;
            }
            
            // Load the target enum
            foreach (AnimationTarget animTarget in Enum.GetValues(typeof(AnimationTarget)))
                target.Items.Add(animTarget);

            // Load the list of tilesets
            for (int i = 0; i < tilesets.Count; i++)
                tilesetList.Items.Add(String.Format(
                    "Tileset {0}",
                    i + 1));

            this.anim = anim;
            this.tilesets = tilesets;

            LoadParameters();
        }

        private void palette_Click(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            int index = (int)l.Tag;

            colorDialog.Color = anim.Palette.Colors[index];
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color newColor = Color.FromArgb(
                    colorDialog.Color.R & 0xF8,
                    colorDialog.Color.G & 0xF8,
                    colorDialog.Color.B & 0xF8);

                anim.Palette.Colors[index] = newColor;
                l.BackColor = newColor;
            }
        }

        private void LoadParameters()
        {
            frameInterval.Value = anim.FrameInterval;
            paletteInterval.Value = anim.PaletteInterval;
            enemyColorInterval.Value = anim.EnemyColorInterval;
            enemyColorDelay.Value = anim.EnemyColorDelay;
            unknownA.Value = anim.UnknownA;
            unknownB.Value = anim.UnknownB;

            target.SelectedItem = anim.Target;
            tilesetList.SelectedIndex = tilesets.IndexOf(anim.Tileset);

            enemyColor.BackColor = anim.EnemyColor;

            for (int i = 0; i < 4; i++)
                paletteLabels[i].BackColor = anim.Palette.Colors[i];
        }

        private void enemyColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = anim.EnemyColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color newColor = Color.FromArgb(
                    colorDialog.Color.R & 0xF8,
                    colorDialog.Color.G & 0xF8,
                    colorDialog.Color.B & 0xF8);

                anim.EnemyColor = newColor;
                enemyColor.BackColor = newColor;
            }
        }

        private void enemyColorDelay_ValueChanged(object sender, EventArgs e)
        {
            anim.EnemyColorDelay = (int)enemyColorDelay.Value;
        }

        private void enemyColorInterval_ValueChanged(object sender, EventArgs e)
        {
            anim.EnemyColorInterval = (int)enemyColorInterval.Value;
        }

        private void frameInterval_ValueChanged(object sender, EventArgs e)
        {
            anim.FrameInterval = (int)frameInterval.Value;
        }

        private void paletteInterval_ValueChanged(object sender, EventArgs e)
        {
            anim.PaletteInterval = (int)paletteInterval.Value;
        }

        private void unknownA_ValueChanged(object sender, EventArgs e)
        {
            anim.UnknownA = (int)unknownA.Value;
        }

        private void unknownB_ValueChanged(object sender, EventArgs e)
        {
            anim.UnknownB = (int)unknownB.Value;
        }

        private void target_SelectedIndexChanged(object sender, EventArgs e)
        {
            anim.Target = (AnimationTarget)target.SelectedItem;
        }

        private void tilesetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            anim.Tileset = tilesets[tilesetList.SelectedIndex];
        }
    }
}
