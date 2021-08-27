using System;
using System.Collections.Generic;

namespace RayTracer
{
    /// <summary>
    /// Groups x and y together because passing two variables around
    /// is bad.
    /// </summary>
    public struct PixelIndex
    {
        public int x, y;

        public PixelIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// Class to represent a ray traced scene, including the objects,
    /// light sources, and associated rendering logic.
    /// </summary>
    public class Scene
    {
        private const int MAX_DEPTH = 50;
        private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

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
            double fov = (Math.PI / 180) * 60.0f;
            double aspectRatio = ((double) outputImage.Width) / outputImage.Height;

            PixelIndex pind = new PixelIndex(0, 0);
            for (int i = 0; i < outputImage.Width; i++)
                for (int j = 0; j < outputImage.Height; j++)
                {
                    pind.x = i;
                    pind.y = j;
                    PixelIteration(pind, outputImage, fov, aspectRatio);
                }
        }

        /// <summary>
        /// Purely for use in the double for loop in the Render method.
        /// </summary>
        private void PixelIteration(PixelIndex pind, Image outputImage, double fov, double aspectRatio)
        {
            double x = (double)(pind.x + 0.5f) / outputImage.Width;
            double y = (double)(pind.y + 0.5f) / outputImage.Height;
            double z = options.FocalLength;

            double x_adj = (x * 2.0f) - 1.0f;
            double y_adj = 1.0f - (y * 2.0f);

            x_adj *= Math.Tan(fov / 2.0f);
            y_adj *= Math.Tan(fov / 2.0f) / aspectRatio;

            Ray r = new Ray(this.origin, new Vector3(x_adj, y_adj, z));

            // Finding the nearest hit point to the camera.
            RayHit closest = RayHit.MaxRayHit();
            foreach (SceneEntity e in entities)
            {
                RayHit rh = e.Intersect(r);
                if (rh != null && rh.Position.Length() < closest.Position.Length()) closest = rh;
            }

            if (closest.Material != null) outputImage.SetPixel(pind.x, pind.y, closest.Material.Color);
        }

        /// <summary>
        /// Sets the color of the pixel pind.
        /// </summary>
        private Color RayColor(Ray r, int depth)
        {
            //if (depth <= 0) return new Color(0.0f, 0.0f, 0.0f);

            //foreach (SceneEntity e in entities)
            //{
            //    RayHit newHit = e.Intersect(r);
            //    if (newHit != null)
            //    {
            //        Vector3 target = newHit.Position + newHit.Normal + Vector3.RandomNormSphere();
            //        return RayColor(new Ray(newHit.Position, target - newHit.Position), depth - 1) * 0.5f;
            //    }
            //}

            // C = (N^ · L^)CmCl
            // Color C =

            return new Color(0.0f, 0.0f, 0.0f);
        }
    }
}
