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

        }

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Brush brush = Brushes.Orange;
            Pen pen = new Pen();        // Fabricate a Pen based on all the StrokeXXX properties
            Rect rect = new Rect();     // Layout determines the size of this rectangle

            drawingContext.DrawGeometry(brush, pen, new EllipseGeometry(rect));

            double radiusX = 10;
            double radiusY = 30;
            drawingContext.DrawRoundedRectangle(brush, pen, rect, radiusX, radiusY);
        }
    }
}
