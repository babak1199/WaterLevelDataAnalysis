using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WaterLevelData;

namespace TrendLine
{
    [DebuggerDisplay("Start:{StartIndex},End:{EndIndex},Point No:{Points.Count}")]
    public class EnvelopeCurve
    {
        List<WLData> _Points;
        int _StartIndex;
        int _EndIndex;

        public List<WLData> Points { get { return _Points; } }
        public int StartIndex { get { return _StartIndex; } }
        public int EndIndex { get { return _EndIndex; } }

        public EnvelopeCurve(List<WLData> points, int index1, int index2)
        {
            _Points = points;
            _StartIndex = index1;
            _EndIndex = index2;
        }

        public void ReplacePoints(List<WLData> points)
        {
            _Points = points;
        }
    }
}
