using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class CubeShape : BoundedPolygonShapeBase
    {
        private int angle = 45;

        public int Angle
        {
            get => angle;
            set => angle = value < 0 ? 0 : (value > 90 ? 90 : value);
        }

        public CubeShape() : base() { }

        public CubeShape(Rectangle bounds) : base(bounds) { }

        public CubeShape(Rectangle bounds, int angle) : base(bounds)
        {
            Angle = angle;
        }

        public CubeShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public CubeShape(Pen pen, BrushHolder brush, int angle) : base(pen, brush)
        {
            Angle = angle;
        }

        public CubeShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

        public CubeShape(Rectangle bounds, Pen pen, BrushHolder brush, int angle) : base(bounds, pen, brush)
        {
            Angle = angle;
        }

        public override Point[] GetPoints()
        {
            var p = angle / 90d;
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
                double p = Angle / 90d;
                g.DrawLine(Pen, new Point(Bounds.Left + Bounds.Width / 2, (int)(Bounds.Top + Bounds.Height * p)), new Point(Bounds.Right, (int)(Bounds.Top + (Bounds.Height * (p / 2)))));
                g.DrawLine(Pen, new Point(Bounds.Left + Bounds.Width / 2, (int)(Bounds.Top + Bounds.Height * p)), new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top + Bounds.Height));//midle
                g.DrawLine(Pen, new Point(Bounds.Left + Bounds.Width / 2, (int)(Bounds.Top + Bounds.Height * p)), new Point(Bounds.Left, (int)(Bounds.Top + (Bounds.Height * (p / 2)))));
                g.DrawPolygon(Pen, points);
            }
        }

        public override object Clone()
        {
            var tmp = new CubeShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone(), angle);
            tmp.TextFormat = (TextFormat)TextFormat.Clone();
            tmp.UseBrush = UseBrush;
            tmp.UsePen = UsePen;
            tmp.UseText = UseText;
            tmp.FlipX = FlipX;
            tmp.FlipY = FlipY;
            return tmp;
        }
    }
}
