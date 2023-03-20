using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class DiamondShape : PolygonShape
    {
        public override Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top),
                new Point(Bounds.Left, Bounds.Top +  (Bounds.Height / 2)), 
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Bottom), 
                new Point(Bounds.Right, Bounds.Top + (Bounds.Height / 2)),
            };
        }

        public DiamondShape() : base() { }

        public DiamondShape(DiamondShape ob) : base(ob) { }

        public DiamondShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Pen = DefaultPen;
        }

        public DiamondShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public DiamondShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
        {
            Bounds = bounds;
            Pen = pen;
            Brush = brush;
        }

        public override void DrawStroke(Graphics g)
        {
            base.DrawStroke(g);
            if (Pen != null)
                g.DrawPolygon(Pen, GetPoints());
        }

        public override void DrawFill(Graphics g)
        {
            base.DrawFill(g);
            if (Brush != null)
                g.FillPolygon(Brush, GetPoints());
        }
    }
}
