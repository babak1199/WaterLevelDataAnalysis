using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WaterLevelData;
using LowPassFilters;

namespace WaterLevelDataLoader
{
    public class WLDataLoader
    {
        LowPassFilter _hourlyFilter;
        List<List<WLData>> _hourlyData;
        List<WLData> _wlData;

        public bool IsHourlyFilterInvolved { get { return (_hourlyFilter != null ? true : false); } }
        public bool IsConvertedToHourly { get { return (_hourlyData != null ? true : false); } }
        public List<List<WLData>> DataHourly { get { return _hourlyData; } }
        public List<WLData> Data { get { return _wlData; } }

        public WLDataLoader(string fileName, LowPassFilter hourlyFilter = null)
        {
            _hourlyFilter = hourlyFilter;

            try
            {
                _wlData = LoadWLDataFromFile(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                return;
            }

            if(IsHourlyFilterInvolved)
                ConvertDataToHourly();
        }

        private List<WLData> LoadWLDataFromFile(string fileName)
        {
            string[] strings = null;
            try
            {
                strings = File.ReadAllLines(fileName);
            }
            catch (Exception e)
            {
                throw new Exception("Error reading file " + fileName + ": " + e.Message);
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

        public void AddHourlyFilter(LowPassFilter hourlyFilter)
        {
            if (IsHourlyFilterInvolved)
                throw new Exception("Already contains an hourly filter. " + 
                        "Please use ReplaceHourlyFilter method to replace it.");
            _hourlyFilter = hourlyFilter;
        }

        public void ReplaceHourlyFilter(LowPassFilter newHourlyFilter)
        {
            if (!IsHourlyFilterInvolved)
                throw new Exception("No hourly filter found. Please use AddHourlyFilter to " +
                                    "add a filter.");
            _hourlyFilter = newHourlyFilter;
        }

        public void ConvertDataToHourly()
        {
            if (!IsHourlyFilterInvolved)
                throw new Exception("No hourly filter provided.");
            if (IsConvertedToHourly)
                throw new Exception("Already converted to hourly");

            try
            {
                _hourlyData = _hourlyFilter.ConvertDataToHourly(Data);
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred during conversion: " + e.Message);
            }
        }

        public void ExportHourlyDataToFile(string filename)
        {
            string[] lines = CreateCommaDelimitedStringArray(DataHourly);
            try
            {
                File.WriteAllLines(filename, lines);
            }
            catch (Exception e)
            {
                throw new Exception("Error exporting to " + filename + ": " + e.Message);
            }
        }

        private string[] CreateCommaDelimitedStringArray(List<List<WLData>> array)
        {
            List<string> lines = new List<string>();

            foreach (List<WLData> list in array)
                foreach (WLData item in list)
                    lines.Add(item.ToString());

            return lines.ToArray();
        }

        public enum DataFileFormat
        {
            TideGaugeRawFile,
            ExportedSpaceOrTabDelimitedText,
            Unknown
        }
    }
}
