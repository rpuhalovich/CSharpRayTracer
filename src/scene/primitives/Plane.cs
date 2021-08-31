using System;

namespace RayTracer
{
    /// <summary>
    /// Class to represent an (infinite) plane in a scene.
    /// </summary>
    public class Plane : SceneEntity
    {
        private Vector3 center;
        private Vector3 normal;
        private Material material;

        /// <summary>
        /// Construct an infinite plane object.
        /// </summary>
        /// <param name="center">Position of the center of the plane</param>
        /// <param name="normal">Direction that the plane faces</param>
        /// <param name="material">Material assigned to the plane</param>
        public Plane(Vector3 center, Vector3 normal, Material material)
        {
            this.center = center;
            this.normal = normal.Normalized();
            this.material = material;
        }

        /// <summary>
        /// Determine if a ray intersects with the plane, and if so, return hit data.
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray)
        {
            double denom = this.normal.Dot(ray.Direction);

            if (Math.Abs(denom) < Double.Epsilon) return null; // Parallel therefore no hit.

            double t = (this.center - ray.Origin).Dot(this.normal) / denom;
            if (t >= 0.0f) return new RayHit(ray.At(t), this.normal, ray.Direction, this.material);

            return null;
        }

        /// <summary>
        /// The material of the plane.
        /// </summary>
        public Material Material
        {
            get
            {
                return this.material;
            }
        }
    }
}
