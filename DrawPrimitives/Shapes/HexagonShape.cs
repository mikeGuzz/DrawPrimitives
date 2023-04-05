using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class HexagonShape : BoundedPolygonShapeBase
    {
        public HexagonShape() : base() { }

        public HexagonShape(Rectangle bounds) : base(bounds) { }

        public HexagonShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public HexagonShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

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

        public override object Clone()
        {
            var tmp = new HexagonShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
