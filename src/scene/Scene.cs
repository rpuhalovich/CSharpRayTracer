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
        private const bool STOPWATCH = true;
        private MyStopwatch msw = new MyStopwatch();

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
            if (STOPWATCH) msw.Start();

            this.cam = new Camera(options, outputImage, 60.0f);

            for (int i = 0; i < outputImage.Width; i++)
                for (int j = 0; j < outputImage.Height; j++)
                {
                    cam.Pind = new PixelIndex(i, j);
                    PixelIteration(cam);
                }

            if (STOPWATCH) msw.Stop();
        }

        /// <summary>
        /// Purely for use in the double for loop in the Render method.
        /// </summary>
        private void PixelIteration(Camera cam)
        {
            Color pixColor = new Color(0.0f, 0.0f, 0.0f);
            foreach (Ray r in cam.CalcPixelRays())
            {
                RayHit closest = ClosestHit(r);

                // Finding the color of the nearest entity.
                if (closest != null)
                {
                    pixColor += RayColor(closest, MAX_DEPTH);
                }
            }
            cam.WriteColor(pixColor);
        }

        /// <summary>
        /// Sets the color of the pixel at pind.
        /// </summary>
        private Color RayColor(RayHit rh, int depth)
        {
            Color c = Color.Black();

            foreach (PointLight pl in lights)
            {
                // Check if rh is in shadow. If not, don't add this light to pixel color c. Ray.At to move vector along line.
                bool contrib = true;
                Vector3 lightDir = (pl.Position - rh.Position).Normalized();
                Ray r = new Ray(rh.Position, lightDir).Offset();

                RayHit hit = ClosestHit(r);

                // If the ray hits something, and it's less than the distance bettwen the ray origin and light position.
                if (hit != null && r.Origin.LengthWith(hit.Position) < r.Origin.LengthWith(pl.Position))
                {
                    // RayHit is in shadow if material is diffuse.
                    if (hit.Material.Type == Material.MaterialType.Diffuse) contrib = false;

                    // Rayhit is reflective if material is reflective.
                    if (hit.Material.Type == Material.MaterialType.Reflective)
                    {
                        //RayHit newRayHit = new RayHit(hit.Position, hit.Normal, hit.Reflect();
                        c += RayReflection(hit, depth - 1);
                    }
                }

                if (contrib)
                {
                    // Stage 2.1: C = (N^ · L^)CmCl
                    c += rh.Normal.Normalized().Dot(lightDir) * rh.Material.Color * pl.Color;
                    c = Color.Clamp(c);
                }
            }

            return c;
        }

        private Color RayReflection(RayHit rh, int depth)
        {
            if (depth <= 0) return Color.Black();

            // Do processing...

            return Color.Black();
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
