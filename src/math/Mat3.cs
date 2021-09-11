using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class Mat3
    {
        Vector3 row0;
        Vector3 row1;
        Vector3 row2;

        public Mat3(Vector3 x, Vector3 y, Vector3 z)
        {
            this.row0 = x;
            this.row1 = y;
            this.row2 = z;
        }

        public static Vector3 operator *(Mat3 x, Vector3 y)
        {
            double u = x.row0.X * y.X + x.row0.Y * y.Y + x.row0.Z * y.Z;
            double v = x.row1.X * y.X + x.row1.Y * y.Y + x.row1.Z * y.Z;
            double w = x.row2.X * y.X + x.row2.Y * y.Y + x.row2.Z * y.Z;

            return new Vector3(u, v, w);
        }

        // angle in radians.
        public static Vector3 RotateZ(double angle, Vector3 vec)
        {
            return new Vector3(
                vec.X * Math.Cos(angle) - vec.Y * Math.Sin(angle),
                vec.X * Math.Sin(angle) + vec.Y * Math.Cos(angle),
                vec.Z
                );
        }

        public static Vector3 RotateY(double angle, Vector3 vec)
        {
            return new Vector3(
                vec.X * Math.Cos(angle) + vec.Z * Math.Sin(angle),
                vec.Y,
                -1.0f * vec.X * Math.Sin(angle) + vec.Z * Math.Cos(angle)
                );
        }

        public static Vector3 RotateX(double angle, Vector3 vec)
        {
            return new Vector3(
                vec.X,
                vec.Y * Math.Cos(angle) - vec.Z * Math.Sin(angle),
                vec.Y * Math.Sin(angle) + vec.Z * Math.Cos(angle)
                );
        }

        // where rotateamt is in radians
        public static Vector3 RandomRotate(double rotateAmt, Vector3 vec)
        {
            double amt = MyMath.NextDoubleMinMax(-rotateAmt, rotateAmt);
            vec = RotateZ(amt, vec);
            amt = MyMath.NextDoubleMinMax(-rotateAmt, rotateAmt);
            vec = RotateY(amt, vec);
            amt = MyMath.NextDoubleMinMax(-rotateAmt, rotateAmt);
            vec = RotateX(amt, vec);
            return vec;
        }

        public override string ToString()
        {
            return "(" + this.row0 + "," + this.row1 + "," + this.row2 + ")";
        }

    }
}
