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
using WaterLevelData;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using Microsoft.Research.DynamicDataDisplay.Charts;
using System.Collections.ObjectModel;
using TrendLine;
using Spikes;

namespace WLDataAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<WLData> WLData;
        List<Spike> mSpikes;
        List<int> PointsToBeRemovedIndices;
        List<WLData> WLDataRefined;
        List<WLData> mAverageTrend;
        ChartPage chartPage;

        string FileName;
        float MaxContinuityThreshold;

        bool bRefined;
        bool bDataLoaded;
        bool bKeepOldCharts;

        public MainWindow()
        {
            InitializeComponent();

            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            new SplashWindow().ShowDialog();
            chartPage = new ChartPage();

            mSpikes = new List<Spike>();
            PointsToBeRemovedIndices = null;

            bRefined = false;
            bDataLoaded = false;
            bKeepOldCharts = Properties.Settings.Default.KeepOldCharts;
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            Properties.Settings.Default.KeepOldCharts = chkKeepOldCharts.IsChecked.Value;
            Properties.Settings.Default.IsExpanded = chkIsExpanded.IsChecked.Value;

            Properties.Settings.Default.Save();
        }

        private List<WLData> LoadWLWLData(string fileName)
        {
            string[] strings = null;
            try
            {
                strings = File.ReadAllLines(fileName);
            }
            catch (Exception e)
            {
                throw new Exception("File cannot be read. Error: " + e.Message);
            }

            if (strings.Length == 0)
                return new List<WLData>();

            var res = new List<WLData>(strings.Length - 1);

            string line = strings[0];
            DataFileFormat fileFormat = DetectInputFileFormat(line);

            for (int i = 1; i < strings.Length; i++)
            {
                line = strings[i];
                string[] data = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                DateTime dt = new DateTime();
                float rate = 0;

                try
                {
                    if (fileFormat == DataFileFormat.TideGaugeRawFile)
                    {
                        dt = DateTime.Parse(data[4] + " " + data[3]);
                        rate = float.Parse(data[1]);
                    }
                    else if (fileFormat == DataFileFormat.ExportedSpaceOrTabDelimitedText)
                    {
                        string dateString = null;
                        string rateString = null;

                        if (data.Length == 3)
                        {
                            dateString = data[0] + " " + data[1];
                            rateString = data[2];
                        }
                        if (data.Length == 2)
                        {
                            dateString = data[0];
                            rateString = data[1];
                        }

                        dt = DateTime.Parse(dateString);
                        rate = float.Parse(rateString);
                    }
                }
                catch (Exception)
                {
                    continue;
                }

                res.Add(new WLData { Date = dt, Value = rate });
            }

            return res;
        }

        private DataFileFormat DetectInputFileFormat(string line)
        {
            string[] data = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length == 6 && line.Contains(','))
                return DataFileFormat.TideGaugeRawFile;
            else if (data.Length == 3 && line.Contains(' '))
                return DataFileFormat.ExportedSpaceOrTabDelimitedText;

            return DataFileFormat.Unknown;
        }

        private void OnOpenDataClicked(object sender, RoutedEventArgs e)
        {
            if (bDataLoaded)
            {
                var res = MessageBox.Show("Do you want to clear previous data?", "", 
                                                                            MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    chartPage.ClearChart();
                    WLData = new List<WLData>();
                    WLDataRefined = new List<WLData>();
                    bDataLoaded = false;
                    mSpikes = new List<Spike>();
                    PointsToBeRemovedIndices = null;
                    bRefined = false;
                }
                else
                    return;
            }

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "All Recognized Files|*.txt;*.csv;*.dat|Text Files (*.txt)|*.txt|" +
                            "Comma Delimited Files (*.csv)|*.csv|" +
                            "Data Files (*.dat)|*.dat|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            dlg.Title = "Please Select File to Open";
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                FileName = dlg.FileName;

                try
                {
                    WLData = LoadWLWLData(FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nPlease try again", "Error loading file",
                                                MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                chartPage.showChart(WLData);

                btnCheckInvalid.IsEnabled = true;
                btnFindSpikes.IsEnabled = true;
                btnWriteOutput.IsEnabled = true;

                bDataLoaded = true;

                if (rbSensorElevationAuto.IsChecked == true)
                    CalculateSensorElevationAuto();
            }
        }

        private void CalculateSensorElevationAuto()
        {
            double sum = 0;
            for (int i = 0; i < WLData.Count; i++)
                sum += WLData[i].Value;
            tbSensorElevationAuto.Text = (sum / WLData.Count).ToString("0.00");
        }

        private void OnCheckInvalidDataClicked(object sender, RoutedEventArgs e)
        {
            EnableControlsOnInvalidCheck(true);

            PointsToBeRemovedIndices = new List<int>();
            List<WLData> pointsToBeRemoved = new List<WLData>();
            float MaxLevelVariation = float.Parse(tbMaximumLevelVariation.Text);
            List<WLData> data = new List<WLData>();

            if (bRefined)
                data = WLDataRefined;
            else
                data = WLData;

            if (chkFromSensorElevation.IsChecked == true)
            {
                double SensorElevation = 0;
                if(rbSensorElevationAuto.IsChecked == true)
                    SensorElevation = float.Parse(tbSensorElevationAuto.Text);
                else
                    SensorElevation = float.Parse(tbSensorElevationManual.Text);

                double MaxValidLevel = SensorElevation + MaxLevelVariation;
                double MinValidLevel = SensorElevation - MaxLevelVariation;

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Value > MaxValidLevel || data[i].Value < MinValidLevel)
                    {
                        PointsToBeRemovedIndices.Add(i);
                        pointsToBeRemoved.Add(data[i]);
                    }
                }

                chartPage.ShowRangesOnChart(SensorElevation, MaxLevelVariation);
            }
            else if (chkFromCurveAverage.IsChecked == true)
            {
                int shapeFuncPointNo = int.Parse(tbShapeFuncPointNo.Text);

                TrendLineSpikeRemover zoneDetect = new TrendLineSpikeRemover(data, shapeFuncPointNo);
                mAverageTrend = zoneDetect.CalculateAverageTrend();
                List<WLData> UpBound = new List<WLData>();
                List<WLData> DownBound = new List<WLData>();

                for (int i = 0; i < mAverageTrend.Count; i++)
                {
                    double MaxValidLevel = mAverageTrend[i].Value + MaxLevelVariation;
                    double MinValidLevel = mAverageTrend[i].Value - MaxLevelVariation;
                    UpBound.Add(new WLData(mAverageTrend[i].Date, MaxValidLevel));
                    DownBound.Add(new WLData(mAverageTrend[i].Date, MinValidLevel));

                    if (data[i].Value > MaxValidLevel || data[i].Value < MinValidLevel)
                    {
                        PointsToBeRemovedIndices.Add(i);
                        pointsToBeRemoved.Add(data[i]);
                    }
                }

                chartPage.ShowUpperLowerBoundOnChart(mAverageTrend, DownBound, UpBound);
            }

            chartPage.ShowPointsOnChart(pointsToBeRemoved);
        }

        private void OnRefineDataClicked(object sender, RoutedEventArgs e)
        {
            if (!bRefined)
                WLDataRefined = WLData;

            for (int i = 0; i < PointsToBeRemovedIndices.Count; i++)
                WLDataRefined[PointsToBeRemovedIndices[i]] = null;

            WLDataRefined = WLDataRefined.Where(p => p != null).ToList();

            chartPage.ClearChart(false);
            chartPage.ShowSeriesOnChart(WLDataRefined, "Validated", Colors.Yellow);

            bRefined = true;
            WLData = null;

            EnableControlsOnInvalidCheck(false);
        }

        private void EnableControlsOnInvalidCheck(bool status)
        {
            btnRefineData.IsEnabled = status;
            btnCheckInvalid.IsEnabled = !status;
            btnDespike.IsEnabled = false;
            btnFindSpikes.IsEnabled = !status;
            btnWriteOutput.IsEnabled = !status;
        }

        private void OnFindSpikesClicked(object sender, RoutedEventArgs e)
        {
            EnableControlsOnFindSpikes(true);

            List<WLData> data = new List<WLData>();
            if (bRefined)
                data = WLDataRefined;
            else
                data = WLData;

            PointsToBeRemovedIndices = new List<int>();
            MaxContinuityThreshold = float.Parse(tbMaxContinuityThreshold.Text);
            List<WLData> pointsToBeRemoved = new List<WLData>();

            if (rbDespikeSpike.IsChecked == true)
            {
                pointsToBeRemoved = FindBySpikeMethod(data);
            }
            else if (rbDespikeDifference.IsChecked == true)
            {
                pointsToBeRemoved = FindByDifferenceMethod(data);
            }
            else if (rbDespikeMovAverage.IsChecked == true)
            {
                int shapeFuncPointNo = int.Parse(tbShapeFuncPointNo.Text);

                TrendLineSpikeRemover trendlinedetect = 
                                                new TrendLineSpikeRemover(data, shapeFuncPointNo, true);
                pointsToBeRemoved = trendlinedetect.DetectSpikes(tbPercentile.Text);
                PointsToBeRemovedIndices = trendlinedetect.PointsToBeRemovedIndices;
            }

            chartPage.ShowPointsOnChart(pointsToBeRemoved);
        }

        private List<WLData> FindBySpikeMethod(List<WLData> data)
        {
            List<WLData> pointsToBeRemoved = new List<WLData>();
            List<string> lines = new List<string>();

            for (int i = 1; i < data.Count; i++)
            {
                if (IsMaxContinuityThresholdExceeds(data, i - 1, i))
                {
                    Spike spike = DetectSpike(data, i - 1);

                    if (spike != null)
                    {
                        spike.DeterminePointsToBeDeleted();
                        mSpikes.Add(spike);
                        i += spike.PointNo - 2;
                    }

                    foreach (int idx in spike.PointsToBeRemoved)
                    {
                        PointsToBeRemovedIndices.Add(idx);
                        pointsToBeRemoved.Add(data[idx]);
                    }
                }
            }

            return pointsToBeRemoved;
        }

        private List<WLData> FindByDifferenceMethod(List<WLData> data)
        {
            List<WLData> pointsToBeRemoved = new List<WLData>();

            int i = 0;
            int j = i + 1;
            while (true)
            {
                if (Math.Abs(data[i].Value - data[j].Value) < MaxContinuityThreshold)
                {
                    if (j - i > 1)
                    {
                        if (j < data.Count - 1)
                            j++;
                        else
                            break;
                        i = j - 1;
                    }
                    else
                    {
                        if (i < data.Count - 2)
                            i++;
                        else
                            break;

                        if (j < data.Count - 1)
                            j++;
                        else
                            break;
                    }
                }
                else
                {
                    PointsToBeRemovedIndices.Add(j);
                    pointsToBeRemoved.Add(data[j]);
                    if (j < data.Count - 1)
                        j++;
                    else
                        break;
                }

                btnDespike.IsEnabled = true;
            }

            return pointsToBeRemoved;
        }
        
        private void EnableControlsOnFindSpikes(bool status)
        {
            btnDespike.IsEnabled = status;
            btnRefineData.IsEnabled = false;
            btnCheckInvalid.IsEnabled = !status;
            btnFindSpikes.IsEnabled = !status;
            btnWriteOutput.IsEnabled = !status;
        }

        private void OnDespikeClicked(object sender, RoutedEventArgs e)
        {
            var points = new List<WLData>();

            if (bRefined)
                points = WLDataRefined;
            else
                points = WLData;

            foreach (int idx in PointsToBeRemovedIndices)
                points[idx] = new WLData(new DateTime(), double.NaN);

            points = points.Where(p => !double.IsNaN(p.Value)).ToList();

            WLDataRefined = points;

            chartPage.ClearChart();
            chartPage.ShowSeriesOnChart(WLDataRefined, "Water Level Data", Colors.Blue);

            EnableControlsOnFindSpikes(false);
            PointsToBeRemovedIndices = new List<int>();
        }

        private bool IsMaxContinuityThresholdExceeds(List<WLData> data, int prev, int next)
        {
            float value = (float) Math.Abs(data[next].Value - data[prev].Value);

           if (value > MaxContinuityThreshold)
               return true;

           return false;
        }

        private Spike DetectSpike(List<WLData> Data, int startIndex)
        {
            List<WLData> data = new List<WLData>();
            if (bRefined)
                data = WLDataRefined;
            else
                data = WLData;

            Spike spike = new Spike(startIndex);

            int i = startIndex;
            double diffOld = data[i + 1].Value - data[i].Value;
            int status = -1;                                             // first slope began

            while (true)
            {
                double diffNew = data[i + 1].Value - data[i].Value;

                spike.AddPattern(Math.Abs(diffNew) > MaxContinuityThreshold);
                spike.AddPoint(data[i]);

                if (diffNew * diffOld < 0)
                {
                    status++;
                    if (status == 0)
                        spike.Asset = i;
                    if (status == 1)                                        // beginning of 3rd slope
                        break;
                }

                if(diffNew != 0)
                    diffOld = diffNew;
                i++;
            }

            spike.RemovePattern(spike.Elements.Length - 1);
            spike.RemovePoint(spike.Elements.Length - 1);
            return spike;
        }

        private void OnWriteOutputData(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog svDlg = new Microsoft.Win32.SaveFileDialog();
            svDlg.DefaultExt = "csv";
            svDlg.AddExtension = true;
            svDlg.CheckPathExists = true;
            svDlg.Filter = "Comma Delimited (*.csv)|*.csv|Excel File (*.xlsx)|*.xlsx";
            svDlg.InitialDirectory = Directory.GetCurrentDirectory();
            svDlg.OverwritePrompt = true;
            svDlg.RestoreDirectory = true;
            FileInfo fInfo = new FileInfo(FileName);
            string safeFName = fInfo.Name.Substring(0, fInfo.Name.LastIndexOf('.'));
            svDlg.FileName = safeFName + " - refined";
            svDlg.Title = "Output File Location";
            Nullable<bool> result = svDlg.ShowDialog();
            List<WLData> WillBeExported = null;
            List<WLData> DataToBeExported = WLDataRefined != null ? WLDataRefined : WLData; 

            if (result.Value)
            {
                string filename = svDlg.FileName;

                if (rbAllResult.IsChecked.Value)
                {
                    try
                    {
                        File.WriteAllLines(filename, DataToBeExported.ConvertToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                        return;
                    }
                }
                else
                {
                    TimeSpan interval = GetReduceDataInterval();

                    WillBeExported = ReduceDataByInterval(DataToBeExported, interval);

                    try
                    {
                        File.WriteAllLines(filename, WillBeExported.ConvertToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                        return;
                    }
                }

                MessageBox.Show("Results successfully exported to file");

                if(WillBeExported != null)
                    chartPage.ShowSeriesOnChart(WillBeExported, "Water Level Data", Colors.Yellow);
            }
        }

        public List<WLData> ReduceDataByInterval(List<WLData> data, TimeSpan interval)
        {
            int i = 0;
            DateTime nextDate = data[i].Date;
            WLData[] pickedPoints = new WLData[data.Count];
            data.CopyTo(pickedPoints);
            List<WLData> reducedData = new List<WLData>();
            double average = pickedPoints[0].Value;

            while (pickedPoints.Length > 0)
            {
                pickedPoints = pickedPoints.SkipWhile(s => s.Date <= nextDate).ToArray();

                WLData point = new WLData(nextDate, average);
                reducedData.Add(point);

                nextDate += interval;
                
                List<WLData> pointsbetween = new List<WLData>();
                pointsbetween = pickedPoints.TakeWhile(s => (s.Date <= nextDate)).ToList();
                if (pointsbetween.Count > 0)
                    average = pointsbetween.Average(s => s.Value);
                else
                {
                    nextDate += interval;
                    continue;
                }
            }

            return reducedData;
        }

        public TimeSpan GetReduceDataInterval()
        {
            ExportReduceType reduceType = ExportReduceType.Unknown;

            if (rbHourlyResult.IsChecked == true)
                reduceType = ExportReduceType.Hourly;
            else if (rbHourlyLowPassResult.IsChecked.Value)
                reduceType = ExportReduceType.HourlyLowpass;
            else if (rbDailyResult.IsChecked == true)
                reduceType = ExportReduceType.Daily;
            else if (rbMonthlyResult.IsChecked == true)
                reduceType = ExportReduceType.Monthly;
            else if (rbCustomResult.IsChecked == true)
                reduceType = ExportReduceType.Custom;

            TimeSpan interval = new TimeSpan();
            bool intervalSet = false;
            int coeff = 1;
            while (!intervalSet)
            {
                switch (reduceType)
                {
                    case ExportReduceType.Hourly:
                        interval = new TimeSpan(coeff, 0, 0);
                        intervalSet = true;
                        break;
                    case ExportReduceType.HourlyLowpass:
                        break;
                    case ExportReduceType.Daily:
                        interval = new TimeSpan(coeff, 0, 0, 0);
                        intervalSet = true;
                        break;
                    case ExportReduceType.Monthly:
                        interval = new TimeSpan(coeff * 30, 0, 0, 0, 0);
                        intervalSet = true;
                        break;
                    case ExportReduceType.Custom:
                        for (ExportReduceType type = ExportReduceType.Hourly; type < ExportReduceType.Custom; type++)
                        {
                            if (cbCustomresult.SelectedItem.ToString().Contains(type.ToString()))
                            {
                                reduceType = type;
                                break;
                            }
                        }
                        coeff = int.Parse(tbCustomResult.Text);
                        break;
                }
            }

            return interval;
        }

        public enum ExportReduceType
        {
            Hourly,
            HourlyLowpass,
            Daily,
            Monthly,
            Custom,
            Unknown
        }

        public enum DataFileFormat
        {
            TideGaugeRawFile,
            ExportedSpaceOrTabDelimitedText,
            Unknown
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        #region Event Handlers
        private void OnResetAllClicked(object sender, RoutedEventArgs e)
        {
            mSpikes = new List<Spike>();
            PointsToBeRemovedIndices = null;

            bRefined = false;
            bDataLoaded = false;

            chartPage.ClearChart();
        }

        private void OnchkSensorElevationClicked(object sender, RoutedEventArgs e)
        {
            pShapeFunc.IsEnabled = false;
            pSensorElevation.IsEnabled = true;
            tbSensorElevationAuto.IsEnabled = false;
            tbSensorElevationManual.IsEnabled = false;
        }

        private void OnCurveAverageClicked(object sender, RoutedEventArgs e)
        {
            pSensorElevation.IsEnabled = chkFromSensorElevation.IsChecked == true ? true : false;

            pShapeFunc.IsEnabled = true;
            pSensorElevation.IsEnabled = false;
        }

        private void OnRBSensorElevationAutoClicked(object sender, RoutedEventArgs e)
        {
            tbSensorElevationManual.IsEnabled = false;
            CalculateSensorElevationAuto();
        }

        private void OnRBSensorElevationManualClicked(object sender, RoutedEventArgs e)
        {
            tbSensorElevationManual.IsEnabled = true;
            tbSensorElevationAuto.Text = "";
        }

        private void OnRBSpikeIdentificationClicked(object sender, RoutedEventArgs e)
        {
            pThreshold.IsEnabled = true;
            pPercentile.IsEnabled = false;
        }

        private void OnRBDifferenceCalcClicked(object sender, RoutedEventArgs e)
        {
            pThreshold.IsEnabled = false;
            pPercentile.IsEnabled = false;
        }

        private void OnRBTrendLineClicked(object sender, RoutedEventArgs e)
        {
            pThreshold.IsEnabled = false;
            pPercentile.IsEnabled = true;
        }

        private void OnBtnAboutClicked(object sender, RoutedEventArgs e)
        {
            pageTransitionControl.TransitionType = WpfPageTransitions.PageTransitionType.SlideAndFade;

            if (btnAbout.IsChecked == true)
            {
                AboutPage about = new AboutPage();

                pageTransitionControl.ShowPage(about);
            }
            else
            {
                pageTransitionControl.ShowPage(chartPage);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pageTransitionControl.TransitionType = WpfPageTransitions.PageTransitionType.Fade;
            pageTransitionControl.ShowPage(chartPage);

            string[] custom = new string[] { "Hourly", "Daily", "Monthly" };
            foreach (string s in custom)
                cbCustomresult.Items.Add(s);
            cbCustomresult.SelectedIndex = 0;

            foreach (Expander exp in FindVisualChildren<Expander>(this))
                exp.IsExpanded = Properties.Settings.Default.IsExpanded;

            chkIsExpanded.IsChecked = Properties.Settings.Default.IsExpanded;
            chkKeepOldCharts.IsChecked = bKeepOldCharts;
        }

        private void ShowHideMarkerClicked(object sender, RoutedEventArgs e)
        {
            chartPage.ShowHideMarker();
        }

        private void OnRBCustomResult_Clicked(object sender, RoutedEventArgs e)
        {
            pCustomResult.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnRBMonthlyResult_Clicked(object sender, RoutedEventArgs e)
        {
            pCustomResult.Visibility = System.Windows.Visibility.Hidden;
        }

        private void OnRBDailyResult_Clicked(object sender, RoutedEventArgs e)
        {
            pCustomResult.Visibility = System.Windows.Visibility.Hidden;
        }

        private void OnRBHourlyResult_Clicked(object sender, RoutedEventArgs e)
        {
            pCustomResult.Visibility = System.Windows.Visibility.Hidden;
        }

        private void OnRBAllResult_Clicked(object sender, RoutedEventArgs e)
        {
            pCustomResult.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion
    }
}
