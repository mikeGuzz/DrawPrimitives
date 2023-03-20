using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class TextBoxShape : Shape
    {
        public static Font DefaultFont { get; set; } = SystemFonts.DefaultFont;

        public string Text { get; set; }
        public Font Font { get; set; } = DefaultFont;

        public TextBoxShape() : base()
        {
            Text = string.Empty;
            Brush = DefaultBrush;
        }

        public TextBoxShape(Brush? brush) : base()
        {
            Brush = brush;
            Text = string.Empty;
        }

        public TextBoxShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Text = string.Empty;
        }

        public TextBoxShape(Rectangle bounds, Brush brush) : base()
        {
            Bounds = bounds;
            Brush = brush;
            Text = string.Empty;
        }

        public TextBoxShape(Rectangle bounds, Brush brush, string text) : base()
        {
            Bounds = bounds;
            Brush = brush;
            Text = text;
        }

        public TextBoxShape(Rectangle bounds, string text, Font font) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Text = text;
            Font = font;
        }

        public TextBoxShape(Rectangle bounds, Brush brush, string text, Font font) : base()
        {
            Bounds = bounds;
            Brush = brush;
            Text = text;
            Font = font;
        }

        public override void DrawFill(Graphics g)
        {
            base.DrawFill(g);
            if (Brush != null)
                g.DrawString(Text, Font, Brush, Bounds);
        }

        public override void DrawStroke(Graphics g) { }
    }
}
