using Sesion2_Lab01.com.isil.shader.d2d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.isil.graphics {

    public class NSprite2D {

        // variables para dibujar nuestro primitivo
        private ushort[] mIndices;
        private float[] mVertices;

        private ShaderTextureProgram mShader;
        private NTexture2D mTexture2D;

        private float mX;
        private float mY;
        private float mScaleX;
        private float mScaleY;
        private float mRotationX;
        private float mRotationY;
        private float mRotationZ;

        public float X {
            get { return mX; }
            set { mX = value; }
        }

        public float Y {
            get { return mY; }
            set { mY = value; }
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

        public NSprite2D (string path, int x, int y) {
            mTexture2D = new NTexture2D(NativeApplication.instance.Device);
            mTexture2D.Load(path);

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
            mVertices[18] = 1f; mVertices[19] = 0f; // texture coordinate
            // nuestro tercer vertice
            mVertices[20] = 150f; mVertices[21] = 150f; mVertices[22] = 0f; mVertices[23] = 1f; // vertex
            mVertices[24] = 1f; mVertices[25] = 1f; mVertices[26] = 1f; mVertices[27] = 1f; // color
            mVertices[28] = 1f; mVertices[29] = 1f; // texture coordinate
            // nuestro cuarto vertice
            mVertices[30] = 0f; mVertices[31] = 150f; mVertices[32] = 0f; mVertices[33] = 1f; // vertex
            mVertices[34] = 1f; mVertices[35] = 1f; mVertices[36] = 1f; mVertices[37] = 1f; // color
            mVertices[38] = 0f; mVertices[39] = 1f; // texture coordinate
        }

        public void SetShader(ShaderTextureProgram shader) {
            mShader = shader;
        }

        public void ChangeColor(float r, float g, float b, float a)
        {
            mVertices[4]  = r; mVertices[5]  = g; mVertices[6]  = b; mVertices[7]  = a; // color
            mVertices[14] = r; mVertices[15] = g; mVertices[16] = b; mVertices[17] = a; // color
            mVertices[24] = r; mVertices[25] = g; mVertices[26] = b; mVertices[27] = a; // color
            mVertices[34] = r; mVertices[35] = g; mVertices[36] = b; mVertices[37] = a; // color
        }

        public void Draw(RenderCamera camera, int dt) {
            float old_x = mShader.X;
            float old_y = mShader.Y;
            float old_scaleX = mShader.ScaleX;
            float old_scaleY = mShader.ScaleY;
            float old_RotationX = mShader.RotationX;
            float old_RotationY = mShader.RotationY;
            float old_RotationZ = mShader.RotationZ;

            mShader.X = mX;
            mShader.Y = mY;
            mShader.ScaleX = mScaleX;
            mShader.ScaleY = mScaleY;
            mShader.RotationX = mRotationX;
            mShader.RotationY = mRotationY;
            mShader.RotationZ = mRotationZ;

            mShader.Update(mVertices, mIndices);
            mShader.Draw(camera.transformed, mTexture2D);

            mShader.X = old_x;
            mShader.Y = old_y;
            mShader.ScaleX = old_scaleX;
            mShader.ScaleY = old_scaleY;
            mShader.RotationX = old_RotationX;
            mShader.RotationY = old_RotationY;
            mShader.RotationZ = old_RotationZ;
        }
    }
}
