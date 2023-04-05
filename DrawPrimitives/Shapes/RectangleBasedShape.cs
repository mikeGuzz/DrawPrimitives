using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DrawPrimitives.My;

namespace DrawPrimitives.Shapes
{
    public abstract class RectangleBasedShape : Shape
    {
        protected Rectangle bounds;

        public Rectangle Bounds
        {
            get => bounds;
            set => bounds = value;
        }

        public RectangleBasedShape() : base() { }

        public RectangleBasedShape(Rectangle bounds) : base()
        {
            Bound(bounds);
        }

        public RectangleBasedShape(Pen pen, BrushHolder brush) : base(pen, brush) { }

        public RectangleBasedShape(Rectangle bounds, Pen pen, BrushHolder brush) : base(pen, brush)
        {
            Bound(bounds);
        }

        public override Rectangle GetBounds()
        {
            return bounds;
        }

        public override void Bound(Rectangle r)
        {
            SetPosition(r.Location);
            SetSize(r.Size);
        }

        public override void SetPosition(Point p)
        {
            bounds.Location = p;
        }

        public override void SetSize(Size s)
        {
            bounds.Size = new Size(Math.Abs(s.Width) < MinimumSize.Width ? MinimumSize.Width : s.Width,
                Math.Abs(s.Height) < MinimumSize.Height ? MinimumSize.Height : s.Height);
        }
    }
}
