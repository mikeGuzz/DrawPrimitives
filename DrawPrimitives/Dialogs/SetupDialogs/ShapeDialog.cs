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
using DrawPrimitives.Shapes;
using DrawPrimitives;
using System.Drawing.Drawing2D;

namespace DrawPrimitives.Dialog.SetupDialogs
{
    public partial class ShapeDialog : Form
    {
        private Pen? pen;
        private Brush? brush;

        public bool IsEditedSize => size_textBox.Text.TryParseSize(out _);
        public bool IsEditedLocation => position_textBox.Text.TryParsePoint(out _);
        public bool IsEditedBrush => brush_checkBox.CheckState != CheckState.Indeterminate;
        public bool IsEditedPen => pen_checkBox.CheckState != CheckState.Indeterminate;
        public bool IsEditedFlipX => flipX_checkBox.CheckState != CheckState.Indeterminate;
        public bool IsEditedFlipY => flipY_checkBox.CheckState != CheckState.Indeterminate;

        public Size ShapeSize
        {
            get
            {
                if (size_textBox.Text.TryParseSize(out var size))
                    return size;
                return Size.Empty;
            }
        }
        public Point ShapeLocation
        {
            get
            {
                if (position_textBox.Text.TryParsePoint(out var point))
                    return point;
                return Point.Empty;
            }
        }
        public Pen? ShapePen => pen;
        public Brush? ShapeBrush => brush;
        public bool ShapeFlipX => flipX_checkBox.Checked;
        public bool ShapeFlipY => flipY_checkBox.Checked;

        public ShapeDialog(string text, Shape shape)
        {
            InitializeComponent();

            Text = text;
            var bounds = shape.GetBounds();
            position_textBox.Text = $"{bounds.X}, {bounds.Y}";
            size_textBox.Text = $"{bounds.Width}, {bounds.Height}";
            brush = shape.Brush;
            pen = shape.Pen;
            pen_checkBox.Checked = pen != null;
            brush_checkBox.Checked = brush != null;
            flipX_checkBox.Checked = shape.FlipX;
            flipY_checkBox.Checked = shape.FlipY;
        }

        public ShapeDialog(string text, ICollection<Shape> shapes)
        {
            InitializeComponent();

            Text = text;
            var first = shapes.First();
            var firstBounds = first.GetBounds();
            position_textBox.Text = shapes.All(i => i.GetBounds().Location == firstBounds.Location) ? $"{firstBounds.X}, {firstBounds.Y}" : string.Empty;
            size_textBox.Text = shapes.All(i => i.GetBounds().Size == firstBounds.Size) ? $"{firstBounds.Width}, {firstBounds.Height}" : string.Empty;
            brush = shapes.All(i => i.Brush == first.Brush) ? first.Brush : null;
            pen = shapes.All(i => i.Pen == first.Pen) ? first.Pen : null;
            pen_checkBox.CheckState = (pen == null) ? CheckState.Indeterminate : CheckState.Checked;
            brush_checkBox.CheckState = (brush == null) ? CheckState.Indeterminate : CheckState.Checked;
            if (shapes.All(i => i.FlipX == first.FlipX))
                flipX_checkBox.Checked = first.FlipX;
            else
                flipX_checkBox.CheckState = CheckState.Indeterminate;
            if (shapes.All(i => i.FlipY == first.FlipY))
                flipY_checkBox.Checked = first.FlipY;
            else
                flipY_checkBox.CheckState = CheckState.Indeterminate;
        }

        private bool IsValidPoint(string str)
        {
            if(string.IsNullOrEmpty(str))
                return true;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            return int.TryParse(arr[1], out _) && int.TryParse(arr[0], out _);
        }

        private void NumericTextBoxFocusLeave(object? sender, EventArgs e)
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
            //Clipboard.SetData()
            PenDialog dialog;
            if (pen != null)
                dialog = new PenDialog("Pen setting", pen);
            else
                dialog = new PenDialog("Pen setting");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pen = dialog.GetValue();
                pen_pictureBox.Invalidate();
            }
        }

        private void pen_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (pen == null)
            {
                if(pen_checkBox.CheckState == CheckState.Checked)
                    e.Graphics.DrawString("Render error.", new Font("Consolas", 10), new SolidBrush(Color.DarkRed), new Point(8, 15));
                else
                    e.Graphics.DrawString("Cannot render.", new Font("Consolas", 10), new SolidBrush(Color.FromArgb(201, 150, 6)), new Point(8, 15));
                return;
            }
            var bounds = pen_pictureBox.ClientRectangle;
            var g = e.Graphics;
            
            pen_pictureBox.BackColor = Color.FromArgb(pen.Color.ToArgb() ^ 0xffffff);
            g.DrawLine(pen, new Point((int)pen.Width / 2, bounds.Height / 2), new Point(bounds.Width - ((int)pen.Width / 2), bounds.Height / 2));
        }

        private void pick_button_Click(object sender, EventArgs e)
        {
            BrushDialog dialog;
            if (brush != null)
            {
                if (brush is SolidBrush solid)
                    dialog = new BrushDialog("Brush setting", solid);
                else if (brush is HatchBrush hatch)
                    dialog = new BrushDialog("Brush setting", hatch);
                else if (brush is TextureBrush texture)
                    dialog = new BrushDialog("Brush setting", texture);
                else return;
            }
            else
                dialog = new BrushDialog("Brush setting"); 
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                brush = dialog.GetValue();
                fill_pictureBox.Invalidate();
            }
        }

        private void fill_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (brush == null)
            {
                if (brush_checkBox.CheckState == CheckState.Checked)
                    e.Graphics.DrawString("Render error.", new Font("Consolas", 10), new SolidBrush(Color.DarkRed), new Point(8, 15));
                else
                    e.Graphics.DrawString("Cannot render.", new Font("Consolas", 10), new SolidBrush(Color.FromArgb(201, 150, 6)), new Point(8, 15));
                return;
            }
            e.Graphics.FillRectangle(brush, fill_pictureBox.ClientRectangle);
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (pen_checkBox.CheckState == CheckState.Checked && pen == null)
            {
                MessageBox.Show("Blank Pen field.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (brush_checkBox.CheckState == CheckState.Checked && brush == null)
            {
                MessageBox.Show("Blank Brush field.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void pen_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            pen_pictureBox.Enabled = pen_checkBox.Checked;
            pickPen_button.Enabled = pen_checkBox.Checked;
            pen_pictureBox.Invalidate();
        }

        private void brush_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            fill_pictureBox.Enabled = brush_checkBox.Checked;
            brushPick_button.Enabled = brush_checkBox.Checked;
            fill_pictureBox.Invalidate();
        }
    }
}
