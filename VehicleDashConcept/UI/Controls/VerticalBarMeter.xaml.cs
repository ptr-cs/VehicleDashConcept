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

namespace VehicleDashConcept.UI.Controls
{
    /// <summary>
    /// Interaction logic for VerticalBarMeter.xaml
    /// </summary>
    /// 
    public partial class VerticalBarMeter : BarMeter
    {

        public VerticalBarMeter()
        {
            InitializeComponent();

            Redraw();
        }

        public override void Redraw()
        {
            barContainer.Children.Clear();
            barContainer.RowDefinitions.Clear();

            meterBackgroundOpactiyMask.Children.Clear();
            meterBackgroundOpactiyMask.RowDefinitions.Clear();

            meterFillOpactiyMask.Children.Clear();
            meterFillOpactiyMask.RowDefinitions.Clear();

            for (int i = 0; i < Bars; ++i)
            {
                Border bBack = new Border
                {
                    BorderThickness = BarMargin,
                    BorderBrush = BlackBrush,
                    Background = TransparentBrush
                };
                Grid.SetRow(bBack, i);

                meterBackgroundOpactiyMask.RowDefinitions.Add(new RowDefinition());
                meterBackgroundOpactiyMask.Children.Add(bBack);

                Border bFill = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = TransparentBrush
                };
                double barValue = CalculateBarValue(i);
                bFill.Background = CalculateBarOpacityMask(barValue);
                bFill.Margin = BarMargin;
                Grid.SetRow(bFill, i);

                meterFillOpactiyMask.RowDefinitions.Add(new RowDefinition());
                meterFillOpactiyMask.Children.Add(bFill);

                Border b = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = BarBorderBrush,
                    Margin = BarMargin
                };
                Grid.SetRow(b, i);

                barContainer.RowDefinitions.Add(new RowDefinition());
                barContainer.Children.Add(b);
            }
        }

        public override void RedrawValue()
        {
            for (int i = 0; i < Bars; ++i)
            {
                double barValue = CalculateBarValue(i);
                ((Border)meterFillOpactiyMask.Children[i]).Background = CalculateBarOpacityMask(barValue);
            }
        }
    }
}
