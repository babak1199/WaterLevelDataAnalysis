using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using Microsoft.Research.DynamicDataDisplay.Charts;
using System.Collections.ObjectModel;
using WaterLevelData;


namespace WLDataAnalysis
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class ChartPage : UserControl
    {
        public LineAndMarker<ElementMarkerPointsGraph> markerGraph;
        public LineGraph WLTimeSeriesChart;

        public ChartPage()
        {
            InitializeComponent();
        }

        public void showChart(List<WLData> data)
        {
            plotter.Legend.LegendLeft = 10;
            plotter.Legend.LegendRight = Double.NaN;

            Color[] colors = ColorHelper.CreateRandomColors(1);
            WLTimeSeriesChart = plotter.AddLineGraph(CreateWLDataSource(data), Colors.Blue,
                                                                                    2, "Water Level");
            dateAxis.Width *= 2;
            CursorCoordinateGraph coordGraph = new CursorCoordinateGraph();
            coordGraph.XTextMapping = x => dateAxis.ConvertFromDouble(x).ToShortDateString() + " " +
                                        dateAxis.ConvertFromDouble(x).ToShortTimeString();
            plotter.Children.Add(coordGraph);
            plotter.FitToView();
        }

        private EnumerableDataSource<WLData> CreateWLDataSource(List<WLData> values)
        {
            EnumerableDataSource<WLData> ds = new EnumerableDataSource<WLData>(values);

            try
            {
                ds.SetXMapping(ci => dateAxis.ConvertToDouble((ci != null) ? ci.Date : new DateTime()));
                ds.SetYMapping(ci => (ci != null) ? ci.Value : 0);
            }
            catch(Exception) { }

            return ds;
        }

        public void ShowHideMarker()
        {
            if (markerGraph != null)
            {
                if (markerGraph.MarkerGraph.Visibility == System.Windows.Visibility.Visible)
                    markerGraph.MarkerGraph.Visibility = System.Windows.Visibility.Hidden;
                else
                    markerGraph.MarkerGraph.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void ClearChart(bool all = true)
        {
            for (int i = plotter.Children.Count - 1; i > 0; i--)
            {
                if (plotter.Children[i] is LineGraph)
                {
                    if (!all)
                    {
                        if ((plotter.Children[i] as LineGraph).Description.ToString().Contains("Water Level"))
                            continue;
                    }
                    plotter.Children.RemoveAt(i);
                }
                else if (plotter.Children[i] is MarkerPointsGraph ||
                    plotter.Children[i] is ElementMarkerPointsGraph || plotter.Children[i] is HorizontalRange)
                {
                    plotter.Children.RemoveAt(i);
                }
            }
        }

        public void ShowPointsOnChart(List<WLData> pointToBeRemoved)
        {
            EnumerableDataSource<WLData> ds =
                                    new EnumerableDataSource<WLData>(pointToBeRemoved);
            ds.SetXMapping(ci => dateAxis.ConvertToDouble(ci.Date));
            ds.SetYMapping(ci => ci.Value);

            markerGraph = plotter.AddLineGraph(ds, new Pen(Brushes.LimeGreen, 0),
                                                                new CircleElementPointMarker
                                                                {
                                                                    Size = 5,
                                                                    Brush = Brushes.Red,
                                                                    Fill = Brushes.Orange
                                                                },
                                                                new PenDescription("Points to be removed"));

            WLTimeSeriesChart.ToolTip = "Water level time series";
        }

        public void ShowRangesOnChart(double SensorElevation, double MaxLevelVariation)
        {
            HorizontalRange verRangeUp = new HorizontalRange()
            {
                Value1 = SensorElevation + MaxLevelVariation,
                Value2 = 100,
                Fill = Brushes.Fuchsia
            };
            HorizontalRange verRangeDown = new HorizontalRange()
            {
                Value1 = SensorElevation - MaxLevelVariation,
                Value2 = -100,
                Fill = Brushes.Fuchsia
            };

            plotter.Children.Add(verRangeUp);
            plotter.Children.Add(verRangeDown);
            plotter.FitToView();

        }

        public void ShowUpperLowerBoundOnChart(List<WLData> AverageTrend,
                                    List<WLData> DownBound, List<WLData> UpBound)
        {
            EnumerableDataSource<WLData> ds = CreateWLDataSource(AverageTrend);
            var AverageTrendChart = plotter.AddLineGraph(ds, Colors.Red, 1, "Average Trend");
            ds = CreateWLDataSource(UpBound);
            plotter.AddLineGraph(ds, Colors.Green, 1, "Upper Bound");
            ds = CreateWLDataSource(DownBound);
            plotter.AddLineGraph(ds, Colors.Green, 1, "Lower Bound");
        }

        public void ShowSeriesOnChart(List<WLData> data, string name, Color color, bool keepCharts = false)
        {
            EnumerableDataSource<WLData> ds = CreateWLDataSource(data);
            if (keepCharts)
                color = ColorHelper.CreateRandomColors(1)[0];
            WLTimeSeriesChart = plotter.AddLineGraph(ds, color, 2, name);
        }
    }
}
