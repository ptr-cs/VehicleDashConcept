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

namespace VehicleDashConcept.UI.Controls
{
    /// <summary>
    /// Interaction logic for DashSituationalAwarenessReadout.xaml
    /// </summary>
    public partial class DashSituationalAwarenessReadout : UserControl
    {
        public DashSituationalAwarenessReadout()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ContainerFillProperty =
            DependencyProperty.Register(nameof(ContainerFill), typeof(Brush), typeof(DashSituationalAwarenessReadout), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush ContainerFill
        {
            get { return (Brush)GetValue(ContainerFillProperty); }
            set { SetValue(ContainerFillProperty, value); }
        }
    }
}
