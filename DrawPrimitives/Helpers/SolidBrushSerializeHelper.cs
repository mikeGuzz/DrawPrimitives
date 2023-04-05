using DrawPrimitives.My;
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

        public SolidBrushSerializeHelper(SolidBrushHolder holder) : base()
        {
            ArgColor = ((SolidBrush)holder.Brush).Color.ToArgb();
        }

        public override BrushHolder GetBrushHolder()
        {
            return new SolidBrushHolder(new SolidBrush(Color.FromArgb(ArgColor)));
        }
    }
}
