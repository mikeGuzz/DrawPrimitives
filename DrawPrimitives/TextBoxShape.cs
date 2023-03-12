using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public class TextBoxShape : Shape
    {
        public string Text { get; set; }
        public Font Font { get; set; } = SystemFonts.DefaultFont;

        public TextBoxShape() : base()
        {
            Text = string.Empty;
        }

        public TextBoxShape(Brush? brush, string text) : base()
        {
            Brush = brush;
            Text = text;
        }

        public TextBoxShape(Rectangle bounds, Brush? brush, string text) : base()
        {
            Bounds = bounds;
            Brush = brush;
            Text = text;
        }

        public TextBoxShape(Rectangle bounds, Brush? brush, string text, Font font) : base()
        {
            Bounds = bounds;
            Brush = brush;
            Text = text;
            Font = font;
        }

        public override void DrawFill(Graphics g)
        {
            if (Brush == null)
                return;
            g.DrawString(Text, Font, Brush, Bounds);
        }

        public override void DrawStroke(Graphics g)
        {
            DrawFill(g);
        }
    }
}
