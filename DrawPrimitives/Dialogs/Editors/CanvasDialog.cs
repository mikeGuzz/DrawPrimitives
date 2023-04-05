using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawPrimitives.My;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class CanvasDialog : Form
    {
        public Canvas Canvas
        {
            get
            {
                return new Canvas(new Size((int)w_numericUpDown.Value, (int)h_numericUpDown.Value), Color.FromArgb((int)opacity_numericUpDown.Value, colorPrev_pictureBox.BackColor));
            }
            set
            {
                w_numericUpDown.Value = value.Size.Width;
                h_numericUpDown.Value = value.Size.Height;
                var color = value.Color;
                opacity_numericUpDown.Value = color.A;
                colorPrev_pictureBox.BackColor = Color.FromArgb(255, color);
            }
        }

        public CanvasDialog()
        {
            InitializeComponent();
            Setup();
        }

        public CanvasDialog(Canvas canvas)
        {
            InitializeComponent();
            Setup();

            Canvas = canvas;
        }

        private void Setup()
        {
            w_numericUpDown.Minimum = Shape.MinimumSize.Width;
            h_numericUpDown.Minimum = Shape.MinimumSize.Height;
        }

        private void pickColor_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.Color = colorPrev_pictureBox.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                colorPrev_pictureBox.BackColor = dialog.Color;
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
