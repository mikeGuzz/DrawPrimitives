using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Security;
using System.Xml.Schema;

namespace DrawPrimitives
{
    public enum ToolType { Pointer = 0, DrawShape = 1 };
    public enum ShapeType { Rectangle, Ellipse, TextBox };

    public partial class Form1 : Form
    {
        private enum TransformState { None, Move, Rotate, Scale };

        //file
        private string filePath = string.Empty;
        private string fileName => string.IsNullOrEmpty(filePath) ? "Untitled" : (File.Exists(filePath) ? Path.GetFileNameWithoutExtension(filePath) : "Untitled");

        //pens
        private Pen mainPen;
        private readonly Pen selectedPen;
        private readonly Pen anchorPen;
        private readonly Pen selectedBoundsPen;

        //brushes
        private Brush mainBrush;
        private readonly SolidBrush anchorBrush;
        private readonly SolidBrush selectedBoundsBrush;

        //select
        private Rectangle selectedBounds; 

        //transform
        private TransformState transformState = TransformState.None;
        private AnchorPosition selectedAnchorPos = AnchorPosition.None;
        private TransformHelper transformHelper;
        private ShapeAnchor[] anchors;
        private readonly Size minimumSize = new Size(9, 9);

        //control
        private bool mouseDown;

        //shapes
        private List<Shape> shapes = new List<Shape>();

        //menu strip
        private ShapeType selectedShapeType = ShapeType.Ellipse;
        private ToolType selectedToolType = ToolType.Pointer;

        //buffer
        private List<Shape> buffer = new List<Shape>();

        public Form1()
        {
            InitializeComponent();

            var p1 = new Point(0, 0);
            var p2 = new Point(-100, -50);

            MessageBox.Show(Rectangle.FromLTRB(p1.X, p1.Y, p2.X, p2.Y).ToString());

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

            var anchorSize = new Size(8, 8);
            anchors = new ShapeAnchor[]
            {
                    new ShapeAnchor(anchorSize, AnchorPosition.Left, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.Top, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.Right, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.Bottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.LeftTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.LeftBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.RightTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.RightBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
            };
            transformHelper = new TransformHelper();

            this.Text = GetTitle();
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
                if(ob.IsSelected)
                    ob.DrawBounds(g, selectedPen, null);
            }

            if (transformHelper.Any())
            {
                transformHelper.DrawBounds(g, selectedPen, null);
                var r = transformHelper.CurrentBounds;
                foreach (var ob in anchors)
                    ob.Draw(g, r, true);
            }

            if (mouseDown && (Math.Abs(selectedBounds.Width) > 1 || Math.Abs(selectedBounds.Height) > 1) && selectedToolType == ToolType.Pointer && transformState == TransformState.None)
            {
                g.FillRectangle(selectedBoundsBrush, selectedBounds.WithoutNegative());
                g.DrawRectangle(selectedBoundsPen, selectedBounds.WithoutNegative());
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                return;
            mouseDown = true;
            selectedBounds.Location = e.Location;
            selectedBounds.Size = Size.Empty;
            var controlKeyState = ModifierKeys.HasFlag(Keys.Control);

            if (!(controlKeyState && selectedToolType == ToolType.Pointer) && transformHelper.Any())
            {
                if(IsHitAnyAnchor(e.Location, out var pos))
                {
                    transformHelper.StartTransform();
                    transformState = TransformState.Scale;
                    selectedAnchorPos = pos;
                    return;
                }
                if(transformHelper.IsHit(e.Location))
                {
                    transformHelper.StartTransform();
                    transformState = TransformState.Move;
                    return;
                }
            }
            
            //tools logic
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    transformHelper.Clear();
                    Shape shape;
                    switch (selectedShapeType)
                    {
                        case ShapeType.Rectangle:
                            shape = new RectangleShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        case ShapeType.Ellipse:
                            shape = new EllipseShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        default:
                            return;
                    }
                    shape.IsSelected = true;
                    shapes.Add(shape);
                    break;
                case ToolType.Pointer:
                    if (!controlKeyState)
                        transformHelper.Clear();
                    foreach(var ob in shapes.Reverse<Shape>())
                    {
                        var p = e.Location;
                        if (p.X > ob.Bounds.X && p.Y > ob.Bounds.Y && p.X < (ob.Bounds.X + ob.Bounds.Width) && p.Y < (ob.Bounds.Y + ob.Bounds.Height))
                        {
                            if (controlKeyState)
                            {
                                if (transformHelper.Contains(ob))
                                    transformHelper.Remove(ob);
                                else
                                    transformHelper.Add(ob);
                            }
                            else
                            {
                                transformState = TransformState.Move;
                                transformHelper.Add(ob);
                                transformHelper.StartTransform();
                            }
                            pictureBox1.Invalidate();
                            return;
                        }
                    }
                    transformHelper.Clear();
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (transformHelper.Any() && transformState == TransformState.None)
            {
                if (IsHitAnyAnchor(e.Location, out var pos))
                {
                    switch (pos)
                    {
                        case AnchorPosition.RightBottom:
                            pictureBox1.Cursor = Cursors.SizeNWSE;
                            break;
                        case AnchorPosition.LeftBottom:
                            pictureBox1.Cursor = Cursors.SizeNESW;
                            break;
                        case AnchorPosition.LeftTop:
                            pictureBox1.Cursor = Cursors.SizeNWSE;
                            break;
                        case AnchorPosition.Left:
                            pictureBox1.Cursor = Cursors.SizeWE;
                            break;
                        case AnchorPosition.RightTop:
                            pictureBox1.Cursor = Cursors.SizeNESW;
                            break;
                        case AnchorPosition.Right:
                            pictureBox1.Cursor = Cursors.SizeWE;
                            break;
                        case AnchorPosition.Top:
                            pictureBox1.Cursor = Cursors.SizeNS;
                            break;
                        case AnchorPosition.Bottom:
                            pictureBox1.Cursor = Cursors.SizeNS;
                            break;
                    }
                }
                else if (transformHelper.IsHit(e.Location))
                    pictureBox1.Cursor = Cursors.SizeAll;
                else
                    ResetCursor();
            }

            if (!mouseDown)
                return;

            selectedBounds.Size = new Size(e.X - selectedBounds.X, e.Y - selectedBounds.Y);

            if (transformState != TransformState.None && transformHelper.Any())
            { 
                switch (transformState)
                {
                    case TransformState.Move:
                        transformHelper.TransformPosition(new Point(selectedBounds.Size));
                        break;
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
                        var tempBounds = new Rectangle(new Point(increase.Location.X + transformHelper.StartTransformBounds.Location.X, increase.Location.Y + transformHelper.StartTransformBounds.Location.Y), increase.Size + transformHelper.StartTransformBounds.Size);

                        if (minimumSize.Width > tempBounds.Width)
                            tempBounds.Width = minimumSize.Width;
                        if (minimumSize.Height > tempBounds.Height)
                            tempBounds.Height = minimumSize.Height;

                        var minX = (transformHelper.StartTransformBounds.X + transformHelper.StartTransformBounds.Width - minimumSize.Width);
                        var minY = (transformHelper.StartTransformBounds.Y + transformHelper.StartTransformBounds.Height - minimumSize.Height);

                        if (minX < tempBounds.X)
                            tempBounds.X = minX;
                        if (minY < tempBounds.Y)
                            tempBounds.Y = minY;

                        transformHelper.Bound(tempBounds);
                        break;
                }
                pictureBox1.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    shapes.Last().Bounds = selectedBounds.WithoutNegative();
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.WithoutNegative();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        foreach (var ob in shapes)
                        {
                            var r = ob.Bounds;
                            ob.IsSelected = r.X > normalized.X && r.Y > normalized.Y && (r.X + r.Width) < (normalized.X + normalized.Width) && (r.Y + r.Height) < (normalized.Y + normalized.Height);
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
                    if (!(Math.Abs(selectedBounds.Width) > minimumSize.Width && Math.Abs(selectedBounds.Height) > minimumSize.Height))
                        shapes.RemoveAt(shapes.Count - 1);
                    else
                    {
                        shapes.Last().IsSelected = false;
                        transformHelper.Add(shapes.Last());
                    } 
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.WithoutNegative();
                    if ((normalized.Width > 1 || normalized.Height > 1))
                    {
                        transformHelper.Clear();
                        transformHelper.AddRange(shapes.Where(i =>
                        {
                            if (i.IsSelected)
                            {
                                i.IsSelected = false;
                                return true;
                            }
                            return false;
                        }));
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private bool IsHitAnyAnchor(Point mousePos, out AnchorPosition pos)
        {
            pos = AnchorPosition.None;
            if (!transformHelper.Any())
                return false;
            foreach (var ob in anchors)
            {
                var anchorBounds = ob.GetBounds(transformHelper.CurrentBounds);
                if (mousePos.X >= anchorBounds.X && mousePos.Y >= anchorBounds.Y &&
                    mousePos.X <= (anchorBounds.X + anchorBounds.Width) && mousePos.Y <= (anchorBounds.Y + anchorBounds.Height))
                {
                    pos = ob.Position;
                    return true;
                }
            }
            return false;
        }

        public override void ResetCursor()
        {
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    pictureBox1.Cursor = Cursors.Cross;
                    break;
                case ToolType.Pointer:
                    pictureBox1.Cursor = Cursors.Default;
                    break;
            }
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
                    ResetCursor();
                }
            }
        }

        private void penSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new PenSetupDialog("Pen setting", mainPen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainPen = dialog.GetValue();
                foreach (var ob in transformHelper)
                {
                    ob.Pen = mainPen;
                }
                pictureBox1.Invalidate();
            }
        }

        private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new BrushSetupDialog("Brush setting", mainBrush);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainBrush = dialog.GetValue();
                foreach (var ob in transformHelper)
                {
                    ob.Brush = mainBrush;
                }
                pictureBox1.Invalidate();
            }
        }

        private void RemoveItems()
        {
            foreach(var ob in transformHelper)
            {
                shapes.Remove(ob);
            }
            transformHelper.Clear();
        }

        private void CutItems()
        {
            if (!transformHelper.Any())
                return;
            buffer.Clear();
            buffer.AddRange(transformHelper.Select(i => (Shape)i.Clone()));
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void PasteItems()
        {
            if (!buffer.Any())
                return;
            transformHelper.Clear();
            shapes.AddRange(buffer.Select(i =>
            {
                i = (Shape)i.Clone();
                if (MousePosition.X > pictureBox1.Location.X && MousePosition.Y > pictureBox1.Location.Y && MousePosition.X < (pictureBox1.Location.X + pictureBox1.Width) && MousePosition.Y < (pictureBox1.Location.Y + pictureBox1.Height))
                {
                    i.Bounds.Location = new Point(MousePosition.X + 15, MousePosition.Y + 15);//temp
                }
                else
                {
                    i.Bounds.X += 15;
                    i.Bounds.Y += 15;
                }
                transformHelper.Add(i);
                return i;
            }));
            pictureBox1.Invalidate();
        }

        private void CopyItems()
        {
            if (!transformHelper.Any())
                return;
            buffer.Clear();
            buffer.AddRange(transformHelper.Select(i => (Shape)i.Clone()));
            pictureBox1.Invalidate();
        }

        private void BringToFrontItems()
        {
            foreach (var ob in transformHelper)
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
            foreach (var ob in transformHelper)
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
            transformHelper.Clear();
            transformHelper.AddRange(shapes);
            pictureBox1.Invalidate();
        }

        private void resetSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transformHelper.Clear();
            pictureBox1.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool oneOrMoreSelected = transformHelper.Count >= 1;
            propertiesToolStripMenuItem.Enabled = transformHelper.Count == 1;
            cutToolStripMenuItem.Enabled = oneOrMoreSelected;
            copyToolStripMenuItem.Enabled = oneOrMoreSelected;
            pasteToolStripMenuItem.Enabled = buffer.Any();
            deleteToolStripMenuItem.Enabled = oneOrMoreSelected;
            bringToFrontToolStripMenuItem.Enabled = oneOrMoreSelected;
            sendToBackToolStripMenuItem.Enabled = oneOrMoreSelected;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            var dialog = new SetupShapePropertiesDialog("Shape setting", transformHelper.Single());
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var index = shapes.IndexOf(transformHelper.Single());
                if (index == -1)
                    return;
                shapes[index] = dialog.GetValue();
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

        private bool IsSaved()
        {
            if (!File.Exists(filePath))
            {
                return !shapes.Any();
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Shape[]));
                try
                {
                    using (Stream s = File.OpenRead(filePath))
                    {
                        if (serializer.Deserialize(s) is Shape[] list)
                        {
                            return shapes.Except(list).Count() == 0;
                        }
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        private string GetTitle()
        {
            return $"{fileName} - {ProductName.SplitCamelCase()}";
        }

        private Bitmap GetBitmap(bool dispose)
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

        public bool Export(string fileName)
        {
            try
            {
                using (var map = GetBitmap(false))
                {
                    map.Save(fileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Export()
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp|HEIC files (*.heic)|*.heic|TIFF files (*.tif)|*.tif|All files (*.*)|*.*";
            dialog.DefaultExt = ".png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Export(dialog.FileName);
            }
        }

        public bool Save(string fileName)
        {
            // json serialize a array of shapes

            //using (Stream s = File.Create("jsonShapeSer.txt"))
            //{
            //    using (StreamWriter w = new StreamWriter(s))
            //    {
            //        var opt = new JsonSerializerOptions()
            //        {
            //            WriteIndented = true,
            //        };
            //        string json = JsonSerializer.Serialize(shapes.ToArray(), opt);

            //        w.WriteLine(json);
            //    }
            //}

            try
            {//use xmlwriter
                using (Stream s = File.Create(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Shape[]));
                    serializer.Serialize(s, shapes.ToArray());
                    filePath = fileName;
                    Text = GetTitle();
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Save()
        {
            if (File.Exists(filePath))
                Save(filePath);
            else
                SaveAsNew();
        }

        private void SaveAsNew()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
                Save(dialog.FileName);
        }

        public void New()
        {
            if(!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                    Save();
                else if (res == DialogResult.Cancel)
                    return;
            }
            transformHelper.Clear();
            shapes.Clear();
            filePath = string.Empty;
            Text = GetTitle();

            pictureBox1.Invalidate();
        }

        public bool OpenXml(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                    Save();
                else if (res == DialogResult.Cancel)
                    return false;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Shape>));
            try
            {
                using(Stream s = File.OpenRead(fileName))
                {
                    if(serializer.Deserialize(s) is List<Shape> list)
                    {
                        transformHelper.Clear();
                        shapes.Clear();

                        shapes.AddRange(list);
                        filePath = fileName;
                        Text = GetTitle();

                        pictureBox1.Invalidate();

                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"'{fileName}'\n{ProductName.SplitCamelCase()} cannot read this file.\nThis is not a valid bitmap file, or its format is not currently supported.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"An error occurred while loading file '{fileName}'.\nError message: {e.Message}", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Open()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                OpenXml(dialog.FileName);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                    Save();
                else if(res == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsNew();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }
    }
}