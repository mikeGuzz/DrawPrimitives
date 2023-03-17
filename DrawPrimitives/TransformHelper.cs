using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace DrawPrimitives
{
    public class TransformHelper : IList<Shape>
    {
        private List<Rectangle> startBounds = new List<Rectangle>();
        private List<Shape> shapes = new List<Shape>();

        public Rectangle StartTransformBounds { get; private set; }

        public Rectangle CurrentBounds
        {
            get
            {
                if (shapes.Count == 1)
                    return shapes.Single().Bounds;
                else if (!shapes.Any())
                    return Rectangle.Empty;
                else
                    return Rectangle.FromLTRB(shapes.Min(i => i.Bounds.Left), shapes.Min(i => i.Bounds.Top), shapes.Max(i => i.Bounds.Right), shapes.Max(i => i.Bounds.Bottom));
            }
        }

        public int Count => shapes.Count;

        public bool IsReadOnly => false;

        public Shape this[int index]
        {
            get => shapes[index];
            set => shapes[index] = value;
        }

        public TransformHelper(params Shape[] shapes)
        {
            this.shapes.AddRange(shapes);
        }

        public void StartTransform()
        {
            startBounds.Clear();
            startBounds.AddRange(shapes.Select(i => i.Bounds));
            StartTransformBounds = CurrentBounds;
        }

        public void DrawBounds(Graphics g, Pen pen, Brush? brush)
        {
            if (!shapes.Any())
                return;
            if (shapes.Count == 1)
            {
                shapes.Single().DrawBounds(g, pen, brush);
                return;
            }
            var r = CurrentBounds.WithoutNegative();
            if (brush != null)
                g.FillRectangle(brush, r);
            g.DrawRectangle(pen, r);
        }

        public bool IsHit(Point p)
        {
            if (!shapes.Any())
                throw new InvalidOperationException();
            if(shapes.Count == 1)
                return shapes.Single().IsHit(p);
            else
            {
                var rBounds = CurrentBounds.WithoutNegative();
                return p.X >= rBounds.X && p.Y >= rBounds.Y &&
                        p.X <= (rBounds.X + rBounds.Width) && p.Y <= (rBounds.Y + rBounds.Height);
            }
        }

        public void Bound(Rectangle newBounds)
        {
            if (startBounds.Count != shapes.Count)
                StartTransform();
            var pW = newBounds.Width / (double)StartTransformBounds.Width;
            var pH = newBounds.Height / (double)StartTransformBounds.Height;
            for (int i = 0; i < startBounds.Count; i++)
            {
                var dX = (double)startBounds[i].X - StartTransformBounds.X;
                var dY = (double)startBounds[i].Y - StartTransformBounds.Y;
                shapes[i].Bounds.Size = new Size((int)(startBounds[i].Width * pW), (int)(startBounds[i].Height * pH));
                shapes[i].Bounds.Location = new Point((int)(newBounds.X + (dX * pW)), (int)(newBounds.Y + (dY * pH)));
            }
        }

        public void TransformPosition(Point n)
        {
            if (startBounds.Count != shapes.Count)
                StartTransform();
            for (int i = 0; i < startBounds.Count; i++)
            {
                shapes[i].Bounds.Location = new Point(startBounds[i].X + n.X, startBounds[i].Y + n.Y);
            }
        }

        public void TransformSize(Size n)
        {
            if (startBounds.Count != shapes.Count)
                StartTransform();
            for (int i = 0; i < startBounds.Count; i++)
            {
                shapes[i].Bounds.Size = startBounds[i].Size + n;
            }
        }

        public int IndexOf(Shape item)
        {
            return shapes.IndexOf(item);
        }

        public void Insert(int index, Shape item)
        {
            shapes.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            shapes.RemoveAt(index);
        }

        public void CopyTo(Shape[] array, int arrayIndex)
        {
            shapes.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Shape> GetEnumerator()
        {
            return (IEnumerator<Shape>)new TransformHelperEnumerator(shapes.ToArray());
        }

        public void Add(Shape item)
        {
            shapes.Add(item);
        }

        public void AddRange(IEnumerable<Shape> collection)
        {
            shapes.AddRange(collection);
        }

        public void Clear()
        {
            shapes.Clear();
        }

        public bool Contains(Shape item)
        {
            return shapes.Contains(item);
        }

        public bool Remove(Shape item)
        {
            return shapes.Remove(item);
        }
    }

    public class TransformHelperEnumerator : IEnumerator<Shape>
    {
        private Shape[] arr;
        private int index = -1;

        public TransformHelperEnumerator(Shape[] collection)
        {
            arr = collection;
        }

        object IEnumerator.Current => Current;

        public Shape Current
        {
            get
            {
                try
                {
                    return arr[index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        { 
            return ++index < arr.Length;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose() { }
    }
}
