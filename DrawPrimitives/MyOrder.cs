using DrawPrimitives.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public class MyOrder
    {
        public Shape[]? Shapes { get; set; }
        public Canvas? Canvas { get; set; }

        public MyOrder() { }

        [JsonConstructor]
        public MyOrder(Shape[] shapes, Canvas canvas)
        {
            Shapes = shapes;
            Canvas = canvas;
        }

        public MyOrder(Canvas canvas, Shape[] shapes)
        {
            Shapes = shapes;
            Canvas = canvas;
        }
    }
}
