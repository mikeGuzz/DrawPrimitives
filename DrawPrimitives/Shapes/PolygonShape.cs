using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public abstract class PolygonShape : Shape
    {
        public PolygonShape() { }

        public PolygonShape(PolygonShape shape) : base(shape) { }

        public abstract Point[] GetPoints();
    }
}
