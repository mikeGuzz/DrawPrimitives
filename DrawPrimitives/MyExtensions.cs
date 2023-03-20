using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public static class MyExtensions
    {
        public static Rectangle WithoutNegative(this Rectangle r)
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

        public static bool TryParsePoint(this string str, out Point res)
        {
            res = Point.Empty;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            res = new Point(int.Parse(arr[0]), int.Parse(arr[1]));
            return true;
        }

        public static bool TryParseSize(this string str, out Size res)
        {
            res = Size.Empty;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            res = new Size(int.Parse(arr[0]), int.Parse(arr[1]));
            if (res.Width < 0 || res.Height < 0)
                return false;
            return true;
        }
    }
}
