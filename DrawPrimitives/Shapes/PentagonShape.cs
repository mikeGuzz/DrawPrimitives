using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class PentagonShape : BoundedPolygonShapeBase
    {
        public PentagonShape() : base() { }

        public PentagonShape(Rectangle bounds) : base(bounds) { }

        public PentagonShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public PentagonShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

        public override Point[] GetPoints()
        {
            int sizeW = Bounds.Width;
            int sizeH = Bounds.Height;
            return new Point[]
            {
                new Point((int)(Bounds.Left + (sizeW * 0.25)), Bounds.Top + sizeH),
                new Point((int)(Bounds.Left + (sizeW * 0.75)), Bounds.Top + sizeH),
                new Point(Bounds.Left + sizeW, Bounds.Top + (sizeH / 2)),
                new Point(Bounds.Left + (sizeW / 2), Bounds.Top),
                new Point(Bounds.Left, Bounds.Top + (sizeH / 2)),
            };
        }

        public override object Clone()
        {
            var tmp = new PentagonShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
