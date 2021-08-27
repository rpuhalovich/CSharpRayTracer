using System;
using System.Collections.Generic;

namespace RayTracer
{
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
            double fov = (Math.PI / 180) * 60.0f;
            double aspectRatio = ((double) outputImage.Width) / outputImage.Height;

            for (int i = 0; i < outputImage.Width; i++)
                for (int j = 0; j < outputImage.Height; j++)
                    PixelIteration(i, j, outputImage, fov, aspectRatio);
        }

        /// <summary>
        /// Purely for use in the double for loop in the Render method.
        /// </summary>
        private void PixelIteration(int i, int j, Image outputImage, double fov, double aspectRatio)
        {
            double x = (double)(i + 0.5f) / outputImage.Width;
            double y = (double)(j + 0.5f) / outputImage.Height;
            double z = options.FocalLength;

            double x_adj = (x * 2.0f) - 1.0f;
            double y_adj = 1.0f - (y * 2.0f);

            x_adj *= Math.Tan(fov / 2.0f);
            y_adj *= Math.Tan(fov / 2.0f) / aspectRatio;

            Ray r = new Ray(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(x_adj, y_adj, z));

            // Finding the nearest hit point to the camera.
            RayHit closest = RayHit.MaxRayHit();
            foreach (SceneEntity e in entities)
            {
                RayHit rh = e.Intersect(r);
                if (rh != null && rh.Position.Z < closest.Position.Z) closest = rh;
            }

            if (closest.Material != null) outputImage.SetPixel(i, j, closest.Material.Color);
        }
    }
}
