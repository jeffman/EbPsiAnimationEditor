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
    public partial class ClosingBox : Form
    {
        Func<Task<DialogResult>> closingTask;
        bool isClosing = true;

        public ClosingBox(Func<Task<DialogResult>> closingTask)
        {
            InitializeComponent();

            this.ClientSize = closingGif.Size;
            this.closingTask = closingTask;
        }

        private async void ClosingBox_Load(object sender, EventArgs e)
        {
            var result = await closingTask();

            this.DialogResult = result;
            isClosing = false;
            this.Close();
        }

        private void ClosingBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing)
                e.Cancel = true;
        }
    }
}
