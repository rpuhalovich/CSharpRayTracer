using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public readonly struct Vector2
    {
        private readonly double x, y;

        /// <summary>
        /// Construct a three-dimensional vector.
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X => x;

        public double Y => y;
    }
}
