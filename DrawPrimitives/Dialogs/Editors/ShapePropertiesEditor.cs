using DrawPrimitives.My;
using DrawPrimitives.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class ShapePropertiesEditor : Form
    {
        private const string MultiEditText = "In multiple edit mode.";
        private readonly Size canvasSize;

        private Rectangle? transform;
        private Pen? outline;
        private BrushHolder? fill;
        private TextFormat? textFormat;

        public Rectangle? Transform
        {
            get => transform;
            set
            {
                transform = value;
                if (!transform.HasValue)
                {
                    transform_label.Text = MultiEditText;
                    return;
                }  
                var ob = transform.Value;
                transform_label.Text = $"X: {ob.X}; Y: {ob.Y}\nW: {ob.Width}; H: {ob.Height}";
            }
        }

        public Pen? Outline
        {
            get => outline;
            set
            {
                outline = value;
                if (value == null)
                {
                    outline_label.Text = MultiEditText;
                    return;
                }
                outline_label.Text = $"Color: {value.Color.Name}\nWidth: {value.Width}\nAlignment: {value.Alignment}\nDash cap: {value.DashCap}\nDash style: {value.DashStyle}";
            }
        }

        public BrushHolder? Fill
        {
            get => fill;
            set
            {
                fill = value;
                if (value == null)
                {
                    fill_label.Text = MultiEditText;
                    return;
                }
                fill_label.Text = value.ToString();
            }
        }

        public TextFormat? TextFormat
        {
            get => textFormat; 
            set
            {
                textFormat = value;
                if (value == null)
                {
                    text_label.Text = MultiEditText;
                    return;
                }
                text_textBox.Text = value.Text;
                text_label.Text = value.ToString();
            }
        }

        public bool? DrawOutline
        {
            get => outline_checkBox.CheckState == CheckState.Indeterminate ? null : (outline_checkBox.CheckState == CheckState.Checked);
            set
            {
                var state = value == null ? CheckState.Indeterminate : ((bool)value ? CheckState.Checked : CheckState.Unchecked);
                outline_checkBox.CheckState = state;
                outline_groupBox.Enabled = state == CheckState.Checked;
            }
        }

        public bool? DrawFill
        {
            get => fill_checkBox.CheckState == CheckState.Indeterminate ? null : (fill_checkBox.CheckState == CheckState.Checked);
            set
            {
                var state = value == null ? CheckState.Indeterminate : ((bool)value ? CheckState.Checked : CheckState.Unchecked);
                fill_checkBox.CheckState = state;
                fill_groupBox.Enabled = state == CheckState.Checked;
            }
        }

        public bool? DrawText
        {
            get => text_checkBox.CheckState == CheckState.Indeterminate ? null : (text_checkBox.CheckState == CheckState.Checked);
            set
            {
                var state = value == null ? CheckState.Indeterminate : ((bool)value ? CheckState.Checked : CheckState.Unchecked);
                text_checkBox.CheckState = state;
                text_groupBox.Enabled = state == CheckState.Checked;
            }
        }

        public bool? FlipX
        {
            get => flipX_checkBox.CheckState == CheckState.Indeterminate ? null 
                : (flipX_checkBox.CheckState == CheckState.Checked);
            set => flipX_checkBox.CheckState = value == null ? CheckState.Indeterminate 
                : ((bool)value ? CheckState.Checked : CheckState.Unchecked);
        }

        public bool? FlipY
        {
            get => flipY_checkBox.CheckState == CheckState.Indeterminate ? null 
                : (flipY_checkBox.CheckState == CheckState.Checked);
            set => flipY_checkBox.CheckState = value == null ? CheckState.Indeterminate
                : ((bool)value ? CheckState.Checked : CheckState.Unchecked);
        }

        public SmoothingMode? SmoothingMode
        {
            get => smoothMode_checkBox.CheckState == CheckState.Indeterminate ? null
                : (smoothMode_checkBox.CheckState == CheckState.Checked ? System.Drawing.Drawing2D.SmoothingMode.HighQuality 
                : System.Drawing.Drawing2D.SmoothingMode.HighSpeed);
            set => smoothMode_checkBox.CheckState = value == null ? CheckState.Indeterminate
                : ((SmoothingMode)value == System.Drawing.Drawing2D.SmoothingMode.HighQuality ? CheckState.Checked 
                : ((SmoothingMode)value == System.Drawing.Drawing2D.SmoothingMode.HighSpeed ? CheckState.Unchecked 
                : CheckState.Indeterminate));
        }

        public Shape Shape
        {
            set
            {
                Transform = value.GetBounds();
                Outline = value.Pen;
                Fill = value.BrushHolder;
                TextFormat = value.TextFormat;
                DrawOutline = value.UsePen; 
                DrawFill = value.UseBrush;
                DrawText = value.UseText;
                FlipX = value.FlipX;
                FlipY = value.FlipY;
                SmoothingMode = value.SmoothingMode;
            }
        }

        public IEnumerable<Shape> ShapeCollection
        {
            set
            {
                if (!value.Any())
                    return;
                if(value.Count() == 1)
                {
                    Shape = value.Single();
                    return;
                }

                var first = value.First();
                var firstBounds = first.GetBounds();
                value = value.TakeLast(value.Count() - 1);

                Transform = value.All(i => i.GetBounds() == firstBounds) ? firstBounds : null;
                Outline = value.All(i => i.Pen.EqualsPen(first.Pen)) ? first.Pen : null;
                Fill = value.All(i => i.BrushHolder.Equals(first.BrushHolder)) ? first.BrushHolder : null;
                TextFormat = value.All(i => i.TextFormat == first.TextFormat) ? first.TextFormat : null;
                
                DrawOutline = (value.All(i => i.UsePen == first.UsePen) ? first.UsePen : null);
                DrawFill = (value.All(i => i.UseBrush == first.UseBrush) ? first.UseBrush : null);
                DrawText = (value.All(i => i.UseText == first.UseText) ? first.UseText : null);

                FlipX = (value.All(i => i.FlipX == first.FlipX) ? first.FlipX : null);
                FlipY = (value.All(i => i.FlipY == first.FlipY) ? first.FlipY : null);

                SmoothingMode = (value.All(i => i.SmoothingMode == first.SmoothingMode) ? first.SmoothingMode : null);
            }
        }

        public ShapePropertiesEditor(Size canvSize, Shape value)
        {
            InitializeComponent();

            canvasSize = canvSize;
            Shape = value;
        }

        public ShapePropertiesEditor(Size canvSize, IEnumerable<Shape> coll)
        {
            if (!coll.Any())
                throw new ArgumentException();
            InitializeComponent();

            canvasSize = canvSize;
            ShapeCollection = coll;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void transform_button_Click(object sender, EventArgs e)
        {
            TransformEditor dialog;
            if(transform.HasValue)
                dialog = new TransformEditor(canvasSize, transform.Value);
            else
                dialog = new TransformEditor(canvasSize);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Transform = dialog._Bounds;
            }
        }

        private void text_button_Click(object sender, EventArgs e)
        {
            TextEditor dialog;
            if (textFormat != null)
                dialog = new TextEditor(textFormat);
            else
                dialog = new TextEditor(Shape.DefaultTextFormat);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TextFormat = dialog.TextFormat;
            }
        }

        private void outline_button_Click(object sender, EventArgs e)
        {
            PenPropertiesEditor dialog;
            if (outline != null)
                dialog = new PenPropertiesEditor(outline);
            else
                dialog = new PenPropertiesEditor(Shape.DefaultPen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Outline = dialog.Pen;
            }
        }

        private void fill_button_Click(object sender, EventArgs e)
        {
            BrushPropertiesEditor dialog;
            if (fill != null)
                dialog = new BrushPropertiesEditor(fill);
            else
                dialog = new BrushPropertiesEditor(Shape.DefaultBrushHolder);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Fill = dialog.BrushHolder;
            }
        }

        private void text_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            text_groupBox.Enabled = text_checkBox.CheckState == CheckState.Checked;
        }

        private void outline_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            outline_groupBox.Enabled = outline_checkBox.CheckState == CheckState.Checked;
        }

        private void fill_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            fill_groupBox.Enabled = fill_checkBox.CheckState == CheckState.Checked;
        }
    }
}
