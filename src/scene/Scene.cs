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
        private const int MAX_DEPTH = 4;
        private const int NUM_DOF_RAYS = 50;

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
            {
                if (DEBUG) Console.WriteLine("Scanlines remaining: " + (outputImage.Width - i));
                for (int j = 0; j < outputImage.Height; j++)
                {
                    cam.Pind = new PixelIndex(i, j);

                    Color pixelColor = Color.Black();
                    foreach (Ray r in cam.CalcPixelRays())
                    {
                        if (options.ApertureRadius > 0.0f)
                        {
                            for (int k = 0; k < NUM_DOF_RAYS; k++)
                            {
                                Ray adj = cam.CalcAperatureColorRay(r).At(1.0f);
                                pixelColor += RayColor(adj, MAX_DEPTH) / NUM_DOF_RAYS;
                            }
                        }
                        else
                        {
                            pixelColor += RayColor(r.At(1.0f), MAX_DEPTH);
                        }
                    }
                    cam.WriteColor(pixelColor);
                }
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
            Color diffuseColor = Color.Black(), reflectColor = Color.Black(), refractColor = Color.Black(), emissiveColor = Color.Black(), glossyColor = Color.Black();

            RayHit sourceRh = ClosestHit(r);
            if (depth <= 0 || sourceRh == null) return Color.Black(); // If nothing is hit, you're off to the abyss so return bg.

            if (sourceRh.Material.Type == Material.MaterialType.Emissive)
            {
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

            if (sourceRh.Material.Type == Material.MaterialType.Glossy)
            {
                Color diffColor = Color.Black(), specColor = Color.Black();
                double n = 30.0f; // Where this is the exponent of a specular refleciton.
                double Kd = 0.8f; // phong model diffuse weight
                double Ks = 0.3f; // phong model specular weight

                foreach (PointLight pl in lights)
                {
                    Vector3 lightDir = (pl.Position - sourceRh.Position).Normalized();
                    Ray shadowRay = new Ray(sourceRh.Position, lightDir).Offset();
                    RayHit shadowRh = ClosestHit(shadowRay);
                    if (shadowRh == null) continue;
                    if (shadowRh != null && shadowRay.Origin.LengthWith(shadowRh.Position) < shadowRay.Origin.LengthWith(pl.Position)) continue;

                    // Diffuse component.
                    diffColor += sourceRh.Normal.Normalized().Dot(lightDir) * sourceRh.Material.Color * pl.Color;

                    // Spec component.
                    Vector3 reflection = shadowRh.Reflect();
                    specColor += pl.Color * Math.Pow(Math.Max(0.0f, reflection.Dot(-shadowRh.Incident)), n);
                }

                glossyColor = (diffColor * Kd + specColor * Ks) * 0.85f + RayColor(new Ray(sourceRh.Position, sourceRh.RandomishReflect()).Offset(), depth - 1) * 0.15f;
            }

            if (sourceRh.Material.Type == Material.MaterialType.Diffuse)
            {
                if (cam.PixelIndexDebug(170, 170)) Debugger.Break();
                foreach (PointLight pl in lights)
                {
                    // Ray to point light hit.
                    Vector3 lightDir = (pl.Position - sourceRh.Position).Normalized();
                    Ray shadowRay = new Ray(sourceRh.Position, lightDir).Offset();
                    RayHit shadowRh = ClosestHit(shadowRay);

                    //if (shadowRh == null) continue;
                    // If the current ray (r) is NOT in shadow (ray from intersection to light is blocked by entity).
                    if (shadowRh != null && shadowRay.Origin.LengthWith(shadowRh.Position) < shadowRay.Origin.LengthWith(pl.Position)) continue;

                    diffuseColor += sourceRh.Normal.Normalized().Dot(lightDir) * sourceRh.Material.Color * pl.Color;
                }
            }

            return emissiveColor + diffuseColor + reflectColor + refractColor + glossyColor;
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
