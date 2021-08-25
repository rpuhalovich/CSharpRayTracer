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
            double aspectRatio = outputImage.Width / outputImage.Height;

            double portHeight = 2.0f;
            double portWidth = aspectRatio * portHeight;
            double focalLen = options.FocalLength;

            Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 horizontal = new Vector3(portWidth, 0.0f, 0.0f);
            Vector3 vertical = new Vector3(0.0f, portHeight, 0.0f);
            Vector3 lowerLeft = origin - horizontal / 2.0f - vertical / 2.0f - new Vector3(0.0f, 0.0f, focalLen);

            for (int i = 0; i < outputImage.Width; i++)
            {
                for (int j = 0; j < outputImage.Width; j++)
                {
                    double x = (double)(j + 0.5f) / outputImage.Width;
                    double y = (double)(i + 0.5f) / outputImage.Height;
                    double z = options.FocalLength; // TODO: Fix weird distance.

                    double x_adj = (x * 2.0f) - 1.0f;
                    double y_adj = (1.0f - (y * 2.0f)) * -1.0f;

                    x_adj *= Math.Tan(fov / 2.0f);
                    y_adj *= (Math.Tan(fov / 2.0f) / aspectRatio);

                    //Ray r = new Ray(origin, lowerLeft + x * horizontal + y * vertical - origin);
                    Ray r = new Ray(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(x_adj, y_adj, z));

                    foreach (SceneEntity e in entities)
                    {
                        RayHit rh = e.Intersect(r);
                        if (rh != null) outputImage.SetPixel(j, i, rh.Material.Color);
                    }
                }
            }
        }
    }
}
