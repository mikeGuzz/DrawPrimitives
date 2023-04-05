using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public abstract class BoundedPolygonShapeBase : RectangleBasedShape
    {
        public BoundedPolygonShapeBase() : base() { }

        public BoundedPolygonShapeBase(Rectangle bounds) : base()
        {
            Bound(bounds);
        }

        public BoundedPolygonShapeBase(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public BoundedPolygonShapeBase(Rectangle bounds, Pen pen, BrushHolder brush) : base(pen, brush)
        {
            Bound(bounds);
        }

        public abstract Point[] GetPoints();

        protected override void Draw(Graphics g, Rectangle rect)
        {
            if (rect.IsEmpty)
                return;
            rect = rect.WithoutNegative();
            var points = GetPoints();
            if (UseBrush)
            {
                g.FillPolygon(BrushHolder.GetBrush(rect), points);
            }
            DrawString(g, rect);
            if (UsePen)
            {
                g.DrawPolygon(Pen, points);
            }
        }
    }
}
