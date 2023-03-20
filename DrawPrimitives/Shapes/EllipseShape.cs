using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.Shapes
{
    public class EllipseShape : Shape
    {
        public EllipseShape() : base() { }

        public EllipseShape(EllipseShape ob) : base(ob) { }

        public EllipseShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Pen = DefaultPen;
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
            base.DrawStroke(g);
            if(Pen != null)
                g.DrawEllipse(Pen, Bounds);
        }

        public override void DrawFill(Graphics g)
        {
            base.DrawFill(g);
            if(Brush != null)
                g.FillEllipse(Brush, Bounds);
        }
    }
}
