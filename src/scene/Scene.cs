using System;
using System.Collections.Generic;
using System.IO;

namespace RayTracer
{
    /// <summary>
    /// Groups x and y together because passing two variables around
    /// is bad.
    /// </summary>
    public struct PixelIndex
    {
        private int y;
        private int x;

        public PixelIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }

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
                    Color rayColor = Color.Black();
                    foreach (Ray r in cam.CalcPixelRays()) rayColor += RayColor(r, MAX_DEPTH);
                    cam.WriteColor(rayColor);
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
            if (depth <= 0) return Color.Black();

            // Input ray hit.
            RayHit sourceRh = ClosestHit(r);

            // If nothing is hit, you're off to the abyss so return black.
            // TODO: maybe add bg color?
            if (sourceRh == null) return Color.Black();

            logger.LogRay(new int[] { cam.Pind.X, cam.Pind.Y }, r, sourceRh);

            Color c = Color.Black();
            foreach (PointLight pl in lights)
            {
                // Ray to point light hit.
                Vector3 lightDir = (pl.Position - sourceRh.Position).Normalized();
                Ray shadowRay = new Ray(sourceRh.Position, lightDir).Offset();
                RayHit shadowRh = ClosestHit(shadowRay);

                // If the current ray (r) is NOT in shadow (ray from intersection to light is blocked by entity).
                if (!(shadowRh != null && shadowRay.Origin.LengthWith(shadowRh.Position) < shadowRay.Origin.LengthWith(pl.Position)))
                {
                    //if (cam.PixelIndexDebug(170, 170))
                    //{
                    //    int ye = 100;
                    //}

                    if (sourceRh.Material.Type == Material.MaterialType.Diffuse)
                    {
                        // Stage 2.1: C = (N^ � L^)CmCl
                        Color diffuseColor = sourceRh.Normal.Normalized().Dot(lightDir) * sourceRh.Material.Color * pl.Color;
                        return c += diffuseColor;
                    }

                    if (sourceRh.Material.Type == Material.MaterialType.Reflective)
                    {
                        Ray newRay = new Ray(sourceRh.Position, sourceRh.Reflect());
                        return c += RayColor(newRay, depth - 1); // TODO: Make reflections work.
                    }
                }
            }
            return c;
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
                if (rh != null && rh.Position.LengthWith(r.Origin) < closest.Position.LengthWith(r.Origin) && rh.Position.LengthWith(cam.Origin) > options.FocalLength)
                    closest = rh;
            }
            if (closest.Equals(RayHit.MaxRayHit())) return null;
            return closest;
        }
    }
}
