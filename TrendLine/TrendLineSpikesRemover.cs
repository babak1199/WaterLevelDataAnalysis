using Stat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WaterLevelData;

namespace TrendLine
{
    [DebuggerDisplay("IsDetectZone:{DetectZones}")]
    public class TrendLineSpikeRemover
    {
        bool _DetectZones;
        ShapeFunction _shapeFunc;
        List<WLData> _Data;
        Envelope _mEnvelope;
        List<int> _PointsToBeRemovedIndices;

        public bool DetectZones { get { return _DetectZones; } }
        public ShapeFunction shapeFunc { get { return _shapeFunc; } }
        public List<WLData> Data { get { return _Data; } }
        public Envelope mEnvelope { get { return _mEnvelope; } }
        public List<int> PointsToBeRemovedIndices { get { return _PointsToBeRemovedIndices; } }

        public TrendLineSpikeRemover(List<WLData> data, int shapefuncPointNo, bool detect = false)
        {
            _Data = data;
            _shapeFunc = new ShapeFunction(shapefuncPointNo);
            _DetectZones = detect;
            _mEnvelope = null;
        }

        public List<WLData> CalculateAverageTrend()
        {
            List<WLData> averages = new List<WLData>();

            for (int i = shapeFunc.RelativeCenter; i < Data.Count - (shapeFunc.PointNumber -
                                                                        shapeFunc.RelativeCenter) + 1; i++)
            {
                WLData ts = CalculateAverage(i);
                averages.Add(ts);
            }

            return averages;
        }

        public List<WLData> DetectSpikes(string percent)
        {
            _PointsToBeRemovedIndices = new List<int>();

            CalculateEnvelop();

            _mEnvelope.InterpolateCurves();
            double[] ranges = _mEnvelope.CalculateCurvesDifferences();

            List<double> rangePurified = ranges.Where(p => p >= 0).ToList();
            rangePurified.Sort();
            //double percentile = double.Parse(percent) / 100.0;
            double Sigma = Statistics.StandardDeviation(rangePurified.ToArray());
            double percentile = 1 - 2 * Sigma;
            int num = (int)(percentile * rangePurified.Count);

            double Value = rangePurified[num];

            List<WLData> pointsToBeRemoved = new List<WLData>();
            for (int i = 0; i < Data.Count; i++)
            {
                if (ranges[i] > Value)
                {
                    PointsToBeRemovedIndices.Add(i);
                    pointsToBeRemoved.Add(Data[i]);
                }
            }

            return pointsToBeRemoved;
        }

        public void CalculateEnvelop()
        {
            List<WLData> AverageTrend = CalculateAverageTrend();
            List<WLData> upperEnvPoints = new List<WLData>();
            List<WLData> lowerEnvPoints = new List<WLData>();

            for (int i = 0; i < AverageTrend.Count; i++)
            {
                if (Data[i + 1].Value >= AverageTrend[i].Value)
                    upperEnvPoints.Add(Data[i + 1]);
                else
                    lowerEnvPoints.Add(Data[i + 1]);
            }

            int upSIdx = Data.FindIndex(s => (s.Date.Equals(upperEnvPoints[0].Date)));
            int upEIdx = Data.FindIndex(s => (s.Date.Equals(upperEnvPoints[upperEnvPoints.Count - 1].Date)));
            int lowSIdx = Data.FindIndex(s => (s.Date.Equals(lowerEnvPoints[0].Date)));
            int lowEIdx = Data.FindIndex(s => (s.Date.Equals(lowerEnvPoints[lowerEnvPoints.Count - 1].Date)));

            EnvelopeCurve upperCurve = new EnvelopeCurve(upperEnvPoints, upSIdx, upEIdx);
            EnvelopeCurve lowerCurve = new EnvelopeCurve(lowerEnvPoints, lowSIdx, lowEIdx);

            _mEnvelope = new Envelope(Data, lowerCurve, upperCurve);
        }

        private WLData CalculateAverage(int pos)
        {
            WLData ts = new WLData(Data[pos].Date, Data[pos].Value);

            double sum = 0;
            for (int i = pos - shapeFunc.RelativeCenter;
                                        i < pos + shapeFunc.PointNumber - shapeFunc.RelativeCenter; i++)
                sum += Data[i].Value;
            ts.Value = sum / shapeFunc.PointNumber;

            return ts;
        }
    }
}
