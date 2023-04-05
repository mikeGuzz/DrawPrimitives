using DrawPrimitives.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.My
{
    public class MyOrder
    {
        public Shape[]? Shapes { get; set; }
        public Canvas? Canvas { get; set; }

        public MyOrder() { }

        public MyOrder(Shape[] shapes, Canvas canvas)
        {
            Shapes = shapes;
            Canvas = canvas;
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(null, obj))
                return false;
            if(ReferenceEquals(this, obj))
                return true;
            if (obj is not MyOrder)
                return false;
            var b = (MyOrder)obj;
            var excFlag = Shapes != null ? (b.Shapes != null ? !Shapes.Except(b.Shapes).Any() : false) : false;
            return Canvas == b.Canvas
                && excFlag;
        }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            if(Canvas != null)
                hash ^= Canvas.GetHashCode();
            if(Shapes != null) 
                hash ^= Shapes.GetHashCode();
            return hash;
        }

        public static bool operator ==(MyOrder? left, MyOrder? right)
        {
            if (ReferenceEquals(null, left))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(MyOrder? left, MyOrder? right)
        {
            return !(left == right);
        }
    }
}