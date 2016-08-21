using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WaterLevelData;

namespace LowPassFilters
{
    [DebuggerDisplay("f0={_filw0}, f={_filw.Count}")]
    public class LowPassFilter
    {
        #region 10m coefficients
        public double[] _coeff10 = new double[] 
                {
                    0.1296296,
                    0.1257208,      0.114463,       0.0971907,      
                    0.075901,       0.0529545,      0.0307322,
                    0.0113057,      -0.0038254,     -0.0138889,
                    -0.0188692,     -0.0194147,     -0.0166494,
                    -0.0119285,     -0.0065884,     -0.0017344,
                    0.0018986,      0.0039967,      0.0046296,
                    0.0047458,      0.003031,       0.001766,
                    0.0007136,      0.0000589,      -0.0001939,
                    -0.0001725,     -0.0000576,     0
                };
        #endregion

        #region 5m coefficients
        public double[] _coeff5 = new double[]
                {
                      0.0648148,     0.0643225,      0.0628604,
                      0.0604728,     0.0572315,      0.0532331,
                      0.0485954,     0.0434525,      0.0379505,
                      0.0322412,     0.0264773,      0.0208063,
                      0.0153661,     0.0102800,      0.0056529,
                      0.0015685,    -0.0019127,     -0.0047544,
                     -0.0069445,    -0.0084938,     -0.0094346,
                     -0.0098173,    -0.0097074,     -0.0091818,
                     -0.0083247,    -0.0072233,     -0.0059642,
                     -0.0046296,    -0.0032942,     -0.0020225,
                     -0.0008672,     0.0001321,      0.0009493,
                      0.0015716,     0.0019984,      0.0022398,
                      0.0023148,     0.0022492,      0.0020729,
                      0.0018178,     0.0015155,      0.0011954,
                      0.0008830,     0.0005986,      0.0003568,
                      0.0001662,     0.0000294,     -0.0000560,
                     -0.0000970,    -0.0001032,     -0.0000862,
                     -0.0000578,    -0.0000288,     -0.0000077,
                      0.0000000
                };
        #endregion

        double[] _inuseCoeff;
        List<double> _filw;
        double _filw0;
        FilterType _filterType;
        double _timeStep;

        public FilterType filterType { get { return _filterType; } }
        public TimeSpan TimeStep { get { return TimeSpan.FromMinutes(_timeStep); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Type of filter. It can be 5min or 10min for now.</param>
        /// <param name="stepMinutes">Time step between two consecutive times. Leave blank for auto assignment.</param>
        public LowPassFilter(FilterType type, Nullable<double> stepMinutes = null)
        {
            _filterType = type;
            if(stepMinutes.HasValue)
                _timeStep = stepMinutes.Value;
            switch (type)
            {
                case FilterType.FiveMinutesToHourly:
                    _inuseCoeff = _coeff5;
                    if (stepMinutes == null)
                        _timeStep = 5;
                    break;
                case FilterType.TenMinutesToHourly:
                    _inuseCoeff = _coeff10;
                    if (stepMinutes == null)
                        _timeStep = 10;
                    break;
                default:
                    _inuseCoeff = null;
                    break;
            }

            if (_inuseCoeff == null)
                throw new Exception("Invalid filter type.");
            _filw = _inuseCoeff.ToList().SkipWhile((c, idx) => idx == 0).ToList();
            _filw0 = _inuseCoeff[0];
        }


        #region water level data
        public List<List<WLData>> ConvertDataToHourly(List<WLData> data)
        {
            List<List<WLData>> dataHourly = null;
            List<List<WLData>> separated = FindFilterApplicableSubData(data, _timeStep);
            for (int i = 0; i < separated.Count; i++)
                if (separated[i].Count < _inuseCoeff.Length + 1)
                    separated.RemoveAt(i--);
            dataHourly = CalculateSubDataHourly(separated, _inuseCoeff);

            return dataHourly;
        }

        private List<List<WLData>> CalculateSubDataHourly(List<List<WLData>> data, double[] filter)
        {
            int m = filter.Length;
            List<List<WLData>> dataHourly = new List<List<WLData>>();
            int step = (int)(60.0 / _timeStep);

            for (int n = 0; n < data.Count; n++)
            {
                List<WLData> subDataHourly = new List<WLData>();
                int timesteps = data[n].Count;

                for (int j = m; j < timesteps - m; j += step)
                {
                    WLData dpoint = Calculate10mTo1h(data[n], j, _filw, _filw0);
                    subDataHourly.Add(dpoint);
                }

                dataHourly.Add(subDataHourly);
            }

            dataHourly = dataHourly.Where(s => s.Count > 0).ToList();
            return dataHourly;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timestepMinutes">difference between to consecutive times in minutes</param>
        /// <returns></returns>
        private List<List<WLData>> FindFilterApplicableSubData(List<WLData> data, double timestepMinutes)
        {
            List<List<WLData>> subData = new List<List<WLData>>();

            subData.Add(new List<WLData>());
            subData[subData.Count - 1].Add(data[0]);
            bool sub = true;
            for (int i = 1; i < data.Count; i++)
            {
                TimeSpan ts = data[i].Date - data[i - 1].Date;
                if (ts != TimeSpan.FromMinutes(timestepMinutes))
                    sub = !sub;

                if (!sub)
                {
                    subData.Add(new List<WLData>());
                    sub = !sub;
                }

                subData[subData.Count - 1].Add(data[i]);
            }

            return subData;
        }

        private WLData Calculate10mTo1h(List<WLData> array, int index, List<double> filw, double filw0)
        {
            int m = filw.Count + 1;
            double sum = 0;
            for (int i = 1; i < m; i++)
            {
                try
                {
                    sum += (filw[i - 1] * (array[index + i].Value + array[index - i].Value));
                }
                catch (Exception e)
                {
                    throw new Exception("Filter calculation error (i=" + i + ", index=" + index + "): " + e.Message);
                }
            }

            float p = 0;
            p = (float)(filw0 * array[index].Value + sum);

            WLData d = new WLData(array[index].Date, p);

            return d;
        }
        #endregion

        private TimeSpan AbsoluteDifference(DateTime d1, DateTime d2)
        {
            return (d1 > d2) ? (d1 - d2) : (d2 - d1);
        }

        public enum FilterType
        {
            FiveMinutesToHourly,
            TenMinutesToHourly
        }
    }
}
