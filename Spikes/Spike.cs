using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TrendLine;
using WaterLevelData;

namespace Spikes
{
    [DebuggerDisplay("Start at: {StartIndex}, Points No: {PointNo}, Pattern:\"{Pattern.ConvertToString()}\"")]
    public class Spike
    {
        List<WLData> _Elements;
        int StartIndex;
        List<bool> _Pattern;
        List<int> _PointsToBeRemoved;
        public int Asset { get; set; }

        public int PointNo { get { return _Elements.Count; } }
        public WLData[] Elements { get { return _Elements.ToArray(); } }
        public bool[] Pattern { get { return _Pattern.ToArray(); } }
        public int[] PointsToBeRemoved { get { return _PointsToBeRemoved.ToArray(); } }

        public Spike()
        {
            StartIndex = -1;
            _Elements = new List<WLData>();
            _Pattern = new List<bool>();
            _PointsToBeRemoved = new List<int>();
        }

        public Spike(int startIndex)
        {
            StartIndex = startIndex;
            _Elements = new List<WLData>();
            _Pattern = new List<bool>();
            _PointsToBeRemoved = new List<int>();
        }


        public void DeterminePointsToBeDeleted(RemoveMethod method = RemoveMethod.ByDifference,
                                                int domainMethodPointNo = -1)
        {
            if (method == RemoveMethod.ByPattern)
            {
                PatternType patType = DetectPattern();
                if (patType == PatternType.OTHER)
                {
                }
            }

            else if (method == RemoveMethod.ByDifference)
            {
                for (int i = 0; i < Pattern.Length; i++)
                {
                    int notLast = (i != Pattern.Length - 1) ? 1 : 0;

                    if (Pattern[i])
                        _PointsToBeRemoved.Add(StartIndex + i + notLast);
                }
            }

            else if (method == RemoveMethod.ByDomain)
            {
                TrendLineSpikeRemover zoneDetect = new TrendLineSpikeRemover(Elements.ToList(), domainMethodPointNo);


            }
        }

        private PatternType DetectPattern(int start = -1, int end = -1)
        {
            int tNdx1 = 0;
            while (Pattern[tNdx1])
            {
                if (tNdx1 < Pattern.Length - 1)
                    tNdx1++;
                else
                {
                    // TTTTTT....
                    for (int i = 1; i < PointNo; i++)
                        _PointsToBeRemoved.Add(StartIndex + i);
                    return PatternType.T;
                }
            }
            int fNdx = tNdx1;
            while (!Pattern[fNdx])
            {
                if (fNdx < Pattern.Length - 1)
                    fNdx++;
                else
                {
                    // TTTT...FFFF...
                    for (int i = 1; i < tNdx1; i++)
                        _PointsToBeRemoved.Add(StartIndex + i);
                    return PatternType.TF;
                }
            }
            int tNdx2 = fNdx;
            while (Pattern[tNdx2])
            {
                if (tNdx2 < Pattern.Length - 1)
                    tNdx2++;
                else
                {
                    // TTTTT.... FFFF.....TTTTT.....
                    for (int i = 1; i < PointNo; i++)
                        _PointsToBeRemoved.Add(StartIndex + i);
                    return PatternType.TFT;
                }
            }

            return PatternType.OTHER;
        }

        #region add/remove elements
        public void AddPoint(DateTime date, double value)
        {
            _Elements.Add(new WLData(date, value));
        }

        public void AddPoint(WLData data)
        {
            _Elements.Add(data);
        }

        public void RemovePoint(int index)
        {
            _Elements.RemoveAt(index);
        }

        public void AddPattern(bool pattern)
        {
            _Pattern.Add(pattern);
        }

        public void RemovePattern(int index)
        {
            _Pattern.RemoveAt(index);
        }
        #endregion

        public enum PatternType
        {
            T,
            TF,
            TFT,
            OTHER
        }

        public enum RemoveMethod
        {
            ByPattern,
            ByDifference,
            ByDomain
        }
    }
}
