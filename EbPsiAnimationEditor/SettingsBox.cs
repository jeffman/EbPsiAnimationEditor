using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EbPsiAnimationEditor
{
    public partial class SettingsBox : Form
    {
        AppConfig appConfig;

        public SettingsBox(AppConfig appConfig)
        {
            InitializeComponent();

            this.appConfig = appConfig;

            fastCompress.Checked = appConfig.FastCompress;
            multiThreaded.Checked = appConfig.Multithreaded;
            gridColor.BackColor = appConfig.GridColor;
            tileColor.BackColor = appConfig.TileColor;
        }

        private void gridColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = appConfig.GridColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                appConfig.GridColor = colorDialog.Color;
                gridColor.BackColor = colorDialog.Color;
            }
        }

        private void tileColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = appConfig.TileColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                appConfig.TileColor = Color.FromArgb(128, colorDialog.Color);
                tileColor.BackColor = appConfig.TileColor;
            }
        }
    }
}
