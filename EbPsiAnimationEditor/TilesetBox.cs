using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EbPsiAnimationEditor.EB;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor
{
    public partial class TilesetBox : Form
    {
        List<Tileset> tilesets;
        Animation[] animationInfo;

        Palette tilePalette = new Palette();
        List<Bitmap[]> tileCache = new List<Bitmap[]>();

        Tileset CurrentTileset { get { return tilesets[tilesetList.SelectedIndex]; } }

        public TilesetBox(Animation[] animationInfo, List<Tileset> tilesets)
        {
            InitializeComponent();

            this.animationInfo = animationInfo;
            this.tilesets = tilesets;

            // Create the tile bitmap cache
            for (int i = 0; i < 4; i++)
                tilePalette.Colors[i] = Color.FromArgb(i * 80, i * 80, i * 80);

            for (int i = 0; i < tilesets.Count; i++)
                tileCache.Add(CreateTileCache(tilesets[i]));
        }

        private Bitmap[] CreateTileCache(Tileset tileset)
        {
            var cache = new Bitmap[tileset.Bitmaps.Length];

            for (int i = 0; i < cache.Length; i++)
                cache[i] = Render.Tile(tileset.Bitmaps[i], tilePalette);

            return cache;
        }

        private bool IsTilesetUsed(Tileset tileset)
        {
            for (int i = 0; i < animationInfo.Length; i++)
                if (animationInfo[i].Tileset == tileset)
                    return true;

            return false;
        }

        private void TilesetBox_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < tilesets.Count; i++)
            {
                tilesetList.Items.Add(String.Format(
                    "Tileset {0}",
                    i + 1));
            }

            tilesetList.SelectedIndex = 0;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            importMenu.Show(MousePosition);
        }

        private void tilesetPreview_Paint(object sender, PaintEventArgs e)
        {
            var tileset = CurrentTileset;
            if (tileset == null) return;

            // Draw the tileset
            for (int tileY = 0; tileY < 8; tileY++)
            {
                for (int tileX = 0; tileX < 32; tileX++)
                {
                    int tileIndex = tileY + (tileX * 8);
                    e.Graphics.DrawImage(tileCache[tilesetList.SelectedIndex][tileIndex],
                        tileX * 8, tileY * 8);
                }
            }
        }

        private void tilesetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tilesetPreview.Refresh();
        }

        private void importNewMenu_Click(object sender, EventArgs e)
        {
            if (importDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(importDialog.FileName);

                Tileset tileset;

                try
                {
                    tileset = new Tileset(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not import tileset. Reason: " +
                        ex.Message, "Error importing tileset", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                tileCache.Add(CreateTileCache(tileset));
                tilesets.Add(tileset);
                tilesetList.Items.Add(String.Format(
                    "Tileset {0}",
                    tilesetList.Items.Count + 1));
            }
        }

        private void importOverwriteMenu_Click(object sender, EventArgs e)
        {
            if (IsTilesetUsed(CurrentTileset))
            {
                MessageBox.Show("Cannot overwrite a tileset that is currently in use.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (importDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(importDialog.FileName);

                Tileset tileset;

                try
                {
                    tileset = new Tileset(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not import tileset. Reason: " +
                        ex.Message, "Error importing tileset", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                tileCache[tilesetList.SelectedIndex] = CreateTileCache(tileset);
                tilesets[tilesetList.SelectedIndex] = tileset;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (exportDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] data = CurrentTileset.Write();
                File.WriteAllBytes(exportDialog.FileName, data);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (IsTilesetUsed(CurrentTileset))
            {
                MessageBox.Show("Cannot overwrite a tileset that is currently in use.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            int index = tilesetList.SelectedIndex;
            tilesets.RemoveAt(index);
            tileCache.RemoveAt(index);
            tilesetList.Items.RemoveAt(index);

            if (index >= tilesetList.Items.Count)
                tilesetList.SelectedIndex = tilesetList.Items.Count - 1;
            else
                tilesetList.SelectedIndex = index;
        }
    }
}
