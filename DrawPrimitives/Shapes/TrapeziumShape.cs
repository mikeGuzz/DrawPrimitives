using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class TrapeziumShape : BoundedPolygonShapeBase
    {
        public TrapeziumShape() : base() { }

        public TrapeziumShape(Rectangle bounds) : base(bounds) { }

        public TrapeziumShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public TrapeziumShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

        public override Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(Bounds.Left + (int)(Bounds.Width * 0.25f), Bounds.Top),
                new Point(Bounds.Left + (int)(Bounds.Width * 0.75f), Bounds.Top),
                new Point(Bounds.Right, Bounds.Bottom),
                new Point(Bounds.Left, Bounds.Bottom),
            };
        }

        public override object Clone()
        {
            var tmp = new TrapeziumShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
