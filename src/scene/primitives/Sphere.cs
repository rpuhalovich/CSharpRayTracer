using System;

namespace RayTracer
{
    /// <summary>
    /// Class to represent an triangle in a scene.
    /// </summary>
    public class Sphere : SceneEntity
    {
        private Vector3 center;
        private double radius;
        private Material material;

        /// <summary>
        /// Construct a sphere given its center point and a radius.
        /// </summary>
        /// <param name="center">Center of the sphere</param>
        /// <param name="radius">Radius of the spher</param>
        /// <param name="material">Material assigned to the sphere</param>
        public Sphere(Vector3 center, double radius, Material material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;
        }

        /// <summary>
        /// Determine if a ray intersects with the sphere, and if so, return hit data.
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray)
        {
            double offset = 0.0001f;

            Vector3 L = this.center - ray.Origin;
            double tca = L.Dot(ray.Direction);
            double d2 = L.Dot(L) - tca * tca; //float d2 = L*L - tca*tca;
            if (d2 > this.radius * this.radius) return null;
            double thc = Math.Sqrt(this.radius * this.radius - d2);
            double t0 = tca - thc; // t0 is the t value for distance of ray.
            double t1 = tca + thc;
            if (t0 < offset) t0 = t1;
            if (t0 < offset) return null;

            Vector3 pos = ray.At(t0);
            pos = new Vector3(pos.X, pos.Y, pos.Z * -1);

            return new RayHit(pos, pos - this.center.Normalized(), ray.Direction, this.material);
        }

        /// <summary>
        /// The material of the sphere.
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
