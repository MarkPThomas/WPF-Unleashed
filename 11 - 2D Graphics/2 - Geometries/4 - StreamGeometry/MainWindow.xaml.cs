using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUnleashed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawStreamGeometry();
        }

        public void DrawStreamGeometry()
        {
            // This is more efficient than using PathGeometry in XAML as this is stored as a compact byte strea,
            // rather than a graph of .net objects.
            // Rather than creating LineSegments, ArcSegment, BezierSegments, and so on, you call methods such as:
            // LineTo, ArcTo, and BezierTo
            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext context = geometry.Open())
            {
                // Triangle #1
                context.BeginFigure(new Point(0, 0), isFilled: true, isClosed: true);
                context.LineTo(new Point(0, 100), isStroked: true, isSmoothJoin: true);
                context.LineTo(new Point(100, 100), isStroked: true, isSmoothJoin: true);

                // Triangle #1
                context.BeginFigure(new Point(70, 0), isFilled: true, isClosed: true);
                context.LineTo(new Point(0, 100), isStroked: true, isSmoothJoin: true);
                context.LineTo(new Point(100, 100), isStroked: true, isSmoothJoin: true);
            }

            // Apply this Geometry to an existing Geometry Drawing
            GeometryDrawing geometryDrawing = new GeometryDrawing
            {
                Pen = new Pen((SolidColorBrush)new BrushConverter().ConvertFromString("Black"), 10),
                Brush = (SolidColorBrush)new BrushConverter().ConvertFromString("Orange"),
                Geometry = geometry
            };

            //
            // Use a DrawingImage and an Image control
            // to display the drawing.
            //
            DrawingImage geometryImage = new DrawingImage(geometryDrawing);

            // Freeze the DrawingImage for performance benefits.
            geometryImage.Freeze();

            Image image = new Image();
            image.Source = geometryImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Left;

            // Add to the WPF
            SpStreamGeometry.Children.Add(image);
        }

    }
}
