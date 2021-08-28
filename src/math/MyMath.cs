using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class MyMath
    {
        /// <summary>
        /// https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        /// </summary>
        public static double NextDoubleMinMax(double min, double max)
        {
            Random rand = new Random();
            return rand.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// So I don't have to make a rand instance...
        /// </summary>
        public static double MyNextDouble()
        {
            Random rand = new Random();
            return rand.NextDouble();
        }

        /// <summary>
        /// See: https://stackoverflow.com/questions/268853/is-it-possible-to-write-quakes-fast-invsqrt-function-in-c
        /// </summary>
        public static double FastInvSqrt(double y)
        {
            float x = (float)y;
            float xhalf = 0.5f * x;
            int i = BitConverter.SingleToInt32Bits(x);
            i = 0x5f3759df - (i >> 1);
            x = BitConverter.Int32BitsToSingle(i);
            x = x * (1.5f - xhalf * x * x); // 1st iteration
            // x = x * (1.5f - xhalf * x * x); // 2nd iteration (can be removed)
            return (double)x;
        }
    }
}
