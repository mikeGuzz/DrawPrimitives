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
using System.Windows.Forms;
using System.Xml;
using DrawPrimitives.Dialogs;
using DrawPrimitives.Helpers;
using DrawPrimitives.Shapes;
using DrawPrimitives.Dialogs.Editors;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using DrawPrimitives.My;
using DrawPrimitives.Dialogs.Editors.ShapesEditor;
using System.IO;
using System.Xml.Linq;

namespace DrawPrimitives
{
    public enum ToolType { Pointer, DrawShape };
    public enum ShapeType { Rectangle, Ellipse, IsoscelesTriangle, RightTriangle, Diamond, Trapezium, Pentagon, Hexagone, Cube };

    public partial class Form1 : Form
    {
        private enum TransformState { None, Move, Rotate, Scale };

        private string filePath = string.Empty;
        private string fileName => string.IsNullOrEmpty(filePath) ? "Untitled" : (File.Exists(filePath) ? Path.GetFileNameWithoutExtension(filePath) : "Untitled");

        private const string SaveFileFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        private const string ExportFileFilter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp|HEIC files (*.heic)|*.heic|TIFF files (*.tif)|*.tif|All files (*.*)|*.*";

        private readonly Pen selectedPen;
        private readonly Pen anchorPen;
        private readonly Pen selectedBoundsPen;
        private readonly SolidBrush anchorBrush;
        private readonly SolidBrush selectedBoundsBrush;

        private Rectangle scaledSelectedArea
        {
            get
            {
                var wN = selectedArea;
                return new Rectangle((int)(wN.X / zoom), (int)(wN.Y / zoom), (int)(wN.Width / zoom), (int)(wN.Height / zoom));
            }
        }
        private Rectangle selectedArea;

        //transform
        private TransformState transformState = TransformState.None;
        private AnchorPosition selectedAnchorPos = AnchorPosition.None;
        private TransformHelper transformHelper;
        private ShapeAnchor[] anchors;

        //control
        private bool mouseLeftDown => MouseButtons.HasFlag(MouseButtons.Left);
        private Point mousePos;

        //shapes
        private List<Shape> shapes = new List<Shape>();

        //menu strip
        private ShapeType selectedShapeType = ShapeType.Ellipse;
        private ToolType selectedToolType = ToolType.Pointer;

        //buffer
        private List<Shape> buffer = new List<Shape>();

        //undo/redo
        private UndoRedoHelper<string> undoRedoHelper;

        //json
        private JsonSerializerOptions jsonProperty;

        //other
        private float zoom { get; set; } = 1f;
        private Canvas canvas = new Canvas();

        public Form1()
        {
            InitializeComponent();

            jsonProperty = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };

            UpdateWorkingScreen();

            selectedPen = new Pen(Color.Black, 1.5f);
            selectedPen.Alignment = PenAlignment.Outset;
            selectedPen.DashStyle = DashStyle.Dash;
            selectedPen.DashPattern = new float[] { 3.5f, 2f };

            selectedBoundsPen = new Pen(Color.FromArgb(51, 120, 232), 1.5f);
            selectedBoundsBrush = new SolidBrush(Color.FromArgb(75, 51, 120, 232));

            anchorPen = new Pen(Color.DimGray, 2.5f);
            anchorBrush = new SolidBrush(Color.White);

            var anchorSize = new Size(10, 10);
            anchors = new ShapeAnchor[]
            {
                    new ShapeAnchor(anchorSize, AnchorPosition.Left, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.Top, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.Right, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.Bottom, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.LeftTop, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.LeftBottom, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.RightTop, anchorPen, anchorBrush, AnchorShape.Round),
                    new ShapeAnchor(anchorSize, AnchorPosition.RightBottom, anchorPen, anchorBrush, AnchorShape.Round),
            };
            transformHelper = new TransformHelper(selectedPen, null);
            undoRedoHelper = new UndoRedoHelper<string>();

            Text = GetTitle();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fill Shape menu
            int index = 0;
            foreach (var ob in Enum.GetNames(typeof(ShapeType)))
            {
                var item = (ToolStripMenuItem)shapeToolStripMenuItem.DropDownItems.Add(ob.SplitCamelCase());
                item.Click += ShapeMenuItemClick;
                var type = (ShapeType)index++;
                item.Tag = type;

                if (type == selectedShapeType)
                    item.CheckState = CheckState.Indeterminate;
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

                if (type == selectedToolType)
                    item.CheckState = CheckState.Indeterminate;
            }
        }

        private void UpdateWorkingScreen()
        {
            pictureBox.Size = new Size((int)(canvas.Width * zoom), (int)(canvas.Height * zoom));
            zoomToolStripMenuItem.Text = $"Zoom ({Math.Round(zoom * 100)} %)";
            pictureBox.Invalidate();
        }

        private void UpdateCanvas()
        {
            pictureBox.BackColor = Color.FromArgb(byte.MaxValue, canvas.Color);
            UpdateWorkingScreen();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            using (var t = new Matrix())
            {
                t.Scale(zoom, zoom);
                g.Transform = t;
                foreach (var ob in shapes)
                {
                    ob.Draw(g);
                }
                g.ResetTransform();
            }

            shapes.ForEach(i =>
            {
                if (i.IsSelected)
                    g.DrawRectangle(selectedPen, i.GetBounds(zoom).WithoutNegative());
            });

            if (transformHelper.Any())
            {
                transformHelper.Draw(g, zoom);
                var r = transformHelper.GetBounds(zoom);
                var temp = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.HighQuality;
                foreach (var ob in anchors)
                    ob.Draw(g, r, true);
                g.SmoothingMode = temp;
            }

            if (mouseLeftDown && (Math.Abs(selectedArea.Width) > 1 || Math.Abs(selectedArea.Height) > 1) && selectedToolType == ToolType.Pointer && transformState == TransformState.None)
            {
                var bounds = selectedArea.WithoutNegative();
                g.FillRectangle(selectedBoundsBrush, bounds);
                g.DrawRectangle(selectedBoundsPen, bounds);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            selectedArea.Location = e.Location;
            selectedArea.Size = Size.Empty;
            var controlKeyState = ModifierKeys.HasFlag(Keys.Control);

            if (!(controlKeyState && selectedToolType == ToolType.Pointer) && transformHelper.Any())
            {
                if (IsHitAnyAnchor(out var pos))
                {
                    transformHelper.StartTransform();
                    transformState = TransformState.Scale;
                    selectedAnchorPos = pos;
                    if (TryXmlSerializeDataToString(out var xml))
                        undoRedoHelper.AddItem(xml);
                    return;
                }
                if (transformHelper.IsHit(e.Location, zoom))
                {
                    transformHelper.StartTransform();
                    transformState = TransformState.Move;
                    if (TryXmlSerializeDataToString(out var xml))
                        undoRedoHelper.AddItem(xml);
                    return;
                }
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    if (TryXmlSerializeDataToString(out var xml))
                        undoRedoHelper.AddItem(xml);

                    transformHelper.Clear();
                    Shape shape;
                    switch (selectedShapeType)
                    {
                        case ShapeType.Rectangle:
                            shape = new RectangleShape(scaledSelectedArea);
                            break;
                        case ShapeType.Ellipse:
                            shape = new EllipseShape(scaledSelectedArea);
                            break;
                        case ShapeType.Diamond:
                            shape = new DiamondShape(scaledSelectedArea);
                            break;
                        case ShapeType.Pentagon:
                            shape = new PentagonShape(scaledSelectedArea);
                            break;
                        case ShapeType.Hexagone:
                            shape = new HexagonShape(scaledSelectedArea);
                            break;
                        case ShapeType.Cube:
                            shape = new CubeShape(scaledSelectedArea);
                            break;
                        case ShapeType.IsoscelesTriangle:
                            shape = new IsoscelesTriangleShape(scaledSelectedArea);
                            break;
                        case ShapeType.RightTriangle:
                            shape = new RightTriangleShape(scaledSelectedArea);
                            break;
                        case ShapeType.Trapezium:
                            shape = new TrapeziumShape(scaledSelectedArea);
                            break;
                        default:
                            return;
                    }
                    shape.IsSelected = true;
                    shape.UseBrush = drawFillToolStripMenuItem.Checked;
                    shape.UsePen = drawOutlineToolStripMenuItem.Checked;
                    shapes.Add(shape);
                    break;
                case ToolType.Pointer:
                    if (!controlKeyState)
                        transformHelper.Clear();
                    foreach (var ob in shapes.Reverse<Shape>())
                    {
                        if (ob.IsHit(e.Location, zoom))
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
                            pictureBox.Invalidate();
                            return;
                        }
                    }
                    transformHelper.Clear();
                    break;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.Location;
            mousePos_toolStripStatusLabel.Text = $"{Math.Round(e.X / zoom)}, {Math.Round(e.Y / zoom)}px";

            UpdateCursor();

            if (e.Button != MouseButtons.Left)
                return;

            selectedArea.Size = new Size(e.X - selectedArea.X, e.Y - selectedArea.Y);

            if (transformState != TransformState.None && transformHelper.Any())
            {
                var realR = scaledSelectedArea;
                switch (transformState)
                {
                    case TransformState.Move:
                        transformHelper.SetPosition(new Point(realR.Size));
                        break;
                    case TransformState.Scale:
                        Rectangle increase = Rectangle.Empty;
                        switch (selectedAnchorPos)
                        {
                            case AnchorPosition.RightBottom:
                                increase.Size = new Size(realR.Width, realR.Height);
                                break;
                            case AnchorPosition.LeftBottom:
                                increase.Size = new Size(-realR.Width, realR.Height);
                                increase.Location = new Point(realR.Width, 0);
                                break;
                            case AnchorPosition.LeftTop:
                                increase.Size = new Size(-realR.Width, -realR.Height);
                                increase.Location = new Point(realR.Width, realR.Height);
                                break;
                            case AnchorPosition.Left:
                                increase.Size = new Size(-realR.Width, 0);
                                increase.Location = new Point(realR.Width, 0);
                                break;
                            case AnchorPosition.RightTop:
                                increase.Size = new Size(realR.Width, -realR.Height);
                                increase.Location = new Point(0, realR.Height);
                                break;
                            case AnchorPosition.Right:
                                increase.Size = new Size(realR.Width, 0);
                                break;
                            case AnchorPosition.Top:
                                increase.Size = new Size(0, -realR.Height);
                                increase.Location = new Point(0, realR.Height);
                                break;
                            case AnchorPosition.Bottom:
                                increase.Size = new Size(0, realR.Height);
                                break;
                        }
                        var tempBounds = new Rectangle(new Point(increase.Location.X + transformHelper.StartTransformBounds.Location.X, increase.Location.Y + transformHelper.StartTransformBounds.Location.Y), increase.Size + transformHelper.StartTransformBounds.Size);

                        if (Shape.MinimumSize.Width > tempBounds.Width)
                            tempBounds.Width = Shape.MinimumSize.Width;
                        if (Shape.MinimumSize.Height > tempBounds.Height)
                            tempBounds.Height = Shape.MinimumSize.Height;

                        var minX = (transformHelper.StartTransformBounds.X + transformHelper.StartTransformBounds.Width - Shape.MinimumSize.Width);
                        var minY = (transformHelper.StartTransformBounds.Y + transformHelper.StartTransformBounds.Height - Shape.MinimumSize.Height);

                        if (minX < tempBounds.X)
                            tempBounds.X = minX;
                        if (minY < tempBounds.Y)
                            tempBounds.Y = minY;

                        transformHelper.Bound(tempBounds);
                        break;
                }
                pictureBox.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    shapes.Last().Bound(scaledSelectedArea.WithoutNegative());
                    break;
                case ToolType.Pointer:
                    var normalized = selectedArea.WithoutNegative();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        foreach (var ob in shapes)
                        {
                            var r = ob.GetBounds(zoom);
                            ob.IsSelected = r.X > normalized.X && r.Y > normalized.Y && (r.X + r.Width) < (normalized.X + normalized.Width) && (r.Y + r.Height) < (normalized.Y + normalized.Height);
                        }
                    }
                    break;
            }
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (transformState != TransformState.None)
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    var rect = shapes[i].GetBounds().WithoutNegative();
                    if (rect.OutOfBounds(canvas.Size))
                    {
                        transformHelper.Remove(shapes[i]);
                        shapes.RemoveAt(i);
                    }
                }
                transformState = TransformState.None;
                pictureBox.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    if (scaledSelectedArea.Width == 0 && scaledSelectedArea.Height == 0)
                        shapes.RemoveAt(shapes.Count - 1);
                    else
                    {
                        shapes.Last().IsSelected = false;
                        shapes.Last().Bound(scaledSelectedArea.WithoutNegative());
                        transformHelper.Add(shapes.Last());

                        //if (shapes.Last() is TextBoxShape)
                        //    OpenTextShapeDialog(sender, e);
                    }
                    break;
                case ToolType.Pointer:
                    var normalized = selectedArea.WithoutNegative();
                    if (normalized.Width > 1 || normalized.Height > 1)
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
            pictureBox.Invalidate();
        }

        private bool IsHitAnyAnchor(out AnchorPosition pos)
        {
            pos = AnchorPosition.None;
            if (!transformHelper.Any())
                return false;
            foreach (var ob in anchors)
            {
                var anchorBounds = ob.GetBounds(transformHelper.GetBounds(zoom));
                if (mousePos.X >= anchorBounds.X && mousePos.Y >= anchorBounds.Y &&
                    mousePos.X <= (anchorBounds.X + anchorBounds.Width) && mousePos.Y <= (anchorBounds.Y + anchorBounds.Height))
                {
                    pos = ob.Position;
                    return true;
                }
            }
            return false;
        }

        private void UpdateCursor()
        {
            if (transformState != TransformState.None)
                return;
            if (!transformHelper.Any())
            {
                ResetCursor();
                return;
            }
            if (IsHitAnyAnchor(out var posE))
            {
                switch (posE)
                {
                    case AnchorPosition.RightBottom:
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case AnchorPosition.LeftBottom:
                        Cursor = Cursors.SizeNESW;
                        break;
                    case AnchorPosition.LeftTop:
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case AnchorPosition.Left:
                        Cursor = Cursors.SizeWE;
                        break;
                    case AnchorPosition.RightTop:
                        Cursor = Cursors.SizeNESW;
                        break;
                    case AnchorPosition.Right:
                        Cursor = Cursors.SizeWE;
                        break;
                    case AnchorPosition.Top:
                        Cursor = Cursors.SizeNS;
                        break;
                    case AnchorPosition.Bottom:
                        Cursor = Cursors.SizeNS;
                        break;
                }
            }
            else if (transformHelper.IsHit(mousePos, zoom))
                Cursor = Cursors.SizeAll;
            else
                ResetCursor();
        }

        public override void ResetCursor()
        {
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    Cursor = Cursors.Cross;
                    break;
                case ToolType.Pointer:
                    Cursor = Cursors.Default;
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
                    foreach(ToolStripMenuItem ob in shapeToolStripMenuItem.DropDownItems)
                    {
                        ob.CheckState = CheckState.Unchecked;
                    }
                    item.CheckState = CheckState.Indeterminate;
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
                    for(int i = 0; i < Enum.GetValues(typeof(ToolType)).Length; i++)
                    {
                        ((ToolStripMenuItem)toolsToolStripMenuItem.DropDownItems[i]).CheckState = CheckState.Unchecked;
                    }
                    item.CheckState = CheckState.Indeterminate;
                }
            }
        }

        private void penSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenPropertiesEditor dialog = new PenPropertiesEditor(Shape.DefaultPen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Shape.DefaultPen = dialog.Pen;
                pictureBox.Invalidate();
            }
        }

        private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrushPropertiesEditor dialog = new BrushPropertiesEditor(Shape.DefaultBrushHolder);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var b = dialog.BrushHolder;
                if (b == null)
                    return;
                Shape.DefaultBrushHolder = b;
                pictureBox.Invalidate();
            }
        }

        private void RemoveItems()
        {
            if (TryXmlSerializeDataToString(out var xml))
                undoRedoHelper.AddItem(xml);
            foreach (var ob in transformHelper)
            {
                shapes.Remove(ob);
                ob.Dispose();
            }
            transformHelper.Clear();
            UpdateCursor();
        }

        private void CutItems()
        {
            if (!transformHelper.Any())
                return;
            buffer.Clear();
            buffer.AddRange(transformHelper.Select(i => (Shape)i.Clone()));
            RemoveItems();
            pictureBox.Invalidate();
        }

        private void PasteItems()
        {
            if (!buffer.Any())
                return;
            if (TryXmlSerializeDataToString(out var xml))
                undoRedoHelper.AddItem(xml);
            transformHelper.Clear();
            shapes.AddRange(buffer.Select(i =>
            {
                var clone = (Shape)i.Clone();
                transformHelper.Add(clone);
                return clone;
            }));
            UpdateCursor();
            pictureBox.Invalidate();
        }

        private void CopyItems()
        {
            if (!transformHelper.Any())
                return;
            buffer.Clear();
            buffer.AddRange(transformHelper.Select(i => (Shape)i.Clone()));
        }

        private void BringToFrontItems()
        {
            if (shapes.Count == 1 || !transformHelper.Any())
                return;
            if (TryXmlSerializeDataToString(out var xml))
                undoRedoHelper.AddItem(xml);
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
            pictureBox.Invalidate();
        }

        private void SendToBackItems()
        {
            if (shapes.Count == 1 || !transformHelper.Any())
                return;
            if (TryXmlSerializeDataToString(out var xml))
                undoRedoHelper.AddItem(xml);
            foreach (var ob in transformHelper)
            {
                int index = shapes.IndexOf(ob);
                if (index != -1 && index > 0)
                {
                    shapes.RemoveAt(index);
                    shapes.Insert(index - 1, ob);
                }
            }
            pictureBox.Invalidate();
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
            pictureBox.Invalidate();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transformHelper.Clear();
            transformHelper.AddRange(shapes);
            pictureBox.Invalidate();
        }

        private void resetSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transformHelper.Clear();
            pictureBox.Invalidate();
        }

        private void outlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            var ob = transformHelper.Single();
            PenPropertiesEditor dialog = new PenPropertiesEditor(ob.Pen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ob.Pen = dialog.Pen;
                pictureBox.Invalidate();
            }
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            var ob = transformHelper.Single();
            BrushPropertiesEditor dialog = new BrushPropertiesEditor(ob.BrushHolder);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var b = dialog.BrushHolder;
                if (b == null)
                    return;
                ob.BrushHolder = b;
                pictureBox.Invalidate();
            }
        }

        private void transformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            var ob = transformHelper.Single();
            var dialog = new TransformEditor(canvas.Size, ob.GetBounds().WithoutNegative());
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                ob.Bound(dialog._Bounds);
                pictureBox.Invalidate();
            }
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            var ob = transformHelper.Single();
            TextEditor dialog = new TextEditor(ob.TextFormat);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                ob.TextFormat = dialog.TextFormat;
                pictureBox.Invalidate();
            }
        }

        private void OpenShapePropertiesEditor()
        {
            if (!transformHelper.Any())
                return;
            var dialog = new ShapePropertiesEditor(canvas.Size, transformHelper);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.Transform != null)
                    transformHelper.Bound(dialog.Transform.Value);
                foreach (var ob in transformHelper)
                {
                    if (dialog.Outline != null)
                        ob.Pen = dialog.Outline;
                    if (dialog.Fill != null)
                        ob.BrushHolder = dialog.Fill;
                    if (dialog.TextFormat != null)
                        ob.TextFormat = dialog.TextFormat;
                    if (dialog.DrawOutline != null)
                        ob.UsePen = (bool)dialog.DrawOutline;
                    if (dialog.DrawFill != null)
                        ob.UseBrush = (bool)dialog.DrawFill;
                    if (dialog.DrawText != null)
                        ob.UseText = (bool)dialog.DrawText;
                    if (dialog.FlipX != null)
                        ob.FlipX = (bool)dialog.FlipX;
                    if (dialog.FlipY != null)
                        ob.FlipY = (bool)dialog.FlipY;
                    if (dialog.SmoothingMode != null)
                        ob.SmoothingMode = (SmoothingMode)dialog.SmoothingMode;
                }
                pictureBox.Invalidate();
            }
        }

        private void OpenParallelepipedEditor(object? sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            if (transformHelper.Single() is CubeShape shape)
            {
                var dialog = new CubeEditor(shape);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    shape.Angle = dialog.Angle;
                    pictureBox.Invalidate();
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenShapePropertiesEditor();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool oneOrMoreSelected = transformHelper.Any();
            bool onlyOneSelected = transformHelper.Count == 1;

            propertiesToolStripMenuItem.Enabled = oneOrMoreSelected;
            outlineToolStripMenuItem.Enabled = onlyOneSelected;
            fillToolStripMenuItem.Enabled = onlyOneSelected;
            textToolStripMenuItem.Enabled = onlyOneSelected;
            transformToolStripMenuItem.Enabled = onlyOneSelected;
            cutToolStripMenuItem.Enabled = oneOrMoreSelected;
            copyToolStripMenuItem.Enabled = oneOrMoreSelected;
            pasteToolStripMenuItem.Enabled = buffer.Any();
            deleteToolStripMenuItem.Enabled = oneOrMoreSelected;
            bringToFrontToolStripMenuItem.Enabled = oneOrMoreSelected;
            sendToBackToolStripMenuItem.Enabled = oneOrMoreSelected;

            if(transformHelper.Count == 1 && transformHelper.Single() is CubeShape)
            {
                contextMenuStrip1.Items.Insert(1, new ToolStripMenuItem(nameof(CubeEditor).SplitCamelCase(), null, OpenParallelepipedEditor));
                contextMenuStrip1.Items[1].Tag = true;
            }
        }

        private void contextMenuStrip1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (contextMenuStrip1.Items[1].Tag != null)
                contextMenuStrip1.Items.RemoveAt(1);
        }

        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenShapePropertiesEditor();
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
            pictureBox.Invalidate();
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringToFrontItems();
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToBackItems();
        }

        private bool TryXmlSerializeDataToFile(string? path)
        {
            if (!File.Exists(path))
                return false;
            XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
            try
            {
                using (var s = File.Create(path))
                {
                    serializer.Serialize(s, new MyOrder(shapes.ToArray(), canvas));
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private bool TryXmlSerializeDataToString(out string xml)
        {
            xml = string.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
            try
            {
                using (var s = new StringWriter())
                {
                    serializer.Serialize(s, new MyOrder(shapes.ToArray(), canvas));
                    xml = s.ToString();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private bool TryXmlDeseializeDataFromFile(string? filePath, out MyOrder order)
        {
            order = new MyOrder();
            if (!File.Exists(filePath))
                return false;
            XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
            try
            {
                using (Stream s = File.OpenRead(filePath))
                {
                    if (serializer.Deserialize(s) is MyOrder ob)
                    {
                        order = ob;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.GetType() == typeof(FileNotFoundException))
                {
                    var fileNotFoundExceptionOb = (FileNotFoundException)e.InnerException;
                    MessageBox.Show($"Error: File {fileNotFoundExceptionOb.FileName} was not found.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                    MessageBox.Show($"Error: {e.Message}", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private bool TryXmlDeseializeDataFromString(string xml, out MyOrder order)
        {
            order = new MyOrder();
            XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
            try
            {
                using (var s = new StringReader(xml))
                {
                    if(serializer.Deserialize(s) is MyOrder ob)
                    {
                        order = ob;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.GetType() == typeof(FileNotFoundException))
                {
                    var fileNotFoundExceptionOb = (FileNotFoundException)e.InnerException;
                    MessageBox.Show($"Error: File {fileNotFoundExceptionOb.FileName} was not found.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show($"Error: {e.Message}", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private string GetTitle()
        {
            return $"{fileName} - {ProductName.SplitCamelCase()}";
        }

        public Bitmap GetBitmap()
        {
            Bitmap map = new Bitmap(canvas.Width, canvas.Height);
            using (var g = Graphics.FromImage(map))
            {
                g.Clear(canvas.Color);
                foreach (var ob in shapes)
                {
                    ob.Draw(g);
                }
            }
            return map;
        }

        public bool Export(string fileName)
        {
            try
            {
                using (var map = GetBitmap())
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

        public bool IsSaved()
        {
            if (!File.Exists(filePath))
            {
                return !shapes.Any();
            }
            else
            {
                if (TryXmlDeseializeDataFromFile(filePath, out var order))
                {
                    if (order.Shapes == null)
                        return !(shapes.Count > 0);
                    if (shapes.Count != order.Shapes.Count())
                        return false;
                    if (shapes.Except(order.Shapes).Any())
                        return false;
                    return order.Canvas == canvas;
                }
            }
            return false;
        }

        public bool Save(string fileName)
        {
            if(TryXmlSerializeDataToFile(fileName))
            {
                filePath = fileName;
                Text = GetTitle();
                return true;
            }
            return false;
        }

        public void New()
        {
            if (!shapes.Any())
                return;

            transformHelper.Clear();
            shapes.ForEach(i => i.Dispose());
            shapes.Clear();
            canvas.Reset();
            UpdateCanvas();
            filePath = string.Empty;
            Text = GetTitle();

            pictureBox.Invalidate();
        }

        private bool Save(bool asNew)
        {
            if (!asNew && File.Exists(filePath))
                return Save(filePath);
            else
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = SaveFileFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                    return Save(dialog.FileName);
            }
            return false;
        }

        private void LoadFromOrder(MyOrder order)
        {
            transformHelper.Clear();
            shapes.ForEach(i => i.Dispose());
            shapes.Clear();

            if (order.Shapes != null)
                shapes.AddRange(order.Shapes);
            if(order.Canvas != null)
            {
                canvas = order.Canvas;
                UpdateCanvas();
            }
            pictureBox.Invalidate();
        }

        public bool Open(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show($"Error: file not found.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (TryXmlDeseializeDataFromFile(fileName, out var order))
            {
                filePath = fileName;
                Text = GetTitle();
                LoadFromOrder(order);
                return true;
            }
            return false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                {
                    if (!Save(false))
                        e.Cancel = true;
                }
                else if (res == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = ExportFileFilter;
            dialog.DefaultExt = ".png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Export(dialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                {
                    if (!Save(false))
                        return;
                } 
                else if (res == DialogResult.Cancel)
                    return;
            }
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                {
                    if (!Save(false))
                        return;
                }
                else if (res == DialogResult.Cancel)
                    return;
            }
            var dialog = new OpenFileDialog();
            dialog.Filter = SaveFileFilter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Open(dialog.FileName);
            }
        }

        private void canvasSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasDialog dialog = new CanvasDialog(canvas);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (TryXmlSerializeDataToString(out var xml))
                    undoRedoHelper.AddItem(xml);
                canvas = dialog.Canvas;
                UpdateCanvas();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!undoRedoHelper.CanUndo())
                return;
            if (TryXmlSerializeDataToString(out var xml) 
                && TryXmlDeseializeDataFromString(undoRedoHelper.Undo(xml), out MyOrder order))
                LoadFromOrder(order);
            else
                undoRedoHelper.Clear();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!undoRedoHelper.CanRedo())
                return;
            if (TryXmlSerializeDataToString(out var xml) 
                && TryXmlDeseializeDataFromString(undoRedoHelper.Redo(xml), out MyOrder order))
                LoadFromOrder(order);
            else
                undoRedoHelper.Clear();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = undoRedoHelper.CanUndo();
            redoToolStripMenuItem.Enabled = undoRedoHelper.CanRedo();

            bool oneOrMoreSelected = transformHelper.Any();

            propertiesToolStripMenuItem1.Enabled = oneOrMoreSelected;
            cutToolStripMenuItem1.Enabled = oneOrMoreSelected;
            copyToolStripMenuItem1.Enabled = oneOrMoreSelected;
            pasteToolStripMenuItem1.Enabled = buffer.Any();
            deleteToolStripMenuItem1.Enabled = oneOrMoreSelected;
            selectAllToolStripMenuItem.Enabled = shapes.Any();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float tmp = zoom * 1.15f;
            if (tmp > 4f)
                tmp = 4f;
            zoom = tmp;
            UpdateWorkingScreen();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float tmp = zoom * 0.85f;
            if (tmp < 0.1f)
                tmp = 0.1f;
            zoom = tmp;
            UpdateWorkingScreen();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new ZoomFactorEditor(Convert.ToDecimal(zoom));
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                zoom = (float)dialog.ScaleFactor;
                UpdateWorkingScreen();
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom = 1f;
            UpdateWorkingScreen();
        }
    }
}