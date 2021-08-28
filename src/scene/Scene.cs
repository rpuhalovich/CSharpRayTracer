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
        private SceneOptions options;
        private ISet<SceneEntity> entities;
        private ISet<PointLight> lights;

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
            Camera cam = new Camera(options, outputImage, 60.0f);

            for (int i = 0; i < outputImage.Width; i++)
                for (int j = 0; j < outputImage.Height; j++)
                {
                    cam.Pind = new PixelIndex(i, j);
                    PixelIteration(cam);
                }
        }

        /// <summary>
        /// Purely for use in the double for loop in the Render method.
        /// </summary>
        private void PixelIteration(Camera cam)
        {
            Color pixColor = new Color(0.0f, 0.0f, 0.0f);
            foreach (Ray r in cam.CalcPixelRays())
            {
                // Finding the nearest hit point to the camera.
                RayHit closest = RayHit.MaxRayHit();
                foreach (SceneEntity e in entities)
                {
                    RayHit rh = e.Intersect(r);
                    if (rh != null && rh.Position.Length() < closest.Position.Length() && rh.Position.LengthWith(cam.Origin) > options.FocalLength)
                        closest = rh;
                }

                // Finding the color of the nearest entity.
                if (closest.Material != null)
                {
                    pixColor += RayColor(closest);
                }
            }
            cam.WriteColor(pixColor);
        }

        /// <summary>
        /// Sets the color of the pixel pind.
        /// </summary>
        private Color RayColor(RayHit rh)
        {
            bool contrib = true;
            double vecOffset = 0.0001f;
            Color c = new Color(0.0f, 0.0f, 0.0f);

            foreach (PointLight pl in lights)
            {
                Vector3 lightDir = (pl.Position - rh.Position).Normalized();

                // Check if rh is in shadow. If not, don't add this light to pixel color c. Ray.At to move vector along line.
                Ray r = new Ray(rh.Position, lightDir);
                r = new Ray(r.At(vecOffset), lightDir);

                foreach (SceneEntity e in entities)
                {
                    RayHit hit = e.Intersect(r);
                    if (hit != null)
                    {
                        contrib = false;
                    }
                }

                if (contrib)
                {
                    // Stage 2.1: C = (N^ · L^)CmCl
                    c += rh.Normal.Normalized().Dot(lightDir) * rh.Material.Color * pl.Color;
                    c = Color.Clamp(c);
                }

                //// Stage 2.1: C = (N^ · L^)CmCl
                //c += rh.Normal.Normalized().Dot(lightDir) * rh.Material.Color * pl.Color;
                //c = Color.Clamp(c);
            }

            return c;
        }
    }
}
