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
    /// Interaction logic for InterstateSign.xaml
    /// </summary>
    public partial class InterstateSign : UserControl
    {
        public InterstateSign()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StateStringProperty =
            DependencyProperty.Register(nameof(StateString), typeof(string), typeof(InterstateSign), new PropertyMetadata(""));

        public string StateString
        {
            get { return (string)GetValue(StateStringProperty); }
            set { SetValue(StateStringProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register(nameof(Number), typeof(string), typeof(InterstateSign), new PropertyMetadata("123"));

        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
    }
}
