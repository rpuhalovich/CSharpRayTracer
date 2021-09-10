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

        //public 

        public override string ToString()
        {
            return "(" + this.row0 + "," + this.row1 + "," + this.row2 + ")";
        }

    }
}
