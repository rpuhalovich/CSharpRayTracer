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
            // TODO: Plane is upside down for some reason...
            Vector3 rd = ray.Direction, ro = ray.Origin, norm = this.normal, cent = this.center;

            double denom = norm.Dot(rd);
            if (denom > Double.MinValue || denom < Double.MinValue)
            {
                double t = (cent - ro).Dot(norm) / denom;
                if (t >= 0.0f)
                    return new RayHit(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), this.material);
            }
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
