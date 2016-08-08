using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.isil.graphics
{
   
    public class PrimitiveCube3D
    {
        // variables para dibujar nuestro primitivo
        private ushort[] mIndices;
        private dtVertexColor[] mVertices;

        private float mSizeX;
        private float mSizeY;
        private float mSizeZ;

        private RenderCamera mRenderCamera;
        private Shader3DPrimitiveProgram mShader;

        public float SizeX { get { return mSizeX; } }
        public float SizeY { get { return mSizeY; } }
        public float SizeZ { get { return mSizeZ; } }

        public float X
        {
            get { return mShader.X; }
            set { mShader.X = value; }
        }

        public float Y
        {
            get { return mShader.Y; }
            set { mShader.Y = value; }
        }

        public float Z
        {
            get { return mShader.Z; }
            set { mShader.Z = value; }
        }

        public float ScaleX
        {
            get { return mShader.ScaleX; }
            set { mShader.ScaleX = value; }
        }

        public float ScaleY
        {
            get { return mShader.ScaleY; }
            set { mShader.ScaleY = value; }
        }

        public float RotationX
        {
            get { return mShader.RotationX; }
            set
            {
                mShader.RotationX = value;
            }
        }

        public float RotationY
        {
            get { return mShader.RotationY; }
            set
            {
                mShader.RotationY = value;
            }
        }

        public float RotationZ
        {
            get { return mShader.RotationZ; }
            set
            {
                mShader.RotationZ = value;
            }
        }

        public PrimitiveCube3D(float size, Color color, RenderCamera camera)
        {
            mRenderCamera = camera;
            mSizeX = mSizeY = mSizeZ = size;
            float hsize = size / 2f;

            mShader = new Shader3DPrimitiveProgram(NativeApplication.instance.Device);
            mShader.Load("Content/Fx_SimplePrimitiveTexture3D.fx");

            mVertices = new dtVertexColor[8];

            // primera cara
            mVertices[0] = new dtVertexColor(-hsize, -hsize, -hsize);
            mVertices[1] = new dtVertexColor(hsize, -hsize, -hsize);
            mVertices[2] = new dtVertexColor(hsize, hsize, -hsize);
            mVertices[3] = new dtVertexColor(-hsize, hsize, -hsize);

            // segunda cara
            mVertices[4] = new dtVertexColor(-hsize, -hsize, hsize);
            mVertices[5] = new dtVertexColor(hsize, -hsize, hsize);
            mVertices[6] = new dtVertexColor(hsize, hsize, hsize);
            mVertices[7] = new dtVertexColor(-hsize, hsize, hsize);

            //indices 6 caras por 6 indices
            int ic = 0;

            mIndices = new ushort[6 * 6];
            mIndices[ic++] = 0; mIndices[ic++] = 1; mIndices[ic++] = 2;
            mIndices[ic++] = 0; mIndices[ic++] = 2; mIndices[ic++] = 3;

            mIndices[ic++] = 6; mIndices[ic++] = 5; mIndices[ic++] = 4;
            mIndices[ic++] = 6; mIndices[ic++] = 4; mIndices[ic++] = 7;

            mIndices[ic++] = 4; mIndices[ic++] = 5; mIndices[ic++] = 1;
            mIndices[ic++] = 4; mIndices[ic++] = 1; mIndices[ic++] = 0;

            mIndices[ic++] = 2; mIndices[ic++] = 3; mIndices[ic++] = 6;
            mIndices[ic++] = 6; mIndices[ic++] = 3; mIndices[ic++] = 7;

            mIndices[ic++] = 0; mIndices[ic++] = 3; mIndices[ic++] = 7;
            mIndices[ic++] = 0; mIndices[ic++] = 7; mIndices[ic++] = 4;

            mIndices[ic++] = 1; mIndices[ic++] = 5; mIndices[ic++] = 6;
            mIndices[ic++] = 1; mIndices[ic++] = 6; mIndices[ic++] = 2;
        }

        public PrimitiveCube3D(float sizeX, float sizeY, float sizeZ, Color color, RenderCamera camera)
        {
            mRenderCamera = camera;
            mSizeX = sizeX;
            mSizeY = sizeY;
            mSizeZ = sizeZ;

            float hsizeX = sizeX / 2f;
            float hsizeY = sizeY / 2f;
            float hsizeZ = sizeZ / 2f;

            mShader = new Shader3DPrimitiveProgram(NativeApplication.instance.Device);
            mShader.Load("Content/Fx_SimplePrimitiveTexture3D.fx");

            mVertices = new dtVertexColor[8];

            // primera cara
            mVertices[0] = new dtVertexColor(-hsizeX, -hsizeY, -hsizeZ);
            mVertices[1] = new dtVertexColor(hsizeX, -hsizeY, -hsizeZ);
            mVertices[2] = new dtVertexColor(hsizeX, hsizeY, -hsizeZ);
            mVertices[3] = new dtVertexColor(-hsizeX, hsizeY, -hsizeZ);

            // segunda cara
            mVertices[4] = new dtVertexColor(-hsizeX, -hsizeY, hsizeZ);
            mVertices[5] = new dtVertexColor(hsizeX, -hsizeY, hsizeZ);
            mVertices[6] = new dtVertexColor(hsizeX, hsizeY, hsizeZ);
            mVertices[7] = new dtVertexColor(-hsizeX, hsizeY, hsizeZ);

            //indices 6 caras por 6 indices
            int ic = 0;

            mIndices = new ushort[6 * 6];
            mIndices[ic++] = 0; mIndices[ic++] = 1; mIndices[ic++] = 2;
            mIndices[ic++] = 0; mIndices[ic++] = 2; mIndices[ic++] = 3;

            mIndices[ic++] = 6; mIndices[ic++] = 5; mIndices[ic++] = 4;
            mIndices[ic++] = 6; mIndices[ic++] = 4; mIndices[ic++] = 7;

            mIndices[ic++] = 4; mIndices[ic++] = 5; mIndices[ic++] = 1;
            mIndices[ic++] = 4; mIndices[ic++] = 1; mIndices[ic++] = 0;

            mIndices[ic++] = 2; mIndices[ic++] = 3; mIndices[ic++] = 6;
            mIndices[ic++] = 6; mIndices[ic++] = 3; mIndices[ic++] = 7;

            mIndices[ic++] = 0; mIndices[ic++] = 3; mIndices[ic++] = 7;
            mIndices[ic++] = 0; mIndices[ic++] = 7; mIndices[ic++] = 4;

            mIndices[ic++] = 1; mIndices[ic++] = 5; mIndices[ic++] = 6;
            mIndices[ic++] = 1; mIndices[ic++] = 6; mIndices[ic++] = 2;
        }

        public void UpdateDraw(int dt) {
            mShader.Update(mVertices, mIndices);
            mShader.Draw(mRenderCamera.transformed);
        }

        public void Free()
        {
            mShader.Free();
            mShader = null;

            mIndices = null;
            mVertices = null;
            mRenderCamera = null;
        }

        public static bool CheckCollision(PrimitiveCube3D a, PrimitiveCube3D b) {
            //check the X axis
            bool hitX = Math.Abs(a.X - b.X) <= (a.SizeX + b.SizeX) / 2f;
            bool hitY = Math.Abs(a.Y - b.Y) <= (a.SizeY + b.SizeY) / 2f;
            bool hitZ = Math.Abs(a.Z - b.Z) <= (a.SizeZ + b.SizeZ) / 2f;

            return hitX && hitY && hitZ;
        } 
    }
}
