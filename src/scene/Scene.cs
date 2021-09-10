using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RayTracer
{
    /// <summary>
    /// Class to represent a ray traced scene, including the objects,
    /// light sources, and associated rendering logic.
    /// </summary>
    public class Scene
    {
        private const bool DEBUG = true;
        private MyStopwatch msw = new MyStopwatch();
        private MyLogger logger = new MyLogger();

        private const double FOV = 60.0f;
        private const int MAX_DEPTH = 10;
        private const int SHADE_SAMPLES = 50;

        private SceneOptions options;
        private ISet<SceneEntity> entities;
        private ISet<PointLight> lights;

        private Camera cam;

        /// <summary>
        /// Construct a new scene with provided options.
        /// </summary>
        /// <param name="options">Options data</param>
        public Scene(SceneOptions options = new SceneOptions())
        {
            this.options = options;
            this.entities = new HashSet<SceneEntity>();
            this.lights = new HashSet<PointLight>();
        }

        /// <summary>
        /// Add an entity to the scene that should be rendered.
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void AddEntity(SceneEntity entity)
        {
            this.entities.Add(entity);
        }

        /// <summary>
        /// Add a point light to the scene that should be computed.
        /// </summary>
        /// <param name="light">Light structure</param>
        public void AddPointLight(PointLight light)
        {
            this.lights.Add(light);
        }

        /// <summary>
        /// Render the scene to an output image. This is where the bulk
        /// of your ray tracing logic should go... though you may wish to
        /// break it down into multiple functions as it gets more complex!
        /// </summary>
        /// <param name="outputImage">Image to store render output</param>
        public void Render(Image outputImage)
        {
            if (DEBUG)
            {
                msw.Start();
            }

            this.cam = new Camera(options, outputImage, FOV);

            for (int i = 0; i < outputImage.Width; i++)
                for (int j = 0; j < outputImage.Height; j++)
                {
                    cam.Pind = new PixelIndex(i, j);

                    Color pixelColor = Color.Black();
                    foreach (Ray r in cam.CalcPixelRays())
                    {
                        pixelColor += RayColor(r, MAX_DEPTH);
                    }
                    cam.WriteColor(pixelColor);
                }

            if (DEBUG)
            {
                msw.Stop();
                logger.WriteFile();
            }
        }

        /// <summary>
        /// Sets the color of the input camera ray.
        /// </summary>
        private Color RayColor(Ray r, int depth)
        {
            Color diffuseColor = Color.Black(), reflectColor = Color.Black(), refractColor = Color.Black(), emissiveColor = Color.Black();

            RayHit sourceRh = ClosestHit(r);
            if (depth <= 0 || sourceRh == null) return Color.Black(); // If nothing is hit, you're off to the abyss so return bg.

            // Get emitted color.
            if (sourceRh.Material.Type == Material.MaterialType.Emissive)
            {
                //emissiveColor = RayColor(new Ray(sourceRh.Position, sourceRh.EmittedDir()).Offset(), depth - 1);
                emissiveColor = sourceRh.Emitted();
            }

            if (sourceRh.Material.Type == Material.MaterialType.Reflective)
            {
                reflectColor = RayColor(new Ray(sourceRh.Position, sourceRh.Reflect()).Offset(), depth - 1);
            }

            if (sourceRh.Material.Type == Material.MaterialType.Refractive)
            {
                double kr = sourceRh.Fresnel(sourceRh.Material.RefractiveIndex);

                Color refractHitColor = Color.Black();
                if (kr < 1.0f)
                {
                    Vector3 refractDir = sourceRh.Refract(sourceRh.Material.RefractiveIndex);
                    Ray recurseRay = new Ray(Vector3.Offset(sourceRh.Position, r.Direction), refractDir);
                    refractHitColor = RayColor(recurseRay, depth - 1);
                }

                Color reflectHitColor = RayColor(new Ray(sourceRh.Position, sourceRh.Reflect()).Offset(), depth - 1);
                refractColor += reflectHitColor * kr + refractHitColor * (1.0f - kr);
            }

            if (sourceRh.Material.Type == Material.MaterialType.Diffuse)
            {
                foreach (PointLight pl in lights)
                {
                    // Ray to point light hit.
                    Vector3 lightDir = (pl.Position - sourceRh.Position).Normalized();
                    Ray shadowRay = new Ray(sourceRh.Position, lightDir).Offset();
                    RayHit shadowRh = ClosestHit(shadowRay);

                    // If the current ray (r) is NOT in shadow (ray from intersection to light is blocked by entity).
                    if (shadowRh != null && shadowRay.Origin.LengthWith(shadowRh.Position) < shadowRay.Origin.LengthWith(pl.Position)) continue;

                    diffuseColor += sourceRh.Normal.Normalized().Dot(lightDir) * sourceRh.Material.Color * pl.Color;
                }

                foreach (SceneEntity e in entities)
                {
                    if (!(e.Material.Type == Material.MaterialType.Emissive)) continue;

                    for (int i = 0; i < SHADE_SAMPLES; i++)
                    {
                        // Ray to point light hit.
                        Vector3 lightDir = Vector3.RandomHemisphere(sourceRh.Normal);
                        Ray shadowRay = new Ray(sourceRh.Position, lightDir).Offset();
                        RayHit shadowRh = ClosestHit(shadowRay);

                        // If the current ray (r) is NOT in shadow (ray from intersection to light is blocked by entity).
                        if (shadowRh != null && shadowRh.Material.Type != Material.MaterialType.Emissive) continue;

                        diffuseColor += (sourceRh.Normal.Normalized().Dot(lightDir) * sourceRh.Material.Color * e.Material.Color) * (1.0f / SHADE_SAMPLES) * 3.0f;
                    }
                }
            }

            return emissiveColor + diffuseColor + reflectColor + refractColor;
        }



        /// <summary>
        /// Finds the nearest hit point to the ray origin.
        /// Returns null if no hit occurs.
        /// </summary>
        private RayHit ClosestHit(Ray r)
        {
            RayHit closest = RayHit.MaxRayHit();
            foreach (SceneEntity e in entities)
            {
                RayHit rh = e.Intersect(r);
                if (rh != null && rh.Position.LengthWith(r.Origin) < closest.Position.LengthWith(r.Origin))
                {
                    closest = rh;
                }
            }
            if (closest.Equals(RayHit.MaxRayHit())) return null;
            return closest;
        }
    }
}
