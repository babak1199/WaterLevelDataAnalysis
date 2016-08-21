using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WaterLevelData
{
    [DebuggerDisplay("Date:{Date}, Value:{Value}")]
    public class WLData
    {
        public WLData()
        {
            Date = new DateTime();
            Value = -1;
        }

        public WLData(DateTime date, double value)
        {
            Date = date;
            Value = value;
        }

        public override string ToString()
        {
            return (Date.ToShortDateString() + " " + Date.ToShortTimeString() + "," +
                        Value.ToString("0.000"));
        }

        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}
