using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EbPsiAnimationEditor.EB;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor
{
    public partial class RomConfigBox : Form
    {
        RomConfig romConfig;
        bool clicking = false;

        private int RangeIndex
        {
            get
            {
                if (freeRangesList.SelectedIndices.Count != 1)
                    return -1;

                return freeRangesList.SelectedIndices[0];
            }
            set
            {
                if (value == -1)
                    freeRangesList.SelectedIndices.Clear();

                else
                    freeRangesList.Items[value].Selected = true;
            }
        }

        public RomConfigBox(RomConfig romConfig, int romSize)
        {
            InitializeComponent();

            this.romConfig = romConfig;

            psiAnimationInfo.Maximum = romSize - 1;
            psiArrangements.Maximum = romSize - 1;
            psiPalettes.Maximum = romSize - 1;
            rangeStart.Maximum = romSize - 1;
            rangeEnd.Maximum = romSize - 1;
        }

        private void LoadAnimationNames()
        {
            animationNames.BeginUpdate();

            animationNames.Items.Clear();
            animationNames.Items.AddRange(romConfig.AnimationNames.Select(
                (n, i) => String.Format("[{0:D2}] {1}", i, n)).ToArray());

            animationNames.EndUpdate();
        }

        private void LoadFreeRanges()
        {
            freeRangesList.BeginUpdate();

            freeRangesList.Items.Clear();

            var ranges = romConfig.FreeRanges.Ranges;
            for (int i = 0; i < ranges.Count; i++)
            {
                freeRangesList.Items.Add(new ListViewItem(new string[]
                {
                    "0x" + ranges[i].Start.ToString("X2"),
                    "0x" + ranges[i].End.ToString("X2"),
                    "0x" + ranges[i].Length.ToString("X2")
                }));
            }

            freeRangesList.EndUpdate();
        }

        private void RomConfigBox_Load(object sender, EventArgs e)
        {
            LoadAnimationNames();
            animationNames.SelectedIndex = 0;

            psiAnimationCount.Value = romConfig.Parameters["psi animation count"];
            psiAnimationInfo.Value = romConfig.Parameters["psi animation info"];
            psiArrangements.Value = romConfig.Parameters["psi animation arrangements"];
            psiPalettes.Value = romConfig.Parameters["psi animation palettes"];

            LoadFreeRanges();
            if (freeRangesList.Items.Count > 0)
                RangeIndex = 0;
        }

        private void animationNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = animationNames.SelectedIndex;
            if (index == -1)
            {
                animationName.Text = "";
                return;
            }

            animationName.Text = romConfig.AnimationNames[animationNames.SelectedIndex];
        }

        private void animationName_Leave(object sender, EventArgs e)
        {
            int index = animationNames.SelectedIndex;
            if (index == -1)
                return;

            romConfig.AnimationNames[index] = animationName.Text;

            LoadAnimationNames();
            animationNames.SelectedIndex = index;
        }

        private void animationName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                animationName_Leave(sender, null);
            }
        }

        private void defaultParameters_Click(object sender, EventArgs e)
        {
            var defaultConfig = RomConfig.GenerateDefaults();

            psiAnimationCount.Value = defaultConfig.Parameters["psi animation count"];
            psiAnimationInfo.Value = defaultConfig.Parameters["psi animation info"];
            psiArrangements.Value = defaultConfig.Parameters["psi animation arrangements"];
            psiPalettes.Value = defaultConfig.Parameters["psi animation palettes"];
        }

        private void defaultFreeRanges_Click(object sender, EventArgs e)
        {
            var defaultConfig = RomConfig.GenerateDefaults();

            romConfig.FreeRanges = defaultConfig.FreeRanges;

            int index = RangeIndex;
            LoadFreeRanges();
            RangeIndex = 0;
        }

        private void freeRangesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = RangeIndex;
            if (index == -1)
                return;

            clicking = true;
            rangeStart.Value = romConfig.FreeRanges.Ranges[index].Start;
            rangeEnd.Value = romConfig.FreeRanges.Ranges[index].End;
            clicking = false;
        }

        private void insertRange_Click(object sender, EventArgs e)
        {
            try
            {
                romConfig.FreeRanges.Add(new Range(0, 1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                freeRangesList_SelectedIndexChanged(null, null);
                return;
            }

            LoadFreeRanges();
            RangeIndex = freeRangesList.Items.Count - 1;
        }

        private void deleteRange_Click(object sender, EventArgs e)
        {
            int index = RangeIndex;
            if (index == -1)
                return;

            romConfig.FreeRanges.Ranges.RemoveAt(index);

            LoadFreeRanges();
            
            if (freeRangesList.Items.Count == 0)
                return;

            if (index >= freeRangesList.Items.Count)
                RangeIndex = index - 1;

            else
                RangeIndex = index;
        }

        private void rangeApplyButton_Click(object sender, EventArgs e)
        {
            if (clicking)
                return;

            int index = RangeIndex;
            if (RangeIndex == -1)
                return;

            try
            {
                romConfig.FreeRanges.Modify(index, new Range((int)rangeStart.Value, (int)(rangeEnd.Value - rangeStart.Value + 1)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            LoadFreeRanges();
            RangeIndex = index;
        }
    }
}
