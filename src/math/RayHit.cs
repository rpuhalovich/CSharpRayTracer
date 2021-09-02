using System;
using System.Collections.Generic;

namespace RayTracer
{
    /// <summary>
    /// Class to represent ray hit data, including the position and
    /// normal of a hit (and optionally other computed vectors).
    /// </summary>
    public class RayHit
    {
        private Vector3 position;
        private Vector3 normal;
        private Vector3 incident;
        private Material material;

        private bool frontFace;

        public RayHit(Vector3 position, Vector3 normal, Vector3 incident, Material material)
        {
            this.position = position;
            this.normal = normal;
            this.incident = incident;
            this.material = material;
        }

        // You may wish to write methods to compute other vectors,
        // e.g. reflection, transmission, etc

        public static RayHit MaxRayHit()
        {
            return new RayHit(
                new Vector3(Double.MaxValue, Double.MaxValue, Double.MaxValue),
                new Vector3(Double.MaxValue, Double.MaxValue, Double.MaxValue),
                new Vector3(Double.MaxValue, Double.MaxValue, Double.MaxValue),
                null
            );
        }

        public override string ToString()
        {
            return "[Position: " + this.position + ", Normal: " + this.normal + ", Incident: " + this.incident + "]";
        }

        public void SetFaceNormal()
        {
            this.FrontFace = this.incident.Dot(this.Normal) < 0.0f;
            this.normal = this.FrontFace ? this.Normal : this.Normal * -1.0f;
        }

        /// <summary>
        /// From: https://raytracing.github.io/books/RayTracingInOneWeekend.html#metal/mirroredlightreflection
        /// return v - 2*dot(v,n)*n;
        /// </summary>
        public Vector3 Reflect()
        {
            return (this.incident - 2.0f * this.incident.Dot(this.normal) * this.Normal).Normalized();
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Refract(double etaiOverEtat)
        {
            double cosTheta = Math.Clamp(this.Normal.Dot(this.incident), -1.0f, 1.0f);
            Vector3 rOutPerp = etaiOverEtat * (this.incident + cosTheta * this.Normal);
            Vector3 rOutParallell = -Math.Sqrt(Math.Abs(1.0f - rOutPerp.LengthSq())) * this.Normal;
            return rOutPerp + rOutParallell;
        }

        /// <summary>
        /// Offsets the position (intersection pos) along the set normal.
        /// </summary>
        public RayHit OffsetNormal()
        {
            Vector3 newPos = new Ray(this.position, this.normal).Offset().Origin;
            return new RayHit(newPos, this.normal, this.incident, this.material);
        }

        public override bool Equals(object obj)
        {
            return obj is RayHit hit &&
                   EqualityComparer<Vector3>.Default.Equals(position, hit.position) &&
                   EqualityComparer<Vector3>.Default.Equals(normal, hit.normal) &&
                   EqualityComparer<Vector3>.Default.Equals(incident, hit.incident) &&
                   EqualityComparer<Material>.Default.Equals(material, hit.material);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(position, normal, incident, material);
        }

        public Vector3 Position
        {
            get
            {
                return this.position;
            }
        }

        public Vector3 Normal
        {
            get
            {
                return this.normal.Normalized();
            }
        }

        public Vector3 Incident
        {
            get
            {
                return this.incident;
            }
        }

        public Material Material
        {
            get
            {
                return this.material;
            }
        }

        public bool FrontFace { get => frontFace; set => frontFace = value; }
    }
}
