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
            Vector3 oc = ray.Origin - this.center;
            double a = ray.Direction.Dot(ray.Direction);
            double b = 2.0f * ray.Direction.Dot(oc);
            double c = oc.Dot(oc) - (this.radius * this.radius);
            double discriminant = (b * b) - (4.0f * a * c);

            if (discriminant >= 0.0f) // Hit
            {
                double t = (-b - Math.Sqrt(discriminant)) / (2.0f * a);
                return new RayHit(ray.At(t), (ray.At(t) - new Vector3(0.0f, 0.0f, -1.0f)).Normalized(), new Vector3(0.0f, 0.0f, 0.0f), this.material);
            }

            return null;
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
