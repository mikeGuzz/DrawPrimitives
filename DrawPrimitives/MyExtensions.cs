using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public static class MyExtensions
    {
        public static Rectangle Normalized(this Rectangle r)
        {
            if (r.Width >= 0 && r.Height >= 0)
                return r;
            else
            {
                var newBounds = r;
                if (r.Width < 0)
                {
                    newBounds.X += r.Width;
                    newBounds.Width -= r.Width * 2;
                }
                if (r.Height < 0)
                {
                    newBounds.Y += r.Height;
                    newBounds.Height -= r.Height * 2;
                }
                return newBounds;
            }
        }

        public static string SplitCamelCase(this string s)
        {
            return Regex.Replace(s, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
