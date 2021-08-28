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

        private int aaMultiplier; // Not sure if this should be here.

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
            this.cameraPosition = options.CameraPosition;
            this.cameraAxis = options.CameraAxis;
            this.cameraAngle = options.CameraAngle;

            this.focalLength = options.FocalLength;
            this.apertureRadius = options.ApertureRadius;
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
    }
}
