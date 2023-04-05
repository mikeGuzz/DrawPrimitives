using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class TransformEditor : Form
    {
        public Size _Size
        {
            get => new Size((int)w_numericUpDown.Value, (int)h_numericUpDown.Value);
            set
            {
                w_numericUpDown.Value = value.Width;
                h_numericUpDown.Value = value.Height;
                UpdateLocationNumeric();
            }
        }

        public Point _Location
        {
            get => new Point((int)x_numericUpDown.Value, (int)y_numericUpDown.Value);
            set
            {
                y_numericUpDown.Value = value.Y;
                x_numericUpDown.Value = value.X;
            }
        }

        public Rectangle _Bounds
        {
            get => new Rectangle(_Location, _Size);
            set
            {
                _Size = value.Size;
                _Location = value.Location;
            }
        }

        public TransformEditor(Size canvSize)
        {
            InitializeComponent();
            Setup(canvSize);
        }

        public TransformEditor(Size canvSize, Rectangle rect)
        {
            InitializeComponent();
            Setup(canvSize);

            _Bounds = rect;
        }

        private void Setup(Size canvSize)
        {
            w_numericUpDown.Maximum = 30000;
            h_numericUpDown.Maximum = 30000;
            w_numericUpDown.Minimum = Shape.MinimumSize.Width;
            h_numericUpDown.Minimum = Shape.MinimumSize.Height;
            x_numericUpDown.Maximum = canvSize.Width;
            y_numericUpDown.Maximum = canvSize.Height;
            UpdateLocationNumeric();
        }

        private void UpdateLocationNumeric()
        {
            x_numericUpDown.Minimum = -w_numericUpDown.Value;
            y_numericUpDown.Minimum = -h_numericUpDown.Value;
        }

        private void SizeNumericValueChanged(object sender, EventArgs e)
        {
            UpdateLocationNumeric();
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
