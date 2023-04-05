using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawPrimitives.My;

namespace DrawPrimitives.Helpers
{
    public sealed class HatchBrushSerializeHelper : BrushSerializeHelper
    {
        public HatchStyle HatchStyle { get; set; }
        public int ArgBackColor { get; set; }
        public int ArgForeColor { get; set; }

        public HatchBrushSerializeHelper() : base() { }

        public HatchBrushSerializeHelper(HatchBrushHolder holder) : base()
        {
            var brush = (HatchBrush)holder.Brush;
            HatchStyle = brush.HatchStyle;
            ArgBackColor = brush.BackgroundColor.ToArgb();
            ArgForeColor = brush.ForegroundColor.ToArgb();
        }

        public override BrushHolder GetBrushHolder()
        {
            return new HatchBrushHolder(new HatchBrush(HatchStyle, Color.FromArgb(ArgForeColor), Color.FromArgb(ArgBackColor)));
        }
    }
}
