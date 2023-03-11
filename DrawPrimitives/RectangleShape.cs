using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public class RectangleShape : Shape
    {
        public RectangleShape() : base() { }

        public RectangleShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
        }

        public RectangleShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public RectangleShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
        {
            Bounds = bounds;
            Pen = pen;
            Brush = brush;
        }

        public override void DrawStroke(Graphics g)
        {
            if (Pen != null)
                g.DrawRectangle(Pen, Bounds);

            g.ResetTransform();
        }

        public override void DrawFill(Graphics g)
        {
            if (Brush != null)
                g.FillRectangle(Brush, GetNormalizedBounds());
        }
    }
}
