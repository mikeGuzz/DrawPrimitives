using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public class LineShape : Shape
    {
        public LineShape() : base() { }

        public LineShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
        }

        public LineShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public LineShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
        {
            Bounds = bounds;
            Pen = pen;
            Brush = brush;
        }

        public override void DrawFill(Graphics g)
        {
            DrawStroke(g);
        }

        public override void DrawStroke(Graphics g)
        {
            if (Pen != null)
            {
                var rBounds = GetNormalizedBounds();
                g.DrawLine(Pen, rBounds.Location, new Point(rBounds.Width, rBounds.Height));
            }
                
        }
    }
}
