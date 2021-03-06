using System;

namespace RayTracer
{
    /// <summary>
    /// Immutable structure to represent a three-dimensional vector.
    /// </summary>
    public readonly struct Vector3
    {
        private const double EPSILON = 0.0001f;
        private const double OFFSET = 0.00000000005f;

        private readonly double x, y, z;

        /// <summary>
        /// Construct a three-dimensional vector.
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Convert vector to a readable string.
        /// </summary>
        /// <returns>Vector as string in form (x, y, z)</returns>
        public override string ToString()
        {
            // return string.Format("({0:N3}, {0:N3}, {0:N3})", this.x, this.y, this.z);
            return "(" + this.x + "," + this.y + "," + this.z + ")";
        }

        /// <summary>
        /// Compute the length of the vector squared.
        /// This should be used if there is a way to perform a vector
        /// computation without needing the actual length, since
        /// a square root operation is expensive.
        /// </summary>
        /// <returns>Length of the vector squared</returns>
        public double LengthSq()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        /// <summary>
        /// Compute the length of the vector.
        /// Where the length is the length from the origin.
        /// </summary>
        /// <returns>Length of the vector</returns>
        public double Length()
        {
            return Math.Sqrt(LengthSq());
        }

        /// <summary>
        /// Computes length of vector from given argument vector.
        /// </summary>
        /// <returns>Length of the vector</returns>
        public double LengthWith(Vector3 with)
        {
            double x = (this - with).x, y = (this - with).y, z = (this - with).z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Compute a length 1 vector in the same direction.
        /// Assumes that the origin is where the ray came from.
        /// </summary>
        /// <returns>Normalized vector</returns>
        public Vector3 Normalized()
        {
            // from glm::normalized
            // return v * inversesqrt(dot(v, v));
            return this * (1.0f / Math.Sqrt(this.Dot(this)));
            // return this * MyMath.FastInvSqrt(this.Dot(this)); // For experimenting, faster too.
        }

        /// <summary>
        /// Compute the dot product with another vector.
        /// </summary>
        /// <param name="with">Vector to dot product with</param>
        /// <returns>Dot product result</returns>
        public double Dot(Vector3 with)
        {
            return (this.x * with.x) + (this.y * with.y) + (this.z * with.z);
        }

        /// <summary>
        /// Compute the cross product with another vector.
        /// </summary>
        /// <param name="with">Vector to cross product with</param>
        /// <returns>Cross product result</returns>
        public Vector3 Cross(Vector3 with)
        {
            return new Vector3(
                (this.y * with.z) - (this.z * with.y),
                (this.z * with.x) - (this.x * with.z),
                (this.x * with.y) - (this.y * with.x)
            );
        }

        /// <summary>
        /// Sum two vectors together (using + operator).
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Summed vector</returns>
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        /// <summary>
        /// Negate a vector (using - operator)
        /// </summary>
        /// <param name="a">Vector to negate</param>
        /// <returns>Negated vector</returns>
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(a.x * -1.0f, a.y * -1.0f, a.z * -1.0f);
        }

        /// <summary>
        /// Subtract one vector from another.
        /// </summary>
        /// <param name="a">Original vector</param>
        /// <param name="b">Vector to subtract</param>
        /// <returns>Subtracted vector</returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        /// <summary>
        /// Multiply a vector by a scalar value.
        /// </summary>
        /// <param name="a">Original vector</param>
        /// <param name="b">Scalar multiplier</param>
        /// <returns>Multiplied vector</returns>
        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }

        /// <summary>
        /// Multiply a vector by a scalar value (opposite operands).
        /// </summary>
        /// <param name="b">Scalar multiplier</param>
        /// <param name="a">Original vector</param>
        /// <returns>Multiplied vector</returns>
        public static Vector3 operator *(double b, Vector3 a)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }

        /// <summary>
        /// Divide a vector by a scalar value.
        /// </summary>
        /// <param name="a">Original vector</param>
        /// <param name="b">Scalar divisor</param>
        /// <returns>Divided vector</returns>
        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }

        /// <summary>
        /// From: https://raytracing.github.io/books/RayTracingInOneWeekend.html
        /// </summary>
        public static Vector3 Random()
        {
            Random rand = new Random();
            return new Vector3(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
        }

        /// <summary>
        /// From: https://raytracing.github.io/books/RayTracingInOneWeekend.html
        /// </summary>
        public static Vector3 Random(double min, double max)
        {
            return new Vector3(MyMath.NextDoubleMinMax(min, max), MyMath.NextDoubleMinMax(min, max), MyMath.NextDoubleMinMax(min, max));
        }

        /// <summary>
        /// From: https://raytracing.github.io/books/RayTracingInOneWeekend.html
        /// </summary>
        public static Vector3 RandomNormSphere()
        {
            while (true)
            {
                Vector3 p = Random(-1.0f, 1.0f);
                if (p.LengthSq() >= 1.0f) continue;
                return p.Normalized();
            }
        }


        /// <summary>
        /// From: https://raytracing.github.io/books/RayTracingInOneWeekend.html
        /// </summary>
        public static Vector3 RandomHemisphere(Vector3 norm, double dotAngleCutoff=0.0f)
        {
            while (true)
            {
                Vector3 p = Random(-1.0f, 1.0f);
                if (p.LengthSq() >= 1.0f) continue;
                if (norm.Dot(p) < dotAngleCutoff) continue;
                return p.Normalized();
            }
        }

        public static Vector3 RandomInUnitDisk(double focalDistance=0.0f)
        {
            while (true)
            {
                double min = -1.0f, max = 1.0f;
                Vector3 p = new Vector3(MyMath.NextDoubleMinMax(min, max), MyMath.NextDoubleMinMax(min, max), 0.0f);
                if (p.LengthSq() >= 1.0f) continue;
                return p;
            }
        }

        public static Vector3 Offset(Vector3 pos, Vector3 dir)
        {
            return pos + (OFFSET * dir);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 vector &&
                   Math.Abs(x - vector.x) < EPSILON &&
                   Math.Abs(y - vector.y) < EPSILON &&
                   Math.Abs(z - vector.z) < EPSILON &&
                   Math.Abs(X - vector.X) < EPSILON &&
                   Math.Abs(Y - vector.Y) < EPSILON &&
                   Math.Abs(Z - vector.Z) < EPSILON;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, X, Y, Z);
        }

        public static Vector3 MaxValue()
        {
            return new Vector3(Double.MaxValue, Double.MaxValue, Double.MaxValue);
        }

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }
    }
}
