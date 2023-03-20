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
    public partial class CanvasDialog : Form
    {
        public bool IsSelectedImage { get; private set; }

        public CanvasDialog(string text)
        {
            InitializeComponent();

            Text = text;
        }

        public CanvasDialog(string text, Canvas canvas)
        {
            InitializeComponent();

            width_numericUpDown.Value = canvas.Size.Width;
            height_numericUpDown.Value = canvas.Size.Height;
            color_pictureBox.BackColor = canvas.BackColor;
            Text = text;
        }

        public Canvas GetValue()
        {
            return new Canvas(new Size((int)width_numericUpDown.Value, (int)height_numericUpDown.Value), color_pictureBox.BackColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void pickColor_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.Color = color_pictureBox.BackColor;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                color_pictureBox.BackColor = dialog.Color;
            }
        }
    }
}
