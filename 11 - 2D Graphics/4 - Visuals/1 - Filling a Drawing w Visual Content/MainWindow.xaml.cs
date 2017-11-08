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

        public void DrawVisuals()
        {
            DrawingGroup ghostDrawing = FindResource("ghostDrawing") as DrawingGroup;
            DrawingVisual ghostVisual = new DrawingVisual();
            using (DrawingContext dc = ghostVisual.RenderOpen())
            {
                dc.DrawDrawing(ghostDrawing);
            }

            GeometryDrawing bodyDrawing = FindResource("bodyDrawing") as GeometryDrawing;
            GeometryDrawing eyesDrawing = FindResource("eyesDrawing") as GeometryDrawing;
            GeometryDrawing mouthDrawing = FindResource("mouthDrawing") as GeometryDrawing;
            DrawingVisual ghostVisualParts = new DrawingVisual();
            using (DrawingContext dc = ghostVisualParts.RenderOpen())
            {
                dc.DrawDrawing(bodyDrawing);
                dc.DrawDrawing(eyesDrawing);
                dc.DrawDrawing(mouthDrawing);
            }

        }
    }
}
