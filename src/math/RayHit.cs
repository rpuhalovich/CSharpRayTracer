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
        /// From: https://graphics.stanford.edu/courses/cs148-10-summer/docs/2006--degreve--reflection_refraction.pdf
        /// ^ignore above it's from tinyraytracer
        /// </summary>
        /// <param name="eta_i">The refraction index of the air.</param>
        public static Vector3 Refract(Vector3 incident, Vector3 norm, double eta_t, double eta_i = 1.0f)
        {
            //double cos_theta = fmin(dot(-unit_direction, rec.normal), 1.0);
            //double sin_theta = sqrt(1.0 - cos_theta * cos_theta);

            double cosi = -Math.Max(-1.0f, Math.Min(1.0f, incident.Dot(norm)));
            double sinTheta = Math.Sqrt(1.0f - cosi * cosi);
            //if (cosi < 0.0f) return Refract(incident, -norm, eta_i, eta_t);
            double eta = eta_i / eta_t;
            if (eta * sinTheta > 1.0f) return Refract(incident, -norm, eta_i, eta_t);
            double k = 1.0f - eta * eta * (1.0f - cosi * cosi);
            return k < 0.0f ? new Vector3(1.0f, 0.0f, 0.0f) : (incident * eta + norm * (eta * cosi - Math.Sqrt(k))).Normalized();
        }

        public RayHit BlankRayHit()
        {
            return new RayHit(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Material(Material.MaterialType.Diffuse, Color.Black()));
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
                return this.normal;
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
