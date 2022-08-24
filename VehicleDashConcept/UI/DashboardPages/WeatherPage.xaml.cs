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
    /// Interaction logic for WeatherPage.xaml
    /// </summary>
    public partial class WeatherPage : Page
    {
        public WeatherPage()
        {
            InitializeComponent();
        }

        // When dragging the image to create a panning action, the delta of mouse location before and after
        // the movement is used to calculate the TranslateTransform amount.
        Point start;
        Point origin;

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            const double MIN_ZOOM = 2.5;
            const double MAX_ZOOM = 3.5;

            ScaleTransform scaleTransform = (ScaleTransform)((TransformGroup)weatherRadarImage.RenderTransform).Children.First(s => s is ScaleTransform);
            ImageUtilities.BoundedImageScale(scaleTransform, MIN_ZOOM, MAX_ZOOM, e.Delta);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TranslateTransform translateTransform = (TranslateTransform)((TransformGroup)weatherRadarImage.RenderTransform).Children.First(t => t is TranslateTransform);
            start = e.GetPosition(weatherRadarImageContainer);
            origin = new Point(translateTransform.X, translateTransform.Y);
            weatherRadarImage.CaptureMouse();
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (weatherRadarImage.IsMouseCaptured)
            {
                TranslateTransform translateTransform = (TranslateTransform)((TransformGroup)weatherRadarImage.RenderTransform).Children.First(t => t is TranslateTransform);
                Vector v = start - e.GetPosition(weatherRadarImageContainer);

                double xAxisMovement = origin.X - v.X;
                double yAxisMovement = origin.Y - v.Y;

                ImageUtilities.BoundedImageTranslate(
                    translateTransform, -50, 50, -50, 50, xAxisMovement, yAxisMovement);
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            weatherRadarImage.ReleaseMouseCapture();
        }
    }
}
