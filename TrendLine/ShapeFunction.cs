using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WaterLevelData;

namespace TrendLine
{
    [DebuggerDisplay("Center:{RelativeCenter}, Point No:{PointNumber}")]
    public class ShapeFunction
    {
        int _RelativeCenter;
        int _PointNumber;

        public int RelativeCenter { get { return _RelativeCenter; } }
        public int PointNumber { get { return _PointNumber; } }

        public ShapeFunction(int pointNo)
        {
            _PointNumber = pointNo;
            _RelativeCenter = (int)Math.Round((decimal)pointNo / 2, MidpointRounding.AwayFromZero) - 1;
        }
    }
}
