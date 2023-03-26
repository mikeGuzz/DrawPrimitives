using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public abstract class RectangleBasedShape : Shape
    {
        protected Rectangle bounds;

        public RectangleBasedShape() : base() { }

        public RectangleBasedShape(Rectangle bounds) : base()
        {
            this.bounds = bounds;
        }

        public RectangleBasedShape(Pen? pen, Brush? brush) : base(pen, brush) { }

        public RectangleBasedShape(Rectangle bounds, Pen? pen, Brush? brush) : base(pen, brush)
        {
            this.bounds = bounds;
        }

        public RectangleBasedShape(RectangleBasedShape ob) : base(ob)
        {
            bounds = ob.bounds;
        }

        public override Rectangle GetBounds()
        {
            return bounds;
        }

        public override void Bound(int x, int y, int w, int h)
        {
            Bound(new Rectangle(x, y, w, h));
        }

        public override void Bound(Rectangle r)
        {
            bounds = r;
        }

        public override void SetPosition(Point p)
        {
            bounds.Location = p;
        }

        public override void SetPosition(int x, int y)
        {
            SetPosition(new Point(x, y));
        }

        public override void SetSize(Size s)
        {
            bounds.Size = s;
        }

        public override void SetSize(int w, int h)
        {
            SetSize(new Size(w, h));
        }
    }
}
