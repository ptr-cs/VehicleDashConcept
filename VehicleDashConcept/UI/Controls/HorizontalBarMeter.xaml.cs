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
    /// Interaction logic for HorizontalBarMeter.xaml
    /// </summary>
    public partial class HorizontalBarMeter : BarMeter
    {
        public HorizontalBarMeter()
        {
            InitializeComponent();

            Redraw();
        }

        public override void Redraw()
        {
            barContainer.Children.Clear();
            barContainer.ColumnDefinitions.Clear();

            meterBackgroundOpactiyMask.Children.Clear();
            meterBackgroundOpactiyMask.ColumnDefinitions.Clear();

            meterFillOpactiyMask.Children.Clear();
            meterFillOpactiyMask.ColumnDefinitions.Clear();

            for (int i = 0; i < Bars; ++i)
            {
                Border bBack = new Border
                {
                    BorderThickness = BarMargin,
                    BorderBrush = BlackBrush,
                    Background = TransparentBrush
                };
                Grid.SetColumn(bBack, i);

                meterBackgroundOpactiyMask.ColumnDefinitions.Add(new ColumnDefinition());
                meterBackgroundOpactiyMask.Children.Add(bBack);

                Border bFill = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = TransparentBrush
                };
                double barValue = CalculateBarValue(i);
                bFill.Background = CalculateBarOpacityMask(barValue);
                bFill.Margin = BarMargin;
                Grid.SetColumn(bFill, i);

                meterFillOpactiyMask.ColumnDefinitions.Add(new ColumnDefinition());
                meterFillOpactiyMask.Children.Add(bFill);

                Border b = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = BarBorderBrush,
                    Margin = BarMargin
                };
                Grid.SetColumn(b, i);

                barContainer.ColumnDefinitions.Add(new ColumnDefinition());
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
