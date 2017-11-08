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
    public partial class WindowHostingVisual : Window
    {
        readonly  DrawingVisual _ghostVisual = null;

        public WindowHostingVisual()
        {
            InitializeComponent();
            
            Title = "Hosting DrawingVisuals";
            Width = 300;
            Height = 350;

            _ghostVisual = new DrawingVisual();
            using (DrawingContext dc = _ghostVisual.RenderOpen())
            {
                // The Body
                dc.DrawGeometry(Brushes.Blue, null,
                    Geometry.Parse(
                        @"M 240,250
                        C 200, 375 200, 250 175, 200
                        C 100, 400 100, 250 100, 200
                        C 0, 350   0, 250   30, 130
                        C 75, 0    100, 0   150, 0
                        C 200, 0   250, 0   250, 150 Z"));

                // Left Eye
                dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
                    new Point(95, 95), 15, 15);

                // Right Eye
                dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
                    new Point(170, 105), 15, 15);

                // The Mouth
                Pen p = new Pen(Brushes.Black, 10);
                p.StartLineCap = PenLineCap.Round;
                p.EndLineCap = PenLineCap.Round;
                dc.DrawLine(p, new Point(75, 160), new Point(175, 150));
            }

            // Bookkeeping
            AddVisualChild(_ghostVisual);
            AddLogicalChild(_ghostVisual);
        }

        // The two necessary overrides, implemented for the single Visual

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        /// <value>The visual children count.</value>
        protected override int VisualChildrenCount => 1;

        /// <summary>
        /// Overrides <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)" />, and returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        /// <returns>The requested child element. This should not return null; if the provided index is out of range, an exception is thrown.</returns>
        /// <exception cref="ArgumentOutOfRangeException">index</exception>
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            return _ghostVisual;
        }
    }
}
