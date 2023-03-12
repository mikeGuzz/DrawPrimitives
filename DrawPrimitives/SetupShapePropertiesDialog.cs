using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DrawPrimitives
{
    public partial class SetupShapePropertiesDialog : Form
    {
        private readonly Shape shape;
        private Pen? pen;
        private Brush? brush;

        public SetupShapePropertiesDialog(string text, Shape shape)
        {
            InitializeComponent();

            this.shape = shape;
            position_textBox.Text = $"{shape.Bounds.X}, {shape.Bounds.Y}";
            size_textBox.Text = $"{shape.Bounds.Width}, {shape.Bounds.Height}";
            if(shape is TextBoxShape textBoxShape)
            {
                textBox.Text = textBoxShape.Text;
                fontInfo_label.Text = textBoxShape.Font.Name;
                fontInfo_label.Font = textBoxShape.Font;
            }

            text_groupBox.Enabled = shape is TextBoxShape;
        }

        public Shape GetValue()
        {
            var ob = (Shape)shape.Clone();
            
            if (TryParsePoint(position_textBox.Text, out var p))
                ob.Bounds.Location = p;
            if(TryParseSize(size_textBox.Text, out var s))
                ob.Bounds.Size = s;
            if (pen != null)
                ob.Pen = pen;
            if(brush != null)
                ob.Brush = brush;
            if(shape is TextBoxShape)
            {
                var textBoxShape = (TextBoxShape)shape;
                textBoxShape.Text = textBox.Text;
                textBoxShape.Font = fontInfo_label.Font;
            }

            return ob;
        }

        private bool TryParsePoint(string str, out Point res)
        {
            res = Point.Empty;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            res = new Point(int.Parse(arr[0]), int.Parse(arr[1]));
            return true;
        }

        private bool TryParseSize(string str, out Size res)
        {
            res = Size.Empty;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            res = new Size(int.Parse(arr[0]), int.Parse(arr[1]));
            return true;
        }

        private bool IsValidPoint(string str)
        {
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            return int.TryParse(arr[1], out _) && int.TryParse(arr[0], out _);
        }

        private void NumTextBoxFocusLeave(object? sender, EventArgs e)
        {
            if(sender is TextBox box)
            {
                if (!IsValidPoint(box.Text))
                {
                    MessageBox.Show($"Invalid value '{box.Text}' in '{box.Name}'");
                }
            }
        }

        private void pickPen_button_Click(object sender, EventArgs e)
        {
            var dialog = new PenSetupDialog("Pen setting", shape.Pen == null ? new Pen(Color.Black, 2) : shape.Pen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pen = dialog.GetValue();
                pen_pictureBox.Invalidate();
            }
        }

        private void pen_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Pen _pen;
            if (pen != null)
                _pen = pen;
            else if (shape.Pen != null)
                _pen = shape.Pen;
            else return;
            var bounds = pen_pictureBox.ClientRectangle;
            var g = e.Graphics;
            
            pen_pictureBox.BackColor = Color.FromArgb(_pen.Color.ToArgb() ^ 0xffffff);
            g.DrawLine(_pen, new Point((int)_pen.Width / 2, bounds.Height / 2), new Point(bounds.Width - ((int)_pen.Width / 2), bounds.Height / 2));
        }

        private void pick_button_Click(object sender, EventArgs e)
        {
            var dialog = new BrushSetupDialog("Fill setting", shape.Brush == null ? new SolidBrush(Color.White) : shape.Brush);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                brush = dialog.GetValue();
                fill_pictureBox.Invalidate();
            }
        }

        private void fill_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if(brush != null)
                e.Graphics.FillRectangle(brush, fill_pictureBox.ClientRectangle);
            else if(shape.Brush != null)
                e.Graphics.FillRectangle(shape.Brush, fill_pictureBox.ClientRectangle);
        }

        private void pickFont_button_Click(object sender, EventArgs e)
        {
            if(shape is TextBoxShape textOb)
            {
                var dialog = new FontDialog();
                dialog.Font = textOb.Font;
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    fontInfo_label.Text = dialog.Font.Name;
                    fontInfo_label.Font = dialog.Font;
                }
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
