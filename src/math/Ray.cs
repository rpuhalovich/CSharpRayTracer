using System;

namespace RayTracer
{
    /// <summary>
    /// Immutable structure to represent a ray (origin, direction).
    /// </summary>
    public readonly struct Ray
    {
        private readonly Vector3 origin;
        private readonly Vector3 direction;

        /// <summary>
        /// Construct a new ray.
        /// </summary>
        /// <param name="origin">The starting position of the ray</param>
        /// <param name="direction">The direction of the ray</param>
        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        /// <summary>
        /// The starting position of the ray.
        /// </summary>
        public Vector3 Origin
        {
            get
            {
                return this.origin;
            }
        }

        /// <summary>
        /// The direction of the ray.
        /// </summary>
        public Vector3 Direction
        {
            get
            {
                return this.direction;
            }
        }

        /// <summary>
        /// Position of a ray at length t from origin.
        /// </summary>
        public Vector3 At(double t)
        {
            return this.origin + (t * this.direction);
        }

        /// <summary>
        /// The Color of the ray.
        /// </summary>
        public Color rayColor(Ray r)
        {
            Vector3 ud = r.direction.Normalized();
            double t = 0.5 * (ud.Y + 1.0);
            return new Color(1.0f, 1.0f, 1.0f) * (1.0f - t) + new Color(0.5f, 0.7f, 1.0f) * t;
            //return new Color(0.0f, 0.0f, 0.0f);
        }
    }
}
