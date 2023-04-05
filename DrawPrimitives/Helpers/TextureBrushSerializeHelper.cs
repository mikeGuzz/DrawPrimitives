using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.Helpers
{
    public sealed class TextureBrushSerializeHelper : BrushSerializeHelper
    {
        public string? ImagePath { get; set; }
        public WrapMode WrapMode { get; set; }
        public Matrix? Transform { get; set; }
        public bool Stretch { get; set; }
        public Size Offset { get; set; }

        public TextureBrushSerializeHelper() : base() { }

        public TextureBrushSerializeHelper(TextureBrushHolder holder) : base()
        {
            ImagePath = holder.Path;
            var brush = holder.Brush;
            WrapMode = brush.WrapMode;
            Transform = brush.Transform;
            Stretch = holder.Stretch;
            Offset = holder.Offset;
        }

        public override BrushHolder GetBrushHolder()
        {
            if (!File.Exists(ImagePath))
            {
                var bounds = new Rectangle(0, 0, 256, 256);
                using(var map = new Bitmap(bounds.Width, bounds.Height))
                {
                    using(var g = Graphics.FromImage(map))
                    {
                        var format = new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                        };
                        g.DrawString("file was not found.", new Font("Consolas", 24), Brushes.DarkRed, bounds, format);
                        var ob = new TextureBrushHolder(new TextureBrush(map), ImagePath != null ? ImagePath : string.Empty);
                        ob.Stretch = true;
                        return ob;
                    }
                }
            }
            var b = new TextureBrush(new Bitmap(ImagePath), WrapMode);
            if (Transform != null)
                b.Transform = Transform;
            var holder = new TextureBrushHolder(b, ImagePath);
            holder.Stretch = Stretch;
            holder.Offset = Offset;
            return holder;
        }
    }
}
