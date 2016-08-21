using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaterLevelData;

namespace WLDataAnalysis
{
    public static class Extensions
    {
        public static string ConvertToString(this List<bool> list)
        {
            string result = "";

            foreach (bool item in list)
                result += (item) ? "T" : "F";

            return result;
        }

        public static string ConvertToString(this bool[] array)
        {
            return array.ToList().ConvertToString();
        }

        public static string ConvertToString(this int[] array)
        {
            string result = "";

            foreach (int item in array)
                result += item + " ";
            result = result.TrimEnd(new char[] { ' ' });

            return result;
        }

        public static string[] ConvertToString(this List<WLData> array)
        {
            List<string> result = new List<string>();

            foreach (WLData ts in array)
                result.Add(ts.ToString());

            return result.ToArray();
        }
    }
}
