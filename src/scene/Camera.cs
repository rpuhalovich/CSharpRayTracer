using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    /// <summary>
    /// Grouping camera parameters in case I want to create a
    /// custom camera controller.
    /// </summary>
    class Camera
    {
        Image outputImage;
        private double fov;
        private double aspectRatio;
        private PixelIndex pind;
        private Vector3 origin;

        private int aaMultiplier;
        private int numRays;
        private Vector2[,] subPixelGrid;

        private Vector3 cameraPosition, cameraAxis;
        private double cameraAngle;

        private readonly double apertureRadius, focalLength;

        /// <param name="fov">Entered in degrees.</param>
        public Camera(SceneOptions options, Image outputImage, double fov)
        {
            this.outputImage = outputImage;
            this.aspectRatio = ((double) outputImage.Width) / outputImage.Height;
            this.fov = (Math.PI / 180) * fov;
            this.pind = new PixelIndex(0, 0);

            this.aaMultiplier = options.AAMultiplier;
            this.numRays = options.AAMultiplier * options.AAMultiplier;
            this.subPixelGrid = new Vector2[aaMultiplier, aaMultiplier];
            
            double increment = 1.0f / (aaMultiplier + 1);
            for (int i = 0; i < aaMultiplier; i++)
                for (int j = 0; j < aaMultiplier; j++)
                    subPixelGrid[i,j] = new Vector2(increment * (1 + i), increment * (1 + j));

            this.cameraPosition = options.CameraPosition;
            this.cameraAxis = options.CameraAxis;
            this.cameraAngle = options.CameraAngle;

            this.focalLength = options.FocalLength;
            this.apertureRadius = options.ApertureRadius;
        }

        /// <returns>An array of Rays that are to be iterated over to calc the current pixel color.</returns>
        public Ray[] CalcPixelRays()
        {
            List<Ray> rays = new List<Ray>();
            foreach (Vector2 offset in this.subPixelGrid)
            {
                double x = (double)(this.Pind.X + offset.X) / this.OutputImage.Width;
                double y = (double)(this.Pind.Y + offset.Y) / this.OutputImage.Height;
                double z = this.FocalLength;

                double x_adj = (x * 2.0f) - 1.0f;
                double y_adj = 1.0f - (y * 2.0f);

                x_adj *= Math.Tan(this.Fov / 2.0f);
                y_adj *= Math.Tan(this.Fov / 2.0f) / this.AspectRatio;

                rays.Add(new Ray(this.Origin, new Vector3(x_adj, y_adj, z)));
            }
            return rays.ToArray();
        }

        public void WriteColor(Color c)
        {
            double scale = 1.0f / this.numRays;
            this.outputImage.SetPixel(this.pind.X, this.pind.Y, Color.Clamp(c * scale));
        }

        /// <summary>
        /// Because the conditional in Visual Studio is unpractically slow.
        /// </summary>
        public bool PixelIndexDebug(int x, int y)
        {
            if (this.Pind.X == x && this.Pind.Y == y)
                return true;
            return false;
        }

        public Image OutputImage { get => outputImage; set => outputImage = value; }
        public double Fov { get => fov; set => fov = value; }
        public double AspectRatio { get => aspectRatio; set => aspectRatio = value; }
        public PixelIndex Pind { get => pind; set => pind = value; }
        public Vector3 Origin { get => origin; set => origin = value; }
        public int AaMultiplier { get => aaMultiplier; set => aaMultiplier = value; }
        public Vector3 CameraPosition { get => cameraPosition; set => cameraPosition = value; }
        public Vector3 CameraAxis { get => cameraAxis; set => cameraAxis = value; }
        public double CameraAngle { get => cameraAngle; set => cameraAngle = value; }

        public double ApertureRadius => apertureRadius;

        public double FocalLength => focalLength;

        public int NumRays { get => numRays; set => numRays = value; }
    }
}
