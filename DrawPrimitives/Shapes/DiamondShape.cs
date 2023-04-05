using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    
    public class DiamondShape : BoundedPolygonShapeBase
    {
        public DiamondShape() : base() { }

        public DiamondShape(Rectangle bounds) : base(bounds) { }

        public DiamondShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public DiamondShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

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

        public override object Clone()
        {
            var tmp = new DiamondShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
