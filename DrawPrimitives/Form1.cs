using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DrawPrimitives
{
    public enum ToolType { Pointer = 0, DrawShape = 1 };

    public partial class Form1 : Form
    {
        private enum TransformState { None, Move, Rotate, Scale };

        //file
        private string fileName = "Untitled";
        private string fileFullName = string.Empty;

        private bool IsSaved()
        {
            if (!File.Exists(fileFullName))
            {
                return !shapes.Any();
            }
            var b = GetBitmap(false);
            var res = new Bitmap(fileFullName).CompareMemCmp(b);
            return res;
            //else
            //{
                try
                {
                    
                }
                catch
                {
                    return false;
                }
            //}
        }

        //pens
        private Pen mainPen;
        private readonly Pen selectedPen;
        private readonly Pen anchorPen;
        private readonly Pen selectedBoundsPen;

        //brushes
        private Brush mainBrush;
        private readonly SolidBrush anchorBrush;
        private readonly SolidBrush selectedBoundsBrush;

        //select variables
        private Point startSelectionPoint;
        private Rectangle selectedBounds;
        private List<Shape> selectedItems = new List<Shape>();

        //transform
        private List<Rectangle> transformBuffer = new List<Rectangle>();
        private TransformState transformState = TransformState.None;
        private AnchorPosition selectedAnchorPos = AnchorPosition.None;
        private readonly Size minimumSize = new Size(9, 9);

        //control
        private bool mouseDown;

        //shapes
        private List<Shape> shapes = new List<Shape>();
        private readonly ShapeAnchor[] anchors;

        //menu strip
        private ShapeType selectedShapeType = ShapeType.Ellipse;
        private ToolType selectedToolType = ToolType.Pointer;

        //buffer
        private List<Shape> buffer = new List<Shape>();

        public Form1()
        {
            InitializeComponent();

            //check on save
            mainPen = new Pen(Color.Black, 2);
            mainBrush = new SolidBrush(Color.White);

            selectedPen = new Pen(Color.Black, 1.5f);
            selectedPen.Alignment = PenAlignment.Outset;
            selectedPen.DashStyle = DashStyle.Dash;
            selectedPen.DashPattern = new float[] { 3.5f, 2f };
            selectedBoundsPen = new Pen(Color.FromArgb(51, 120, 232), 1.5f);
            selectedBoundsBrush = new SolidBrush(Color.FromArgb(75, 51, 120, 232));

            anchorPen = new Pen(Color.DimGray, 2f);
            anchorBrush = new SolidBrush(Color.White);

            var size = new Size(8, 8);
            anchors = new ShapeAnchor[]
            {
                    new ShapeAnchor(size, AnchorPosition.Left, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.Top, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.Right, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.Bottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.LeftTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.LeftBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.RightTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.RightBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fill Shape menu
            int index = 0;
            foreach (var ob in Enum.GetNames(typeof(ShapeType)))
            {
                var item = shapeToolStripMenuItem.DropDownItems.Add(ob.SplitCamelCase());
                item.Click += ShapeMenuItemClick;
                var type = (ShapeType)index++;
                item.Tag = type;
            }
            //fill Tools menu
            index = 0;
            foreach (var ob in Enum.GetNames(typeof(ToolType)))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(ob.SplitCamelCase());
                toolsToolStripMenuItem.DropDownItems.Insert(index, item);
                item.Click += ToolsMenuItemClick;
                var type = (ToolType)index++;
                item.Tag = type;

                switch (type)
                {
                    case ToolType.Pointer:
                        item.ShortcutKeys = Keys.Control | Keys.P;
                        break;
                    case ToolType.DrawShape:
                        item.ShortcutKeys = Keys.Control | Keys.U;
                        break;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            foreach (var ob in shapes)
            {
                ob.DrawFill(g);
                ob.DrawStroke(g);
                if (ob.Tag != null && (bool)ob.Tag)
                    ob.DrawBounds(g, selectedPen, null);
            }

            if(mouseDown && selectedToolType == ToolType.DrawShape && transformState == TransformState.None)
            {
                var ob = selectedItems.Single();
                ob.DrawFill(g);
                ob.DrawStroke(g);
                ob.DrawBounds(g, selectedPen, null);
            }
            else
            {
                foreach (var ob in selectedItems)
                {
                    ob.DrawBounds(g, selectedPen, null);
                    ob.DrawAnchors(g, true);
                }
            }

            if(mouseDown && (Math.Abs(selectedBounds.Width) > 1 || Math.Abs(selectedBounds.Height) > 1) && selectedToolType == ToolType.Pointer && transformState == TransformState.None)
            {
                g.FillRectangle(selectedBoundsBrush, selectedBounds.Normalized());
                g.DrawRectangle(selectedBoundsPen, selectedBounds.Normalized());
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                return;
            mouseDown = true;
            startSelectionPoint = e.Location;
            selectedBounds.Location = e.Location;
            selectedBounds.Size = Size.Empty;
            var controlKeyState = ModifierKeys.HasFlag(Keys.Control);

            if (!(controlKeyState && selectedToolType == ToolType.Pointer))
            {
                foreach (var shape in selectedItems)
                {
                    foreach (var anchor in shape.GetAnchors())
                    {
                        //check anchors
                        var anchorBounds = shape.GetAnchorBounds(anchor.Position);
                        if (e.Location.X >= anchorBounds.X && e.Location.Y >= anchorBounds.Y &&
                            e.Location.X <= (anchorBounds.X + anchorBounds.Width) && e.Location.Y <= (anchorBounds.Y + anchorBounds.Height))
                        {
                            transformState = TransformState.Scale;
                            selectedAnchorPos = anchor.Position;
                            transformBuffer.Clear();
                            transformBuffer.AddRange(selectedItems.Select(i => i.Bounds));
                            return;
                        }
                    }
                }
                //check bounds
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    
                    var rBounds = selectedItems[i].GetNormalizedBounds();
                    if (e.Location.X >= rBounds.X && e.Location.Y >= rBounds.Y &&
                            e.Location.X <= (rBounds.X + rBounds.Width) && e.Location.Y <= (rBounds.Y + rBounds.Height))
                    {
                        transformState = TransformState.Move;
                        transformBuffer.Clear();
                        transformBuffer.AddRange(selectedItems.Select(i => i.Bounds));
                        return;
                    }
                }
            }
            
            //tools logic
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    selectedItems.Clear();
                    Shape tempShape;
                    switch (selectedShapeType)
                    {
                        case ShapeType.Line:
                            tempShape = new LineShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        case ShapeType.Rectangle:
                            tempShape = new RectangleShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        case ShapeType.Ellipse:
                            tempShape = new EllipseShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        default:
                            return;
                    }
                    tempShape.AddAnchors(anchors);
                    selectedItems.Add(tempShape);
                    break;
                case ToolType.Pointer:
                    if (!controlKeyState)
                        selectedItems.Clear();
                    for (int i = shapes.Count - 1; i >= 0; i--)
                    {
                        var p = e.Location;
                        if (p.X > shapes[i].Bounds.X && p.Y > shapes[i].Bounds.Y && p.X < (shapes[i].Bounds.X + shapes[i].Bounds.Width) && p.Y < (shapes[i].Bounds.Y + shapes[i].Bounds.Height))
                        {
                            if (controlKeyState)
                            {
                                if(selectedItems.Contains(shapes[i]))
                                    selectedItems.Remove(shapes[i]);
                                else
                                    selectedItems.Add(shapes[i]);
                            }
                            else
                            {
                                transformState = TransformState.Move;
                                selectedItems.Add(shapes[i]);
                            }
                            transformBuffer.Clear();
                            transformBuffer.AddRange(selectedItems.Select(i => i.Bounds));
                            break;
                        }
                    }
                    pictureBox1.Invalidate();
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;

            selectedBounds.Size = new Size(e.X - startSelectionPoint.X, e.Y - startSelectionPoint.Y);

            if (transformState != TransformState.None)//transform logic
            {
                switch (transformState)
                {
                    case TransformState.Scale:
                        Rectangle increase = Rectangle.Empty;
                        switch (selectedAnchorPos)
                        {
                            case AnchorPosition.RightBottom:
                                increase.Size = new Size(selectedBounds.Width, selectedBounds.Height);
                                break;
                            case AnchorPosition.LeftBottom:
                                increase.Size = new Size(-selectedBounds.Width, selectedBounds.Height);
                                increase.Location = new Point(selectedBounds.Width, 0);
                                break;
                            case AnchorPosition.LeftTop:
                                increase.Size = new Size(-selectedBounds.Width, -selectedBounds.Height);
                                increase.Location = new Point(selectedBounds.Width, selectedBounds.Height);
                                break;
                            case AnchorPosition.Left:
                                increase.Size = new Size(-selectedBounds.Width, 0);
                                increase.Location = new Point(selectedBounds.Width, 0);
                                break;
                            case AnchorPosition.RightTop:
                                increase.Size = new Size(selectedBounds.Width, -selectedBounds.Height);
                                increase.Location = new Point(0, selectedBounds.Height);
                                break;
                            case AnchorPosition.Right:
                                increase.Size = new Size(selectedBounds.Width, 0);
                                break;
                            case AnchorPosition.Top:
                                increase.Size = new Size(0, -selectedBounds.Height);
                                increase.Location = new Point(0, selectedBounds.Height);
                                break;
                            case AnchorPosition.Bottom:
                                increase.Size = new Size(0, selectedBounds.Height);
                                break;
                        }
                        for (int i = 0; i < transformBuffer.Count; i++)
                        {
                            var tempBounds = new Rectangle(new Point(increase.Location.X + transformBuffer[i].Location.X, increase.Location.Y + transformBuffer[i].Location.Y), increase.Size + transformBuffer[i].Size);

                            if (minimumSize.Width > tempBounds.Width)
                                tempBounds.Width = minimumSize.Width;
                            if (minimumSize.Height > tempBounds.Height)
                                tempBounds.Height = minimumSize.Height;
                            var minX = (transformBuffer[i].X + transformBuffer[i].Width - minimumSize.Width);
                            var minY = (transformBuffer[i].Y + transformBuffer[i].Height - minimumSize.Height);
                            if (minX < tempBounds.X)
                                tempBounds.X = minX;
                            if (minY < tempBounds.Y)
                                tempBounds.Y = minY;

                            selectedItems[i].Bounds = tempBounds;
                        }
                        break;
                    case TransformState.Move:
                        for (int i = 0; i < selectedItems.Count; i++)
                        {
                            selectedItems[i].Bounds.Location = new Point(transformBuffer[i].X + selectedBounds.Width, transformBuffer[i].Y + selectedBounds.Height);
                        }
                        break;
                }
                pictureBox1.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    selectedItems.Single().Bounds.Size = selectedBounds.Size;
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.Normalized();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        if (selectedItems.Any())
                            selectedItems.Clear();
                        foreach (var ob in shapes)
                        {
                            var r = ob.Bounds;

                            if (r.X > normalized.X && r.Y > normalized.Y && (r.X + r.Width) < (normalized.X + normalized.Width) && (r.Y + r.Height) < (normalized.Y + normalized.Height))
                                ob.Tag = true;
                            else
                                ob.Tag = false;
                        }
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;
            mouseDown = false;
            if (transformState != TransformState.None)
            {
                transformState = TransformState.None;
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    if (Math.Abs(selectedBounds.Width) > minimumSize.Width && Math.Abs(selectedBounds.Height) > minimumSize.Height)
                    {
                        var item = selectedItems.Single();
                        item.NormalizeBounds();
                        shapes.Add(item);
                    }
                    else
                        selectedItems.Clear();
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.Normalized();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        selectedItems.AddRange(shapes.Where(i =>
                        {
                            var value = i.Tag != null && (bool)i.Tag;
                            i.Tag = false;
                            return value;
                        }).Select(i => i));
                    }
                    break;
            }
            pictureBox1.Invalidate();
            this.Text = IsSaved().ToString();
        }

        private void ShapeMenuItemClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Tag is ShapeType type)
                {
                    selectedShapeType = type;
                }
            }
        }

        private void ToolsMenuItemClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Tag is ToolType type)
                {
                    selectedToolType = type;
                }
            }
        }

        private void penSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new PenSetupDialog("Pen setting", mainPen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainPen = dialog.GetValue();
                if(selectedItems.Count > 0)
                {
                    foreach (var ob in selectedItems)
                    {
                        ob.Pen = mainPen;
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new BrushSetupDialog("Brush setting", mainBrush);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainBrush = dialog.GetValue();
                if (selectedItems.Count > 0)
                {
                    foreach (var ob in selectedItems)
                    {
                        ob.Brush = mainBrush;
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void RemoveItems()
        {
            if (!selectedItems.Any())
                return;
            for (int i = selectedItems.Count - 1; i >= 0; i--)
            {
                if (shapes.Contains(selectedItems[i]))
                {
                    shapes.Remove(selectedItems[i]);
                }
            }
            selectedItems.Clear();
        }

        private void CutItems()
        {
            if (!selectedItems.Any())
                return;
            buffer.Clear();
            buffer.AddRange(selectedItems.Select(i => (Shape)i.Clone()));
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void PasteItems()
        {
            if (!buffer.Any())
                return;
            selectedItems.Clear();
            var coll = buffer.Select(i =>
            {
                i = (Shape)i.Clone();
                i.Bounds.X += 15;
                i.Bounds.Y += 15;
                return i;
            });
            int index = shapes.Count;
            shapes.AddRange(coll);
            selectedItems.AddRange(shapes.Skip(index));
            pictureBox1.Invalidate();
        }

        private void CopyItems()
        {
            if (!selectedItems.Any())
                return;
            buffer.Clear();
            buffer.AddRange(selectedItems.Select(i => (Shape)i.Clone()));
            pictureBox1.Invalidate();
        }

        private void BringToFrontItems()
        {
            foreach(var ob in selectedItems)
            {
                int index = shapes.IndexOf(ob);
                int lastIndex = shapes.Count - 1;
                if (index != -1 && index < lastIndex)
                {
                    shapes.RemoveAt(index);
                    shapes.Insert(index + 1, ob);
                }
            }
            pictureBox1.Invalidate();
        }

        private void SendToBackItems()
        {
            foreach (var ob in selectedItems)
            {
                int index = shapes.IndexOf(ob);
                if (index != -1 && index > 0)
                {
                    shapes.RemoveAt(index);
                    shapes.Insert(index - 1, ob);
                }
            }
            pictureBox1.Invalidate();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CutItems();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PasteItems();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedItems.Clear();
            selectedItems.AddRange(shapes.Select(i => i));
            pictureBox1.Invalidate();
        }

        private void resetSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedItems.Clear();
            pictureBox1.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool oneOrMoreSelected = selectedItems.Count >= 1;
            propertiesToolStripMenuItem.Enabled = selectedItems.Count == 1;
            cutToolStripMenuItem.Enabled = oneOrMoreSelected;
            copyToolStripMenuItem.Enabled = oneOrMoreSelected;
            pasteToolStripMenuItem.Enabled = buffer.Any();
            deleteToolStripMenuItem.Enabled = oneOrMoreSelected;
            bringToFrontToolStripMenuItem.Enabled = oneOrMoreSelected;
            sendToBackToolStripMenuItem.Enabled = oneOrMoreSelected;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedItems.Count != 1)
                return;
            var dialog = new SetupShapePropertiesDialog("Shape setting", selectedItems.Single());
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                var index = shapes.IndexOf(selectedItems.Single());
                if (index == -1)
                    return;
                shapes[index] = dialog.GetValue();
                selectedItems.Clear();
                selectedItems.Add(shapes[index]);
                pictureBox1.Invalidate();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutItems();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteItems();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringToFrontItems();
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToBackItems();
        }

        private void Export()
        {
            var dialog = new SaveFileDialog();

            var codecs = ImageCodecInfo.GetImageEncoders();
            var sep = string.Empty;

            foreach (var c in codecs)
            {
                if (c.CodecName != null)
                {
                    string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                    dialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", dialog.Filter, sep, codecName, c.FilenameExtension);
                    sep = "|";
                }
            }

            dialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", dialog.Filter, sep, "All Files", "*.*");
            dialog.DefaultExt = ".png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileFullName = dialog.FileName;
                fileName = Path.GetFileNameWithoutExtension(fileFullName);

                try
                {
                    if ((dialog.FilterIndex - 1) >= codecs.Length)
                    {
                        GetBitmap(false).Save(fileFullName);
                    }
                    else
                    {
                        EncoderParameters encoderParams = new EncoderParameters(1);
                        EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, 25L);
                        encoderParams.Param[0] = encoderParam;
                        GetBitmap(false).Save(fileFullName, codecs[dialog.FilterIndex], encoderParams);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Bitmap GetBitmap(bool dispose = true)
        {
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = Graphics.FromImage(map))
            {
                foreach (var ob in shapes)
                {
                    ob.DrawFill(g);
                    ob.DrawStroke(g);
                }
            }
            if (dispose)
                map.Dispose();
            return map;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export();
        }
    }
}