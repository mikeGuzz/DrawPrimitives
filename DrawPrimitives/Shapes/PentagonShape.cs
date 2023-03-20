using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class PentagonShape : PolygonShape
    {
        public override Point[] GetPoints()
        {
            int sizeW = Bounds.Width;
            int sizeH = Bounds.Height;
            int size = (Bounds.Width > Bounds.Height) ? Bounds.Height : Bounds.Width;
            return new Point[]
            {
                new Point((int)(Bounds.Left + (sizeW * 0.25)), Bounds.Top + sizeH),
                new Point((int)(Bounds.Left + (sizeW * 0.75)), Bounds.Top + sizeH),
                new Point(Bounds.Left + sizeW, Bounds.Top + (sizeH / 2)),
                new Point(Bounds.Left + (sizeW / 2), Bounds.Top),
                new Point(Bounds.Left, Bounds.Top + (sizeH / 2)),
            };
        }

        public PentagonShape() : base() { }

        public PentagonShape(PentagonShape ob) : base(ob) { }

        public PentagonShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Pen = DefaultPen;
        }

        public PentagonShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public PentagonShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
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
