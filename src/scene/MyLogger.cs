using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RayTracer
{
    /// <summary>
    /// Wrapper for Andrew's logger: https://github.com/shangzhel/RayTracer.Debug
    /// </summary>
    class MyLogger
    {
        private shangzhel.RayTracer.Debug.Logger logger = new shangzhel.RayTracer.Debug.Logger();
        private const String raysFile = "extern/RayTracer.Debug.Gui/Assets/Rays/debug.rays";

        public void LogRay(int[] rayId, Ray ray, RayHit rayHit)
        {
            this.logger.Log(rayId, ray.Origin, rayHit.Position);
        }

        public void WriteFile()
        {
            using (var stream = new System.IO.FileStream(raysFile, FileMode.Create))
            {
                this.logger.WriteToStream(stream);
            }
        }
    }
}
