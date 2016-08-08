using Sesion2_Lab01.com.isil.shader.d2d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.isil.graphics {
    public class NPlaneGraphic3D
    {

        // variables para dibujar nuestro primitivo
        private ushort[] mIndices;
        private float[] mVertices;

        private ShaderTextureProgram mShader;
        private NTexture2D mTexture2D;

        private float mX;
        private float mY;
        private float mZ;
        private float mScaleX;
        private float mScaleY;
        private float mScaleZ;
        private float mRotationX;
        private float mRotationY;
        private float mRotationZ;

        public float X
        {
            get { return mX; }
            set { mX = value; }
        }

        public float Y
        {
            get { return mY; }
            set { mY = value; }
        }

        public float Z
        {
            get { return mZ; }
            set { mZ = value; }
        }

        public float ScaleX
        {
            get { return mScaleX; }
            set { mScaleX = value; }
        }

        public float ScaleY
        {
            get { return mScaleY; }
            set { mScaleY = value; }
        }

        public float ScaleZ
        {
            get { return mScaleZ; }
            set { mScaleZ = value; }
        }

        public float RotationX
        {
            get { return mRotationX; }
            set
            {
                mRotationX = value;
            }
        }

        public float RotationY
        {
            get { return mRotationY; }
            set
            {
                mRotationY = value;
            }
        }

        public float RotationZ
        {
            get { return mRotationZ; }
            set
            {
                mRotationZ = value;
            }
        }

        public NPlaneGraphic3D(string path, float x, float y, float z, int numTiles) {
            this.X = x;
            this.Y = y;
            this.Z = z;

            mRotationX = 0;
            mRotationY = 0;
            mRotationZ = 0;
            mScaleX = 1;
            mScaleY = 1;
            mScaleZ = 1;

            if (numTiles > 0)
            {
                mTexture2D = new NTexture2D(NativeApplication.instance.Device, 
                    SharpDX.Direct3D11.TextureAddressMode.Mirror);
                mTexture2D.Load(path);
            }
            else {
                mTexture2D = new NTexture2D(NativeApplication.instance.Device);
                mTexture2D.Load(path);
            }

            mShader = new ShaderTextureProgram(NativeApplication.instance.Device);
            mShader.Load("Content/Fx_TexturePrimitive.fx");


            // creamos nuestro indices
            mIndices = new ushort[6];
            mIndices[0] = 0;
            mIndices[1] = 1;
            mIndices[2] = 2;
            mIndices[3] = 0;
            mIndices[4] = 2;
            mIndices[5] = 3;

            // creamos nustros vertices
            mVertices = new float[10 * 4];
            // nuestro primer vertice
            mVertices[0] = 0f; mVertices[1] = 0f; mVertices[2] = 0f; mVertices[3] = 1f; // vertex
            mVertices[4] = 1f; mVertices[5] = 1f; mVertices[6] = 1f; mVertices[7] = 1f; // color
            mVertices[8] = 0f; mVertices[9] = 0f; // texture coordinate
            // nuestro segundo vertice
            mVertices[10] = 150f; mVertices[11] = 0f; mVertices[12] = 0f; mVertices[13] = 1f; // vertex
            mVertices[14] = 1f; mVertices[15] = 1f; mVertices[16] = 1f; mVertices[17] = 1f; // color
            mVertices[18] = 1f * (1 + numTiles); mVertices[19] = 0f; // texture coordinate
            // nuestro tercer vertice
            mVertices[20] = 150f; mVertices[21] = 150f; mVertices[22] = 0f; mVertices[23] = 1f; // vertex
            mVertices[24] = 1f; mVertices[25] = 1f; mVertices[26] = 1f; mVertices[27] = 1f; // color
            mVertices[28] = 1f * (1 + numTiles); mVertices[29] = 1f * (1 + numTiles); // texture coordinate
            // nuestro cuarto vertice
            mVertices[30] = 0f; mVertices[31] = 150f; mVertices[32] = 0f; mVertices[33] = 1f; // vertex
            mVertices[34] = 1f; mVertices[35] = 1f; mVertices[36] = 1f; mVertices[37] = 1f; // color
            mVertices[38] = 0f; mVertices[39] = 1f * (1 + numTiles); // texture coordinate
        }

        public void ChangeColor(float r, float g, float b, float a)
        {
            mVertices[4] = r; mVertices[5] = g; mVertices[6] = b; mVertices[7] = a; // color
            mVertices[14] = r; mVertices[15] = g; mVertices[16] = b; mVertices[17] = a; // color
            mVertices[24] = r; mVertices[25] = g; mVertices[26] = b; mVertices[27] = a; // color
            mVertices[34] = r; mVertices[35] = g; mVertices[36] = b; mVertices[37] = a; // color
        }

        public void Draw(RenderCamera camera, int dt)
        {
            float old_x = mShader.X;
            float old_y = mShader.Y;
            float old_z = mShader.Z;
            float old_scaleX = mShader.ScaleX;
            float old_scaleY = mShader.ScaleY;
            float old_scaleZ = mShader.ScaleZ;
            float old_RotationX = mShader.RotationX;
            float old_RotationY = mShader.RotationY;
            float old_RotationZ = mShader.RotationZ;

            mShader.X = mX;
            mShader.Y = mY;
            mShader.Z = mZ;
            mShader.ScaleX = mScaleX;
            mShader.ScaleY = mScaleY;
            mShader.ScaleZ = mScaleZ;
            mShader.RotationX = mRotationX;
            mShader.RotationY = mRotationY;
            mShader.RotationZ = mRotationZ;

            mShader.Update(mVertices, mIndices);
            mShader.Draw(camera.transformed, mTexture2D);

            mShader.X = old_x;
            mShader.Y = old_y;
            mShader.Z = old_z;
            mShader.ScaleX = old_scaleX;
            mShader.ScaleY = old_scaleY;
            mShader.ScaleZ = old_scaleZ;
            mShader.RotationX = old_RotationX;
            mShader.RotationY = old_RotationY;
            mShader.RotationZ = old_RotationZ;
        }
    }
}
