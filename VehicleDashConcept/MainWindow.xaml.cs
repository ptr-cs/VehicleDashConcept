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
using VehicleDashConcept.ViewModel;

namespace VehicleDashConcept
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DashboardViewModel dashboardViewModel;

        public MainWindow()
        {
            InitializeComponent();

            dashboardViewModel = new DashboardViewModel(mainNavigationFrame);

            this.DataContext = dashboardViewModel;
        }

        private void MainNavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ((FrameworkElement)e.Content).DataContext = dashboardViewModel;
        }
    }
}
