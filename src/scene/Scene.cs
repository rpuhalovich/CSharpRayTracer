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
        // Begin writing your code here...
        // Camera
        Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

        Vector3 focalLength = new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 horiz = new Vector3(outputImage.Width, 0.0f, 0.0f);
        Vector3 vert = new Vector3(0.0f, outputImage.Height, 0.0f);
        
        Vector3 bottomLeft = origin - horiz / 2 - vert / 2;

        for (int i = 0; i < outputImage.Height; i++)
        {
            for (int j = 0; j < outputImage.Width; j++)
            {
                // Calculate ray
                double x = ((double) j) / outputImage.Width;
                double y = ((double) i) / outputImage.Height;
                Ray r = new Ray(origin, bottomLeft + x * horiz + y * vert - origin);

                outputImage.SetPixel(j, i, r.rayColor(r)); // this is dumb but temporary

                //foreach (SceneEntity e in entities)
                //{
                //    RayHit rh = e.Intersect(r);
                //    if (rh != null)
                //        outputImage.SetPixel(j, i, new Color(1.0f, 0.0f, 0.0f));
                //    else
                //        outputImage.SetPixel(j, i, new Color(0.0f, 0.3f, 1.0f));
                //}
            }
        }
    }
}
}
