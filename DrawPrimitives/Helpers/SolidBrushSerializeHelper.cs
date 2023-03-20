using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Helpers
{
    public sealed class SolidBrushSerializeHelper : BrushSerializeHelper
    {
        public int ArgColor { get; set; }

        public SolidBrushSerializeHelper() : base() { }

        public SolidBrushSerializeHelper(SolidBrush brush) : base()
        {
            ArgColor = brush.Color.ToArgb();
        }

        public override Brush ToBrush()
        {
            return new SolidBrush(Color.FromArgb(ArgColor));
        }
    }
}
