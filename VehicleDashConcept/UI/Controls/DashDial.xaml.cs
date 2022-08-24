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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VehicleDashConcept.UI.Controls
{
    /// <summary>
    /// Interaction logic for DashDial.xaml
    /// </summary>
    /// 

    public partial class DashDial : UserControl
    {
        public DashDial()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(DashDial), new PropertyMetadata(0.0));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /* Faceplate --------------------- */

        public static readonly DependencyProperty FaceplateStrokeProperty =
            DependencyProperty.Register(nameof(FaceplateStroke), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush FaceplateStroke
        {
            get { return (Brush)GetValue(FaceplateStrokeProperty); }
            set { SetValue(FaceplateStrokeProperty, value); }
        }

        public static readonly DependencyProperty FaceplateStrokeThicknessProperty =
            DependencyProperty.Register(nameof(FaceplateStrokeThickness), typeof(double), typeof(DashDial), new PropertyMetadata(1.0));

        public double FaceplateStrokeThickness
        {
            get { return (double)GetValue(FaceplateStrokeThicknessProperty); }
            set { SetValue(FaceplateStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty FaceplateFillProperty =
            DependencyProperty.Register(nameof(FaceplateFill), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush FaceplateFill
        {
            get { return (Brush)GetValue(FaceplateFillProperty); }
            set { SetValue(FaceplateFillProperty, value); }
        }

        public static readonly DependencyProperty FaceplateMarginProperty =
            DependencyProperty.Register(nameof(FaceplateMargin), typeof(Thickness), typeof(DashDial), new PropertyMetadata(new Thickness(0)));

        public Thickness FaceplateMargin
        {
            get { return (Thickness)GetValue(FaceplateMarginProperty); }
            set { SetValue(FaceplateMarginProperty, value); }
        }

        /* End Faceplate --------------------- */

        /* Inner Bezel --------------------- */

        public static readonly DependencyProperty BezelInnerStrokeProperty =
            DependencyProperty.Register(nameof(BezelInnerStroke), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush BezelInnerStroke
        {
            get { return (Brush)GetValue(BezelInnerStrokeProperty); }
            set { SetValue(BezelInnerStrokeProperty, value); }
        }

        public static readonly DependencyProperty BezelInnerStrokeThicknessProperty =
            DependencyProperty.Register(nameof(BezelInnerStrokeThickness), typeof(double), typeof(DashDial), new PropertyMetadata(1.0));

        public double BezelInnerStrokeThickness
        {
            get { return (double)GetValue(BezelInnerStrokeThicknessProperty); }
            set { SetValue(BezelInnerStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty BezelInnerFillProperty =
            DependencyProperty.Register(nameof(BezelInnerFill), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush BezelInnerFill
        {
            get { return (Brush)GetValue(BezelInnerFillProperty); }
            set { SetValue(BezelInnerFillProperty, value); }
        }

        public static readonly DependencyProperty BezelInnerMarginProperty =
            DependencyProperty.Register(nameof(BezelInnerMargin), typeof(Thickness), typeof(DashDial), new PropertyMetadata(new Thickness(0)));

        public Thickness BezelInnerMargin
        {
            get { return (Thickness)GetValue(BezelInnerMarginProperty); }
            set { SetValue(BezelInnerMarginProperty, value); }
        }

        /* End Inner Bezel --------------------- */

        /* Outer Bezel --------------------- */

        public static readonly DependencyProperty BezelOuterStrokeProperty =
            DependencyProperty.Register(nameof(BezelOuterStroke), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush BezelOuterStroke
        {
            get { return (Brush)GetValue(BezelOuterStrokeProperty); }
            set { SetValue(BezelOuterStrokeProperty, value); }
        }

        public static readonly DependencyProperty BezelOuterStrokeThicknessProperty =
            DependencyProperty.Register(nameof(BezelOuterStrokeThickness), typeof(double), typeof(DashDial), new PropertyMetadata(1.0));

        public double BezelOuterStrokeThickness
        {
            get { return (double)GetValue(BezelOuterStrokeThicknessProperty); }
            set { SetValue(BezelOuterStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty BezelOuterFillProperty =
            DependencyProperty.Register(nameof(BezelOuterFill), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush BezelOuterFill
        {
            get { return (Brush)GetValue(BezelOuterFillProperty); }
            set { SetValue(BezelOuterFillProperty, value); }
        }

        /* End Outer Bezel --------------------- */

        /* Needle Housing --------------------- */

        public static readonly DependencyProperty NeedleHousingStrokeProperty =
            DependencyProperty.Register(nameof(NeedleHousingStroke), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush NeedleHousingStroke
        {
            get { return (Brush)GetValue(NeedleHousingStrokeProperty); }
            set { SetValue(NeedleHousingStrokeProperty, value); }
        }

        public static readonly DependencyProperty NeedleHousingStrokeThicknessProperty =
            DependencyProperty.Register(nameof(NeedleHousingStrokeThickness), typeof(double), typeof(DashDial), new PropertyMetadata(1.0));

        public double NeedleHousingStrokeThickness
        {
            get { return (double)GetValue(NeedleHousingStrokeThicknessProperty); }
            set { SetValue(NeedleHousingStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty NeedleHousingFillProperty =
            DependencyProperty.Register(nameof(NeedleHousingFill), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush NeedleHousingFill
        {
            get { return (Brush)GetValue(NeedleHousingFillProperty); }
            set { SetValue(NeedleHousingFillProperty, value); }
        }

        public static readonly DependencyProperty NeedleHousingWidthProperty =
            DependencyProperty.Register(nameof(NeedleHousingWidth), typeof(double), typeof(DashDial), new PropertyMetadata(20.0));

        public double NeedleHousingWidth
        {
            get { return (double)GetValue(NeedleHousingWidthProperty); }
            set { SetValue(NeedleHousingWidthProperty, value); }
        }

        /* End Needle Housing --------------------- */

        /* Needle Cap --------------------- */

        public static readonly DependencyProperty NeedleCapStrokeProperty =
            DependencyProperty.Register(nameof(NeedleCapStroke), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush NeedleCapStroke
        {
            get { return (Brush)GetValue(NeedleCapStrokeProperty); }
            set { SetValue(NeedleCapStrokeProperty, value); }
        }

        public static readonly DependencyProperty NeedleCapStrokeThicknessProperty =
            DependencyProperty.Register(nameof(NeedleCapStrokeThickness), typeof(double), typeof(DashDial), new PropertyMetadata(1.0));

        public double NeedleCapStrokeThickness
        {
            get { return (double)GetValue(NeedleCapStrokeThicknessProperty); }
            set { SetValue(NeedleCapStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty NeedleCapFillProperty =
            DependencyProperty.Register(nameof(NeedleCapFill), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush NeedleCapFill
        {
            get { return (Brush)GetValue(NeedleCapFillProperty); }
            set { SetValue(NeedleCapFillProperty, value); }
        }

        public static readonly DependencyProperty NeedleCapWidthProperty =
            DependencyProperty.Register(nameof(NeedleCapWidth), typeof(double), typeof(DashDial), new PropertyMetadata(6.0));

        public double NeedleCapWidth
        {
            get { return (double)GetValue(NeedleCapWidthProperty); }
            set { SetValue(NeedleCapWidthProperty, value); }
        }

        /* End Needle Cap --------------------- */

        /* Needle --------------------- */

        public static readonly DependencyProperty NeedleWidthProperty =
            DependencyProperty.Register(nameof(NeedleWidth), typeof(double), typeof(DashDial), new PropertyMetadata(4.0));

        public double NeedleWidth
        {
            get { return (double)GetValue(NeedleWidthProperty); }
            set { SetValue(NeedleWidthProperty, value); }
        }

        public static readonly DependencyProperty NeedleHeightProperty =
            DependencyProperty.Register(nameof(NeedleHeight), typeof(double), typeof(DashDial), new PropertyMetadata(60.0));

        public double NeedleHeight
        {
            get { return (double)GetValue(NeedleHeightProperty); }
            set { SetValue(NeedleHeightProperty, value); }
        }

        public static readonly DependencyProperty NeedleMarginProperty =
            DependencyProperty.Register(nameof(NeedleMargin), typeof(Thickness), typeof(DashDial), new PropertyMetadata(new Thickness(0,60,0,0)));

        public Thickness NeedleMargin
        {
            get { return (Thickness)GetValue(NeedleMarginProperty); }
            set { SetValue(NeedleMarginProperty, value); }
        }

        public static readonly DependencyProperty NeedleStrokeProperty =
            DependencyProperty.Register(nameof(NeedleStroke), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush NeedleStroke
        {
            get { return (Brush)GetValue(NeedleStrokeProperty); }
            set { SetValue(NeedleStrokeProperty, value); }
        }

        public static readonly DependencyProperty NeedleStrokeThicknessProperty =
            DependencyProperty.Register(nameof(NeedleStrokeThickness), typeof(double), typeof(DashDial), new PropertyMetadata(1.0));

        public double NeedleStrokeThickness
        {
            get { return (double)GetValue(NeedleStrokeThicknessProperty); }
            set { SetValue(NeedleStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty NeedleFillProperty =
            DependencyProperty.Register(nameof(NeedleFill), typeof(Brush), typeof(DashDial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush NeedleFill
        {
            get { return (Brush)GetValue(NeedleFillProperty); }
            set { SetValue(NeedleFillProperty, value); }
        }
    }
}
