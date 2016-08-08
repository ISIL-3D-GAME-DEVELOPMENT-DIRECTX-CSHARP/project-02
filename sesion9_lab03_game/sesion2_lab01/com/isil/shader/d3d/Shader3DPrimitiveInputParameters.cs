using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace Sesion2_Lab01.com.isil.shader.d3d {
    public struct Shader3DPrimitiveInputParameters
    {
        public static Shader3DPrimitiveInputParameters EMPTY = new Shader3DPrimitiveInputParameters();

        public Matrix transformation;
    }
}
