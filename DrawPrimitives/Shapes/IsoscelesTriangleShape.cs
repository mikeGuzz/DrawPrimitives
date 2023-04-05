using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class IsoscelesTriangleShape : BoundedPolygonShapeBase
    {
        public IsoscelesTriangleShape() : base() { }

        public IsoscelesTriangleShape(Rectangle bounds) : base(bounds) { }

        public IsoscelesTriangleShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public IsoscelesTriangleShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

        public override Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top),
                new Point(Bounds.Left, Bounds.Bottom),
                new Point(Bounds.Right, Bounds.Bottom),
            };
        }

        public override object Clone()
        {
            var tmp = new IsoscelesTriangleShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
