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

        //public Ray[] ShadowSamples(RayHit shadowRh, int rayMultiplier)
        //{
        //    double sideLen = this.radius * 2.0f;
        //    Vector3 centerPos = this.center;
        //    Vector3 centerRayDir = (centerPos - shadowRh.Position).Normalized();
        //}
        public void ShadowRayDir(RayHit srh, ref double angle, ref double radius)
        {
            radius = this.radius;

            Vector3 toLight = (this.center - srh.Position).Normalized();
            Vector3 perpL = toLight.Cross(new Vector3(0.0f, 1.0f, 0.0f));
            if (perpL.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
            {
                perpL = new Vector3(1.0f, 0.0f, 0.0f);
            }

            Vector3 toLightEdge = ((this.center + perpL * this.radius) - srh.Position).Normalized();
            angle = Math.Acos(toLight.Dot(toLightEdge)) * 2.0f;
        }

        //float3 toLight = normalize(light.position - worldPosition);
        //// Calculate a vector perpendicular to L
        //float3 perpL = cross(toLight, float3(0.f, 1.0f, 0.f));
        //// Handle case where L = up -> perpL should then be (1,0,0)
        //if (all(perpL == 0.0f)) {
        //  perpL.x = 1.0;
        //}
        //// Use perpL to get a vector from worldPosition to the edge of the light sphere
        //float3 toLightEdge = normalize((light.position + perpL * light.radius) - worldPosition);
        //// Angle between L and toLightEdge. Used as the cone angle when sampling shadow rays
        //float coneAngle = acos(dot(toLight, toLightEdge)) * 2.0f;

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
