using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Contains information about a shadow ray for soft shadow calculations.
/// </summary>
namespace RayTracer
{
    class ShadowRay
    {
        private RayHit rh;
        private Ray shr;
        private double coneAngle;

        public ShadowRay(RayHit rh, Ray shr, double coneAngle)
        {
            this.Rh = rh;
            this.Shr = shr;
            this.ConeAngle = coneAngle;
        }

        public RayHit Rh { get => rh; set => rh = value; }
        public Ray Shr { get => shr; set => shr = value; }
        public double ConeAngle { get => coneAngle; set => coneAngle = value; }
    }
}
