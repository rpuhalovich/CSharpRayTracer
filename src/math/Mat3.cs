using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.src.math
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
            return new Vector3(x.x.X * );
        }

        public override string ToString()
        {
            return "(" + this.row0 + "," + this.row1 + "," + this.row2 + ")";
        }

    }
}
