using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public class EllipseShape : Shape
    {
        public EllipseShape() : base() { }

        public EllipseShape(Shape ob) : base(ob) { }

        public EllipseShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
        }

        public EllipseShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public EllipseShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
        {
            Bounds = bounds;
            Pen = pen;
            Brush = brush;
        }

        public override void DrawStroke(Graphics g)
        {
            if (Pen != null)
                g.DrawEllipse(Pen, Bounds);
        }

        public override void DrawFill(Graphics g)
        {
            if (Brush != null)
                g.FillEllipse(Brush, Bounds);
        }
    }
}
