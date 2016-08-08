using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math{
    public struct NPlane3D {
        private static NPlane3D M_EMPTY = new NPlane3D(0f, 0f, 0f, 0f);
        public static NPlane3D EMPTY { get { return NPlane3D.M_EMPTY; } }

        public float A;
        public float B;
        public float C;
        public float D;

        private NPlane3D(float a, float b, float c, float d) {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        public void Normalize() {
            float normal = (float)Math.Sqrt(A * A + B * B + C * C);

            A = A / normal;
            B = B / normal;
            C = C / normal;
            D = D / normal;
        }

        public static void Normalize(ref NPlane3D p) {
            float normal = (float)Math.Sqrt(p.A * p.A + p.B * p.B + p.C * p.C);

            p.A = p.A / normal;
            p.B = p.B / normal;
            p.C = p.C / normal;
            p.D = p.D / normal;
        }

        public static float PlaneDot(NPlane3D pp, NVector4 pv) {
            return pp.A * pv.X + pp.B * pv.Y + pp.C * pv.Z + pp.D * pv.W;
        }

        public static float PlaneDot(ref NPlane3D pp, ref NVector4 pv) {
            return pp.A * pv.X + pp.B * pv.Y + pp.C * pv.Z + pp.D * pv.W;
        }

        public static float PlaneDotCoord(NPlane3D pp, NVector4 pv) {
            return pp.A * pv.X + pp.B * pv.Y + pp.C * pv.Z + pp.D;
        }

        public static float PlaneDotCoord(ref NPlane3D pp, ref NVector4 pv) {
            return pp.A * pv.X + pp.B * pv.Y + pp.C * pv.Z + pp.D;
        }

        public static float PlaneDotCoord(ref NPlane3D pp, float x, float y, float z) {
            return pp.A * x + pp.B * y + pp.C * z + pp.D;
        }
    }
}
