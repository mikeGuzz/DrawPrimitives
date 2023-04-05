using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.Shapes
{
    public class EllipseShape : RectangleBasedShape
    {
        public EllipseShape() : base() { }

        public EllipseShape(Rectangle bounds) : base(bounds) { }

        public EllipseShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public EllipseShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

        protected override void Draw(Graphics g, Rectangle rect)
        {
            if (rect.IsEmpty)
                return;
            if (UseBrush)
            {
                g.FillEllipse(BrushHolder.GetBrush(rect), rect);
            }
            DrawString(g, rect);
            if (UsePen)
            {
                g.DrawEllipse(Pen, rect);
            }
        }

        public override object Clone()
        {
            var tmp = new EllipseShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
