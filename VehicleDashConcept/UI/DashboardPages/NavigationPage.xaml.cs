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
using VehicleDashConcept.UI.Utilities;

namespace VehicleDashConcept.UI.DashboardPages
{
    /// <summary>
    /// Interaction logic for NavigationPage.xaml
    /// </summary>
    /// 

    public partial class NavigationPage : Page
    {
        // When dragging the image to create a panning action, the delta of mouse location before and after
        // the movement is used to calculate the TranslateTransform amount.
        Point start;
        Point origin;

        public NavigationPage()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TranslateTransform translateTransform = 
                (TranslateTransform)((TransformGroup)mapImage.RenderTransform).Children.First(t => t is TranslateTransform);

            start = e.GetPosition(mapImageContainer);
            origin = new Point(translateTransform.X, translateTransform.Y);

            mapImage.CaptureMouse();
        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            const double MIN_ZOOM = 3.7;
            const double MAX_ZOOM = 6;

            ScaleTransform scaleTransform = 
                (ScaleTransform)((TransformGroup)mapImage.RenderTransform).Children.First(s => s is ScaleTransform);

            ImageUtilities.BoundedImageScale(scaleTransform, MIN_ZOOM, MAX_ZOOM, e.Delta);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {

            if (mapImage.IsMouseCaptured)
            {
                TranslateTransform translateTransform = 
                    (TranslateTransform)((TransformGroup)mapImage.RenderTransform).Children.First(t => t is TranslateTransform);

                Vector v = start - e.GetPosition(mapImageContainer);

                double xAxisMovement = origin.X - v.X;
                double yAxisMovement = origin.Y - v.Y;

                ImageUtilities.BoundedImageTranslate(
                    translateTransform, -50, 50, -50, 50, xAxisMovement, yAxisMovement);
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mapImage.ReleaseMouseCapture();
        }
    }
}
