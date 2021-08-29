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

        public void LogRay(int[] _rayID, Ray _ray, RayHit _rayHit)
        {
            var rayId = _rayID;
            var ray = _ray;
            RayHit rayHit = _rayHit;
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
