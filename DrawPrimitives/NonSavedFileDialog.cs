using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives
{
    public partial class NonSavedFileDialog : Form
    {
        public NonSavedFileDialog()
        {
            InitializeComponent();

            Text = ProductName.SplitCamelCase();
            save_button.Focus();
        }

        public NonSavedFileDialog(string fileName)
        {
            InitializeComponent();

            label.Text = string.Format("Do you want to save changes to {0}?", fileName);
            Text = this.ProductName.SplitCamelCase();
            save_button.Focus();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void dontSave_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
