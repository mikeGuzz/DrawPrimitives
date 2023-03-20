using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class HexagonShape : PolygonShape
    {
        public override Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top),
                new Point(Bounds.Left, (int)(Bounds.Top + (Bounds.Height * 0.25))),
                new Point(Bounds.Left, (int)(Bounds.Top + (Bounds.Height * 0.75))),
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top + Bounds.Height),
                new Point(Bounds.Left + Bounds.Width, (int)(Bounds.Top +(Bounds.Height * 0.75))),
                new Point(Bounds.Left + Bounds.Width, (int)(Bounds.Top +(Bounds.Height * 0.25))),
            };
        }

        public HexagonShape() : base() { }

        public HexagonShape(HexagonShape ob) : base(ob) { }

        public HexagonShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Pen = DefaultPen;
        }

        public HexagonShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public HexagonShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
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
