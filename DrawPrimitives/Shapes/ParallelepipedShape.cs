using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class ParallelepipedShape : PolygonShape
    {
        public int Angle { get; set; } = 45;

        public override Point[] GetPoints()
        {
            var p = Angle / 90d;
            return new Point[]
            {
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top),
                new Point(Bounds.Left, (int)(Bounds.Top + (Bounds.Height * (p / 2)))),
                new Point(Bounds.Left, (int)(Bounds.Top + (Bounds.Height * (1 - p / 2)))),
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top + Bounds.Height),
                new Point(Bounds.Left + Bounds.Width, (int)(Bounds.Top +(Bounds.Height * (1 - p / 2)))),
                new Point(Bounds.Left + Bounds.Width, (int)(Bounds.Top +(Bounds.Height * (p / 2)))),
            };
        }

        public ParallelepipedShape() : base() { }

        public ParallelepipedShape(ParallelepipedShape ob) : base(ob) { }

        public ParallelepipedShape(Rectangle bounds) : base()
        {
            Bounds = bounds;
            Brush = DefaultBrush;
            Pen = DefaultPen;
        }

        public ParallelepipedShape(Pen? pen, Brush? brush) : base()
        {
            Pen = pen;
            Brush = brush;
        }

        public ParallelepipedShape(Rectangle bounds, Pen? pen, Brush? brush) : base()
        {
            Bounds = bounds;
            Pen = pen;
            Brush = brush;
        }

        public override void DrawStroke(Graphics g)
        {
            if (Pen == null)
                return;
            Point centerPoint = new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top + (Bounds.Height / 2));
            g.DrawPolygon(Pen, GetPoints());
            var p = Angle / 90d;
            g.DrawLine(Pen, new Point(Bounds.Left + Bounds.Width / 2, (int)(Bounds.Top + Bounds.Height * p)), new Point(Bounds.Left + Bounds.Width, (int)(Bounds.Top + (Bounds.Height * (p / 2)))));
            g.DrawLine(Pen, centerPoint, new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top + Bounds.Height));//midle
            g.DrawLine(Pen, new Point(Bounds.Left + Bounds.Width / 2, (int)(Bounds.Top + Bounds.Height * p)), new Point(Bounds.Left + Bounds.Width, (int)(Bounds.Top + (Bounds.Height * (p / 2)))));


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
