using System;
using System.Collections.Generic;

namespace RayTracer
{
    /// <summary>
    /// Class to represent a triangle in a scene represented by three vertices.
    /// </summary>
    public class Triangle : SceneEntity
    {
        private Vector3 v0, v1, v2;
        private Material material;

        /// <summary>
        /// Construct a triangle object given three vertices.
        /// </summary>
        /// <param name="v0">First vertex position</param>
        /// <param name="v1">Second vertex position</param>
        /// <param name="v2">Third vertex position</param>
        /// <param name="material">Material assigned to the triangle</param>
        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, Material material)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.material = material;
        }

        /// <summary>
        /// Determine if a ray intersects with the triangle,nd if so a, return hit data.
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray)
        {
            // Remember to convert to LHR.
            Vector3 norm = (v1 - v0).Cross(v2 - v0).Normalized();

            double nDotRayDir = norm.Dot(ray.Direction);
            if (Math.Abs(nDotRayDir) < Double.Epsilon) return null; // Parallel, therefore no hit.

            double t = norm.Dot(v0 - ray.Origin) / nDotRayDir;
            if (t < 0.0f) return null;

            // Compute intersection point.
            Vector3 P = ray.At(t).Origin;

            // Inside outside test.
            Vector3 C;

            // e0
            Vector3 e0 = v1 - v0;
            Vector3 vp0 = P - v0;
            C = e0.Cross(vp0);
            if (norm.Dot(C) < 0.0f) return null;

            // e1
            Vector3 e1 = v2 - v1;
            Vector3 vp1 = P - v1;
            C = e1.Cross(vp1);
            if (norm.Dot(C) < 0.0f) return null;

            // e2
            Vector3 e2 = v0 - v2;
            Vector3 vp2 = P - v2;
            C = e2.Cross(vp2);
            if (norm.Dot(C) < 0.0f) return null;

            return new RayHit(P, norm, ray.Direction, this.material);
        }

        public double ShadowRayAngle(RayHit srh)
        {
            Vector3 center = GetCenter();
            Vector3 toLight = (center - srh.Position).Normalized();
            Vector3 perpL = toLight.Cross(new Vector3(0.0f, 1.0f, 0.0f));
            if (perpL.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
            {
                perpL = new Vector3(1.0f, 0.0f, 0.0f);
            }

            Vector3 toLightEdge = ((center + perpL * GetRadius()) - srh.Position).Normalized();
            return toLight.Dot(toLightEdge);
        }

        /// <summary>
        /// Returns center of the longest edge.
        /// </summary>
        public Vector3 GetCenter()
        {
            double x = this.v0.X + this.v1.X + this.v2.X;
            double y = this.v0.Y + this.v1.Y + this.v2.Y;
            double z = this.v0.Z + this.v1.Z + this.v2.Z;
            return new Vector3(x, y, z) / 3.0f;
        }

        public double GetRadius()
        {
            Vector3 center = GetCenter();
            double r0 = (v0 - center).Length();
            double r1 = (v1 - center).Length();
            double r2 = (v2 - center).Length();
            return Math.Max(Math.Max(r0, r1), r2);
        }

        /// <summary>
        /// The material of the triangle.
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
