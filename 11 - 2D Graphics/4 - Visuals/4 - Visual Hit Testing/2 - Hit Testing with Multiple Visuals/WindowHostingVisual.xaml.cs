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
        private List<Visual> visuals = new List<Visual>();

        public WindowHostingVisual()
        {
            InitializeComponent();
            
            Title = "Hosting DrawingVisuals";
            Width = 300;
            Height = 350;

            DrawingVisual bodyVisual = new DrawingVisual();
            DrawingVisual eyesVisual = new DrawingVisual();
            DrawingVisual mouthVisual = new DrawingVisual();

            using (DrawingContext dc = bodyVisual.RenderOpen())
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
            }


            using (DrawingContext dc = eyesVisual.RenderOpen())
            {

                // Left Eye
                dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
                    new Point(95, 95), 15, 15);

                // Right Eye
                dc.DrawEllipse(Brushes.Black, new Pen(Brushes.White, 10),
                    new Point(170, 105), 15, 15);
            }

            using (DrawingContext dc = mouthVisual.RenderOpen())
            {
                // The Mouth
                Pen p = new Pen(Brushes.Black, 10);
                p.StartLineCap = PenLineCap.Round;
                p.EndLineCap = PenLineCap.Round;
                dc.DrawLine(p, new Point(75, 160), new Point(175, 150));
            }

            visuals.Add(bodyVisual);
            visuals.Add(eyesVisual);
            visuals.Add(mouthVisual);

            // Bookkeeping
            foreach (Visual visual in visuals)
            {
                AddVisualChild(visual);
                AddLogicalChild(visual);
            }
        }

        // The two necessary overrides, implemented for the single Visual

        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        /// <value>The visual children count.</value>
        protected override int VisualChildrenCount => visuals.Count;

        /// <summary>
        /// Overrides <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)" />, and returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        /// <returns>The requested child element. This should not return null; if the provided index is out of range, an exception is thrown.</returns>
        /// <exception cref="ArgumentOutOfRangeException">index</exception>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= visuals.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return visuals[index];
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Retrieve the mouse pointer location relative to the window
            Point location = e.GetPosition(this);

            // Perform visual hit testing for the entire Window
            HitTestResult result = VisualTreeHelper.HitTest(this, location);

            // If we hit the ghostVisual, rotate it
            if (result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                DrawingVisual dv = result.VisualHit as DrawingVisual;
                if (dv.Transform == null)
                    dv.Transform = new RotateTransform();

                (dv.Transform as RotateTransform).Angle++;
            }
        }
    }
}
