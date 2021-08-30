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
        /// From: http://kylehalladay.com/blog/tutorial/math/2013/12/24/Ray-Sphere-Intersection.html
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray)
        {
            Vector3 L = this.center - ray.Origin;
            double tc = L.Dot(ray.Direction.Normalized());
            if (tc < 0.0f) return null;

            double d2 = L.Dot(L) - tc * tc;
            if (d2 > this.radius * this.radius) return null;

            double t1c = Math.Sqrt((this.radius * this.radius) - d2);
            double t1 = tc - t1c;
            double t2 = tc + t1c;

            Vector3 pos = ray.At(t1);
            if (ray.At(t2).LengthWith(ray.Origin) < ray.At(t1).LengthWith(ray.Origin)) pos = (ray.At(t2));
            return new RayHit(pos, (pos - this.center).Normalized(), ray.Direction, this.material);
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
