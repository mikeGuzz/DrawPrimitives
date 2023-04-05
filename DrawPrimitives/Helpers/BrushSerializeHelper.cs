using DrawPrimitives.My;
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
    [XmlInclude(typeof(TextureBrushSerializeHelper))]
    public abstract class BrushSerializeHelper
    {
        protected BrushSerializeHelper() { }

        public abstract BrushHolder GetBrushHolder();
    }
}
