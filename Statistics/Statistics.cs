using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stat
{
    public static class Statistics
    {
        public static double Average(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            return (sum / array.Length);
        }

        public static double StandardDeviation(double[] array, bool Population = true)
        {
            double avg = Average(array);

            double sqSum = 0;
            for (int i = 0; i < array.Length; i++)
                sqSum += Math.Pow(Math.Abs(array[i] - avg), 2);

            if (Population)
                return (sqSum / array.Length);

            return (sqSum / (array.Length - 1));
        }
    }
}
