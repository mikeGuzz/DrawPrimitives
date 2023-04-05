using DrawPrimitives.Shapes;
using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives.Dialogs.Editors.ShapesEditor
{
    public partial class CubeEditor : Form
    {
        //use for preview
        private CubeShape shape = new CubeShape(new Pen(Color.Black, 3), new SolidBrushHolder((SolidBrush)Brushes.White), 45);

        private Rectangle bounds => new Rectangle(pictureBox1.ClientRectangle.X + 5, pictureBox1.ClientRectangle.Y + 5, pictureBox1.ClientRectangle.Width - 10, pictureBox1.ClientRectangle.Height - 10);

        public int Angle
        {
            get => shape.Angle; 
            set
            {
                shape.Angle = value;
                numericUpDown1.Value = value;
                pictureBox1.Invalidate();
            }
        }

        public CubeEditor()
        {
            InitializeComponent();
            MinimumSize = Size;
            shape.Bound(bounds);
        }

        public CubeEditor(CubeShape shape)
        {
            InitializeComponent();
            MinimumSize = Size;
            this.shape.Bound(bounds);

            Angle = shape.Angle;
            pictureBox1.Invalidate();
        }

        public CubeEditor(int angle)
        {
            InitializeComponent();
            MinimumSize = Size;
            shape.Bound(bounds);

            Angle = angle;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            shape.Draw(e.Graphics);
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Angle = (int)numericUpDown1.Value;
            if(trackBar1.Value != Angle)
                trackBar1.Value = shape.Angle;
            pictureBox1.Invalidate();
        }

        private void ParallelepipedEditor_SizeChanged(object sender, EventArgs e)
        {
            shape.Bound(bounds);
            pictureBox1.Invalidate();
        }
    }
}
