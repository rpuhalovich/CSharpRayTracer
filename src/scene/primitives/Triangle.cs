using System;

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
        /// Determine if a ray intersects with the triangle, and if so, return hit data.
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <returns>Hit data (or null if no intersection)</returns>
        public RayHit Intersect(Ray ray)
        {
            double epsilon = 0.0001f;

            // Compute planes normal
            Vector3 v0v1 = v1 - v0;
            Vector3 v0v2 = v2 - v0;
            Vector3 norm = v0v1.Cross(v0v2);
            double area2 = norm.Length();

            // Find P
            double normDotRayDir = norm.Dot(ray.Direction);
            if (Math.Abs(normDotRayDir) < epsilon) return null;

            double d = norm.Dot(v0);

            double t = (norm.Dot(ray.Origin) + d) / normDotRayDir;
            if (t > 0) return null; // left hand rule triangle is behind.

            Vector3 P = ray.Origin + t * ray.Direction;

            Vector3 C; // inside-outside test

            // edge0
            Vector3 edge0 = v1 - v0;
            Vector3 vp0 = P - v0;
            C = edge0.Cross(vp0);
            if (norm.Dot(C) < 0) return null;

            // edge1
            Vector3 edge1 = v2 - v1;
            Vector3 vp1 = P - v1;
            C = edge0.Cross(vp1);
            if (norm.Dot(C) < 0) return null;
        
            // edge2
            Vector3 edge2 = v0 - v2;
            Vector3 vp2 = P - v2;
            C = edge0.Cross(vp2);
            if (norm.Dot(C) < 0) return null;

            return new RayHit(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), this.material);
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
