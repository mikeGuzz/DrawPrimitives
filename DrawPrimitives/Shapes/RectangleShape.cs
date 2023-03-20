using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape() : base() { }

        public RectangleShape(RectangleShape ob) : base(ob) { }

        public RectangleShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Pen = DefaultPen;
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
            base.DrawStroke(g);
            if (Pen != null)
                g.DrawRectangle(Pen, GetWithoutNegative());
        }

        public override void DrawFill(Graphics g)
        {
            base.DrawFill(g);
            if (Brush != null)
                g.FillRectangle(Brush, GetWithoutNegative());
        }
    }
}
