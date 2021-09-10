using System;
using System.Collections.Generic;

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
            double tc = L.Dot(ray.Direction);
            if (tc < 0.0f) return null;

            double d2 = L.Dot(L) - tc * tc;
            if (d2 > this.radius * this.radius) return null;

            double t1c = Math.Sqrt((this.radius * this.radius) - d2);
            Vector3 posT1 = ray.At(tc - t1c).Origin;
            Vector3 posT2 = ray.At(tc + t1c).Origin;

            Vector3 pos;
            if (posT1.LengthWith(ray.Origin) < posT2.LengthWith(ray.Origin) && (ray.Origin - this.center).Length() >= this.radius)
                pos = posT1;
            else
                pos = posT2;

            Vector3 norm = (pos - this.center).Normalized();

            return new RayHit(pos, norm, ray.Direction, this.material);
        }

        public Ray[] ShadowSamples(RayHit shadowRh, int rayMultiplier)
        {
            double sideLen = this.radius * 2.0f;
            Vector3 centerPos = this.center;
            Vector3 centerRayDir = (centerPos - shadowRh.Position).Normalized();

            Mat3 transform = new Mat3(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f));

            if (centerRayDir.X.Equals(0.0f) && centerRayDir.Z.Equals(0.0f))
            {
                if (centerRayDir.Y < 0.0f)
                {
                    Vector3 row0 = new Vector3(-1.0f, 0.0f, 0.0f);
                    Vector3 row1 = new Vector3(0.0f, -1.0f, 0.0f);
                    Vector3 row2 = new Vector3(0.0f, 0.0f, 1.0f);
                    transform = new Mat3(row0, row1, row2);
                }
            }
            else
            {
                Vector3 newY = centerRayDir.Normalized();
                Vector3 newZ = newY.Cross(new Vector3(0.0f, 1.0f, 0.0f)).Normalized();
                Vector3 newX = newY.Cross(newZ).Normalized();
                transform = new Mat3(newX, newY, newZ);
            }

            Vector2[,] dirGrid = new Vector2[rayMultiplier, rayMultiplier];
            double increment = 1.0f / (rayMultiplier + 1);
            for (int i = 0; i < rayMultiplier; i++)
                for (int j = 0; j < rayMultiplier; j++)
                    dirGrid[i, j] = new Vector2(increment * (1 + i), increment * (1 + j));

            List<Ray> rays = new List<Ray>();
            foreach (Vector2 offset in dirGrid)
            {
                double x = offset.X / sideLen * 2;
                double y = offset.Y / sideLen * 2;
                double z = 1.0f;

                double x_adj = (x * 2.0f) - sideLen;
                double y_adj = sideLen - (y * 2.0f);

                Vector3 initVector = new Vector3(x, y, z);

                rays.Add(new Ray(shadowRh.Position, initVector));
            }
            return rays.ToArray();
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
