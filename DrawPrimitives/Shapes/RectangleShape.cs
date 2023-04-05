using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DrawPrimitives.My;

namespace DrawPrimitives.Shapes
{
    public class RectangleShape : RectangleBasedShape
    {
        public RectangleShape() : base() { }

        public RectangleShape(Rectangle bounds) : base(bounds) { }

        public RectangleShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public RectangleShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(bounds, pen, brush) { }

        protected override void Draw(Graphics g, Rectangle rect)
        {
            if (rect.IsEmpty)
                return;
            rect = rect.WithoutNegative();
            if (UseBrush)
            {
                g.FillRectangle(BrushHolder.GetBrush(rect), rect);
            }
            DrawString(g, rect);
            if (UsePen)
            {
                g.DrawRectangle(Pen, rect);
            }
        }

        public override object Clone()
        {
            var tmp = new RectangleShape(bounds, (Pen)Pen.Clone(), (BrushHolder)BrushHolder.Clone());
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
