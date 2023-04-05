using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class ZoomFactorEditor : Form
    {
        public decimal ScaleFactor => numericUpDown1.Value / 100;

        public ZoomFactorEditor()
        {
            InitializeComponent();
        }

        public ZoomFactorEditor(decimal startValue)
        {
            InitializeComponent();

            numericUpDown1.Value = startValue * 100;
            if(numericUpDown1.CanFocus)
                numericUpDown1.Focus();
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
