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
        public Vector3 Refract(double ir)
        {
            Vector3 n = this.normal, i = this.incident;

            double cosi = Math.Clamp(n.Dot(i), -1.0f, 1.0f);
            double etai = 1.0f, etat = ir;

            if (cosi < 0.0f)
            {
                cosi = -cosi;
            }
            else
            {
                n = -n;
                MyMath.Swap(ref etai, ref etat);
            }

            double eta = etai / etat;

            double k = 1 - eta * eta * (1 - cosi * cosi);
            return k < 0 ? new Vector3(0.0f, 0.0f, 0.0f) : (eta * i + (eta * cosi - Math.Sqrt(k)) * n).Normalized();
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
    }
}
