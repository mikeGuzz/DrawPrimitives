using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Helpers
{
    public sealed class HatchBrushSerializeHelper : BrushSerializeHelper
    {
        public HatchStyle HatchStyle { get; set; }
        public int ArgBackColor { get; set; }
        public int ArgForeColor { get; set; }

        public HatchBrushSerializeHelper() : base() { }

        public HatchBrushSerializeHelper(HatchBrush brush) : base()
        {
            HatchStyle = brush.HatchStyle;
            ArgBackColor = brush.BackgroundColor.ToArgb();
            ArgForeColor = brush.ForegroundColor.ToArgb();
        }

        public override Brush ToBrush()
        {
            return new HatchBrush(HatchStyle, Color.FromArgb(ArgForeColor), Color.FromArgb(ArgBackColor));
        }
    }
}
