using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.My
{
    public sealed class HatchBrushHolder : BrushHolder
    {
        public HatchBrush Brush { get; set; }

        public HatchBrushHolder(HatchBrush brush) : base()
        {
            Brush = brush;
        }

        public override string ToString()
        {
            return $"Type: Hatch\nStyle: {Brush.HatchStyle}\nBack color: {Brush.BackgroundColor.Name}\nFore color: {Brush.ForegroundColor.Name}";
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            var b1 = ((HatchBrushHolder)obj).Brush;
            return b1.BackgroundColor == Brush.BackgroundColor 
                && b1.ForegroundColor == Brush.ForegroundColor
                && b1.HatchStyle == Brush.HatchStyle;
        }

        public override int GetHashCode()
        {
            var hash = Brush.ForegroundColor.GetHashCode();
            hash ^= Brush.BackgroundColor.GetHashCode();
            hash ^= Brush.HatchStyle.GetHashCode();
            return hash;
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
            return new HatchBrushHolder((HatchBrush)Brush.Clone());
        }

        public static bool operator ==(HatchBrushHolder a, HatchBrushHolder? b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(HatchBrushHolder a, HatchBrushHolder? b)
        {
            return !(a == b);
        }
    }
}
