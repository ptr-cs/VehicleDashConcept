using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VehicleDashConcept.UI.Controls
{
    public class BarMeter : UserControl
    {
        public SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);

        public BarMeter() : base() { }

        public virtual void Redraw() { }

        public virtual void RedrawValue() { }

        public double CalculateBarValue(int barIndex)
        {
            double valueThreshold = (Bars > 0) ? (double)(Maximum / Bars) : 0;
            double barValue = (valueThreshold * barIndex) + valueThreshold;
            return barValue;
        }

        public Brush CalculateBarOpacityMask(double barValue)
        {
            if (barValue <= Value)
            {
                return BlackBrush;
            }
            return TransparentBrush;
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(BarMeter), new PropertyMetadata(0, new PropertyChangedCallback(OnValueChanged)));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(BarMeter), new PropertyMetadata(100, new PropertyChangedCallback(OnValueChanged)));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty BarsProperty =
            DependencyProperty.Register(nameof(Bars), typeof(int), typeof(BarMeter), new PropertyMetadata(3, new PropertyChangedCallback(OnMeterChanged)));

        public int Bars
        {
            get { return (int)GetValue(BarsProperty); }
            set { SetValue(BarsProperty, value); }
        }

        public static readonly DependencyProperty BarMarginProperty =
            DependencyProperty.Register(nameof(BarMargin), typeof(Thickness), typeof(BarMeter), new PropertyMetadata(new Thickness(1), new PropertyChangedCallback(OnMeterChanged)));

        public Thickness BarMargin
        {
            get { return (Thickness)GetValue(BarMarginProperty); }
            set { SetValue(BarMarginProperty, value); }
        }

        public static readonly DependencyProperty MeterBackgroundProperty =
            DependencyProperty.Register(nameof(MeterBackground), typeof(Brush), typeof(BarMeter), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Brush MeterBackground
        {
            get { return (Brush)GetValue(MeterBackgroundProperty); }
            set { SetValue(MeterBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BarBorderBrushProperty =
            DependencyProperty.Register(nameof(BarBorderBrush), typeof(Brush), typeof(BarMeter), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public Brush BarBorderBrush
        {
            get { return (Brush)GetValue(BarBorderBrushProperty); }
            set { SetValue(BarBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MeterFillProperty =
            DependencyProperty.Register(nameof(MeterFill), typeof(Brush), typeof(BarMeter), new PropertyMetadata(new SolidColorBrush(Colors.SpringGreen)));

        public Brush MeterFill
        {
            get { return (Brush)GetValue(MeterFillProperty); }
            set { SetValue(MeterFillProperty, value); }
        }

        private static void OnMeterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarMeter vbm = (BarMeter)d;
            vbm.Redraw();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BarMeter vbm = (BarMeter)d;
            vbm.RedrawValue();
        }
    }
}
