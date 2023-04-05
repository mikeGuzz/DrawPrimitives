using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.My
{
    public class SolidBrushHolder : BrushHolder
    {
        public SolidBrush Brush { get; set; }

        public SolidBrushHolder(SolidBrush brush) : base()
        {
            Brush = brush;
        }

        public override string ToString()
        {
            return $"Type: Solid color\nColor: {Brush.Color.Name}";
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            var b = (SolidBrushHolder)obj;
            return b.Brush.Color == Brush.Color;
        }

        public override int GetHashCode()
        {
            return Brush.Color.GetHashCode();
        }

        public override Brush GetBrush(Rectangle bounds)
        {
            return GetBrush();
        }

        public override Brush GetBrush()
        {
            return Brush;
        }

        public override void Dispose()
        {
            Brush.Dispose();
        }

        public override object Clone()
        {
            return new SolidBrushHolder((SolidBrush)Brush.Clone());
        }

        public static bool operator ==(SolidBrushHolder a, SolidBrushHolder b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(SolidBrushHolder a, SolidBrushHolder b)
        {
            return !(a == b);
        }
    }
}
