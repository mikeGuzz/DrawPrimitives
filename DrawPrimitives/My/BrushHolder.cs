using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.My
{
    [XmlInclude(typeof(SolidBrushHolder))]
    [XmlInclude(typeof(HatchBrushHolder))]
    [XmlInclude(typeof(TextureBrushHolder))]
    public abstract class BrushHolder : IDisposable, ICloneable
    {
        protected BrushHolder() { }

        public abstract Brush GetBrush(Rectangle bounds);

        public abstract Brush GetBrush();

        public abstract void Dispose();

        public abstract object Clone();
    }
}
