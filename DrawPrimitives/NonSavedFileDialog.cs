using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives
{
    public partial class NonSavedFileDialog : Form
    {
        public NonSavedFileDialog()
        {
            InitializeComponent();

            this.Text = this.ProductName.SplitCamelCase();
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

        public static DialogResult ShowNonSaveDialog()
        {
            return new NonSavedFileDialog().ShowDialog();
        }
    }
}
