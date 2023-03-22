using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public class RectangleShape : RectangleBasedShape
    {
        public RectangleShape() : base() { }

        public RectangleShape(RectangleBasedShape ob) : base(ob) { }

        public RectangleShape(Rectangle bounds) : base(bounds) { }

        public RectangleShape(Pen? pen, Brush? brush) : base(pen, brush) { }

        public RectangleShape(Rectangle bounds, Pen? pen, Brush? brush) : base(bounds, pen, brush) { }

        public override void DrawStroke(Graphics g)
        {
            if (Pen != null)
                g.DrawRectangle(Pen, bounds);
        }

        public override void DrawFill(Graphics g)
        {
            if (Brush != null)
                g.FillRectangle(Brush, bounds);
        }
    }
}
