using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Helpers
{
    public sealed class PenSerializeHelper
    {
        public float Width { get; set; }
        public int ArgbColor { get; set; }
        public PenAlignment Alignment { get; set; }
        public DashCap DashCap { get; set; }
        public float DashOffset { get; set; }
        public float[]? DashPattern { get; set; }
        public DashStyle DashStyle { get; set; }
        public LineCap StartCap { get; set; }
        public LineCap EndCap { get; set; }
        public LineJoin LineJoin { get; set; }
        public float MiterLimit { get; set; }
        public Matrix? Transform { get; set; }
        public PenType PenType { get; set; }

        public PenSerializeHelper() { }

        public PenSerializeHelper(Pen? p)
        {
            if (p == null)
                return;
            ArgbColor = p.Color.ToArgb();
            Width = p.Width;
            Alignment = p.Alignment;
            DashCap = p.DashCap;
            DashOffset = p.DashOffset;
            if (p.DashStyle == DashStyle.Custom)
                DashPattern = p.DashPattern;
            DashStyle = p.DashStyle;
            StartCap = p.StartCap;
            EndCap = p.EndCap;
            LineJoin = p.LineJoin;
            MiterLimit = p.MiterLimit;
            Transform = p.Transform;
        }

        public Pen ToPen()
        {
            var pen = new Pen(Color.FromArgb(ArgbColor), Width);
            pen.DashCap = DashCap;
            pen.DashOffset = DashOffset;
            if (DashPattern != null)
                pen.DashPattern = DashPattern;
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;
            pen.Alignment = Alignment;
            pen.LineJoin = LineJoin;
            pen.DashStyle = DashStyle;
            pen.MiterLimit = MiterLimit;
            if (Transform != null)
                pen.Transform = Transform;
            return pen;
        }
    }
}
