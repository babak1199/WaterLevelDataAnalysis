using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaterLevelData;

namespace TrendLine
{
    public class Envelope
    {
        EnvelopeCurve _UpperCurve;
        EnvelopeCurve _LowerCurve;
        List<WLData> _Data;

        public EnvelopeCurve UpperCurve { get { return _UpperCurve; } }
        public EnvelopeCurve LowerCurve { get { return _LowerCurve; } }
        public List<WLData> Data { get { return _Data; } }

        public Envelope(List<WLData> data, EnvelopeCurve lower, EnvelopeCurve upper)
        {
            _LowerCurve = lower;
            _UpperCurve = upper;
            _Data = data;
        }

        public void InterpolateCurves()
        {
            InterpolateEnvelopEmptyPoint(_UpperCurve);
            InterpolateEnvelopEmptyPoint(_LowerCurve);
        }

        private void InterpolateEnvelopEmptyPoint(EnvelopeCurve curve)
        {
            List<WLData> source = curve.Points;

            int idEnd = -1;                                                         // data end position
            int ieStart = 0;                                                        // envelop start position
            int ieEnd = 0;                                                          // envelop end position
            int idStart = FindEqualTime(source, ieStart, idEnd + 1);                // data start position

            if (idStart == -1)
                return;

            while (true)
            {
                if (ieStart + 1 > source.Count - 1)
                    break;

                ieEnd = ieStart + 1;

                idEnd = FindEqualTime(source, ieEnd, idStart + 1);

                if (idEnd == -1)
                    break;

                List<WLData> shouldBeAdded = new List<WLData>();
                for (int i = idStart + 1; i < idEnd; i++)
                {
                    TimeSpan ts2 = Data[idEnd].Date - Data[idStart].Date;
                    TimeSpan ts1 = Data[i].Date - Data[idStart].Date;
                    double dy = Data[idEnd].Value - Data[idStart].Value;

                    double interpolated = ts1.TotalMinutes / ts2.TotalMinutes * dy + Data[idStart].Value;

                    shouldBeAdded.Add(new WLData(Data[i].Date, interpolated));
                }

                source.InsertRange(ieStart + 1, shouldBeAdded);
                ieStart = ieEnd + idEnd - (idStart + 1);
                idStart = idEnd;
            }

            curve.ReplacePoints(source);
        }

        public double[] CalculateCurvesDifferences()
        {
            EnvelopeCurve ReferenceEnv = null;
            EnvelopeCurve MovedEnv = null;

            double[] ranges = new double[Data.Count];
            for (int i = 0; i < Data.Count; i++)
                ranges[i] = -1;

            if (UpperCurve.StartIndex > LowerCurve.StartIndex)
            {
                ReferenceEnv = UpperCurve;
                MovedEnv = LowerCurve;
            }
            else
            {
                ReferenceEnv = LowerCurve;
                MovedEnv = UpperCurve;
            }

            int balance = Math.Abs(UpperCurve.StartIndex - LowerCurve.StartIndex);
            int r = 0;
            while (MovedEnv.Points[r + balance].Date == ReferenceEnv.Points[r].Date &&
                    r + balance < MovedEnv.Points.Count - 1 && r < ReferenceEnv.Points.Count - 1)
            {
                ranges[r + ReferenceEnv.StartIndex] = Math.Abs(ReferenceEnv.Points[r].Value -
                                                                MovedEnv.Points[r + balance].Value);
                r++;
            }

            return ranges;
        }

        private int FindEqualTime(List<WLData> source, int pos, int start)
        {
            int loc = start;

            while (!source[pos].Date.Equals(Data[loc].Date))
            {
                if (loc < Data.Count)
                    loc++;
                else
                {
                    loc = -1;
                    break;
                }
            }

            return loc;
        }
    }
}
