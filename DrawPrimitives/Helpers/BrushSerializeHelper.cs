using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.Helpers
{
    [XmlInclude(typeof(SolidBrushSerializeHelper))]
    [XmlInclude(typeof(HatchBrushSerializeHelper))]
    public abstract class BrushSerializeHelper
    {
        public BrushSerializeHelper() { }

        public abstract Brush ToBrush();
    }
}
