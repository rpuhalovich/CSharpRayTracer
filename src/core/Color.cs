using System;

namespace RayTracer
{
    /// <summary>
    /// Immutable structure to represent a color as r/g/b with 0-1 ranges
    /// </summary>
    public readonly struct Color
    {
        private readonly double r, g, b;

        /// <summary>
        /// Construct a new color structure given red, green, blue
        /// components (0-1 ranges).
        /// </summary>
        /// <param name="r">Red component (0-1)</param>
        /// <param name="g">Blue component (0-1)</param>
        /// <param name="b">Green component (0-1)</param>
        public Color(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        /// <summary>
        /// Convert color structure to string.
        /// </summary>
        /// <returns>Color as string in form (r, g, b)</returns>
        public override string ToString()
        {
            return "(" + this.r + "," + this.g + "," + this.b + ")";
        }

        /// <summary>
        /// The red component of the color (0-1).
        /// </summary>
        public double R
        {
            get
            {
                return this.r;
            }
        }

        /// <summary>
        /// The green component of the color (0-1).
        /// </summary>
        public double G
        {
            get
            {
                return this.g;
            }
        }

        /// <summary>
        /// The blue component of the color (0-1).
        /// </summary>
        public double B
        {
            get
            {
                return this.b;
            }
        }

        /// <summary>
        /// Multiply each color component by a single scalar value.
        /// </summary>
        /// <param name="a">Color structure</param>
        /// <param name="b">Scalar multiple</param>
        /// <returns>Multiplied color structure</returns>
        public static Color operator *(Color a, double b)
        {
            return new Color(a.r * b, a.g * b, a.b * b);
        }

        /// <summary>
        /// Multiply each color component by a single scalar value.
        /// </summary>
        /// <param name="a">Color structure</param>
        /// <param name="b">Scalar multiple</param>
        /// <returns>Multiplied color structure</returns>
        public static Color operator *(double a, Color b)
        {
            return new Color(a * b.r, a * b.g, a * b.b);
        }

        /// <summary>
        /// Multiply two colors together component-wise.
        /// </summary>
        /// <param name="a">First color structure</param>
        /// <param name="b">Second color structure</param>
        /// <returns>Multiplied color structure</returns>
        public static Color operator *(Color a, Color b)
        {
            return new Color(a.r * b.r, a.g * b.g, a.b * b.b);
        }

        /// <summary>
        /// Divide each color component by a single scalar value.
        /// </summary>
        /// <param name="a">Color structure</param>
        /// <param name="b">Scalar divisor</param>
        /// <returns>Divided color structure</returns>
        public static Color operator /(Color a, double b)
        {
            return new Color(a.r / b, a.g / b, a.b / b);
        }

        /// <summary>
        /// Divide each color component by a single scalar value.
        /// </summary>
        /// <param name="a">Scalar divisor</param>
        /// <param name="b">Color structure</param>
        /// <returns>Divided color structure</returns>
        public static Color operator /(double a, Color b)
        {
            return new Color(a/ b.r, a / b.g, a / b.b);
        }

        /// <summary>
        /// Divide two colors component-wise.
        /// </summary>
        /// <param name="a">Color structure</param>
        /// <param name="b">Divisor color structure</param>
        /// <returns>Divided color structure</returns>
        public static Color operator /(Color a, Color b)
        {
            return new Color(a.r / b.r, a.g / b.g, a.b / b.b);
        }

        /// <summary>
        /// Add two colors together component-wise.
        /// </summary>
        /// <param name="a">First color structure</param>
        /// <param name="b">Second color structure</param>
        /// <returns>Multiplied color structure</returns>
        public static Color operator +(Color a, Color b)
        {
            return new Color(a.r + b.r, a.g + b.g, a.b + b.b);
        }

        /// <summary>
        /// Returns the input color except all values below 0 and above 1 have been
        /// clamped down to 0 and 1 respectively.
        /// </summary>
        public Color Clamp()
        {
            double min = 0.0f, max = 1.0f;
            return new Color(Math.Clamp(this.r, min, max), Math.Clamp(this.g, min, max), Math.Clamp(this.b, min, max));
        }

        /// <summary>
        /// Because typing out new Color(0.0f, 0.0f, 0.0f) is a chore.
        /// </summary>
        public static Color Black()
        {
            return new Color(0.0f, 0.0f, 0.0f);
        }
    }
}
