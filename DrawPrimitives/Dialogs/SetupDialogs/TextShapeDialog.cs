using DrawPrimitives.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives.Dialog.SetupDialogs
{
    public partial class TextShapeDialog : Form
    {
        public Font SelectedFont => textBox1.Font;
        public string SelectedText => textBox1.Text;

        public TextShapeDialog(string titleText)
        {
            InitializeComponent();

            Text = titleText;
        }

        public TextShapeDialog(string titleText, string startText)
        {
            InitializeComponent();

            textBox1.Text = startText;
            Text = titleText;
        }

        public TextShapeDialog(string titleText, string startText, Font startFont)
        {
            InitializeComponent();

            textBox1.Text = startText;
            textBox1.Font = startFont;
            Text = titleText;
        }

        public TextShapeDialog(string titleText, TextBoxShape shape)
        {
            InitializeComponent();

            textBox1.Text = shape.Text;
            textBox1.Font = shape.Font;
            Text = titleText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new FontDialog();
            dialog.Font = textBox1.Font;
            if(dialog.ShowDialog() == DialogResult.OK )
            {
                textBox1.Font = dialog.Font;
            }
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
