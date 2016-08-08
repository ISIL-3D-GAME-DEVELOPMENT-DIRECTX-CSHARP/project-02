using Sesion2_Lab01.com.isil.shader.d2d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.isil.graphics {
    public class NSkyBox3D {

        private float mTileWidth;
        private float mTileHeight;
        private NTexture2D mTexture2D;

        // variables para dibujar nuestro primitivo
        private ushort[] mIndices;
        private dtVertexTextureColor[] mVertices;

        private ShaderTextureProgram mShader;

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


        public NSkyBox3D(string path, float tile_width, float tile_height, float size)
        {
            mTileWidth = tile_width;
            mTileHeight = tile_height;

            float hsize = size / 2f;

            mTexture2D = new NTexture2D(NativeApplication.instance.Device);
            mTexture2D.Load(path);

            mShader = new ShaderTextureProgram(NativeApplication.instance.Device);
            mShader.Load("Content/Fx_TexturePrimitive.fx");

            mVertices = new dtVertexTextureColor[24];
            // cara de atras
            mVertices[0] = new dtVertexTextureColor(-hsize, -hsize, -hsize, 0.25f, 0.333334f);
            mVertices[1] = new dtVertexTextureColor(hsize, -hsize, -hsize, 0.5f, 0.333334f);
            mVertices[2] = new dtVertexTextureColor(hsize, hsize, -hsize, 0.5f, 0.6666667f);
            mVertices[3] = new dtVertexTextureColor(-hsize, hsize, -hsize, 0.25f, 0.6666667f);

            // cara de adelante
            mVertices[4] = new dtVertexTextureColor(-hsize, -hsize, hsize, 1f, 0.333334f);
            mVertices[5] = new dtVertexTextureColor(hsize, -hsize, hsize, 0.75f, 0.333334f);
            mVertices[6] = new dtVertexTextureColor(hsize, hsize, hsize, 0.75f, 0.6666667f);
            mVertices[7] = new dtVertexTextureColor(-hsize, hsize, hsize, 1f, 0.6666667f);

            // cara de izquierda
            mVertices[8] = new dtVertexTextureColor(-hsize, -hsize, -hsize, 0.25f, 0.333334f);
            mVertices[9] = new dtVertexTextureColor(-hsize, -hsize, hsize, 0f, 0.333334f);
            mVertices[10] = new dtVertexTextureColor(-hsize, hsize, hsize, 0f, 0.6666667f);
            mVertices[11] = new dtVertexTextureColor(-hsize, hsize, -hsize, 0.25f, 0.6666667f);

            // cara de derecha
            mVertices[12] = new dtVertexTextureColor(hsize, -hsize, -hsize, 0.5f, 0.333334f);
            mVertices[13] = new dtVertexTextureColor(hsize, -hsize, hsize, 0.75f, 0.333334f);
            mVertices[14] = new dtVertexTextureColor(hsize, hsize, hsize, 0.75f, 0.6666667f);
            mVertices[15] = new dtVertexTextureColor(hsize, hsize, -hsize, 0.5f, 0.6666667f);

            // cara de arriba
            mVertices[16] = new dtVertexTextureColor(-hsize, -hsize, -hsize, 0.25f, 0.333334f);
            mVertices[17] = new dtVertexTextureColor(hsize, -hsize, -hsize, 0.5f, 0.333334f);
            mVertices[18] = new dtVertexTextureColor(hsize, -hsize, hsize, 0.5f, 0f);
            mVertices[19] = new dtVertexTextureColor(-hsize, -hsize, hsize, 0.25f, 0f);

            // cara de abajo
            mVertices[20] = new dtVertexTextureColor(-hsize, hsize, -hsize, 0.25f, 0.6666667f);
            mVertices[21] = new dtVertexTextureColor(hsize, hsize, -hsize, 0.5f, 0.6666667f);
            mVertices[22] = new dtVertexTextureColor(hsize, hsize, hsize, 0.5f, 1f);
            mVertices[23] = new dtVertexTextureColor(-hsize, hsize, hsize, 0.25f, 1f);

            int ic = 0;

            mIndices = new ushort[6 * 6];
            // cara de atras
            mIndices[ic++] = 0; mIndices[ic++] = 1; mIndices[ic++] = 2;
            mIndices[ic++] = 0; mIndices[ic++] = 2; mIndices[ic++] = 3;
            // cara de adelante
            mIndices[ic++] = 4; mIndices[ic++] = 5; mIndices[ic++] = 6;
            mIndices[ic++] = 4; mIndices[ic++] = 6; mIndices[ic++] = 7;
            // cara de izquierda
            mIndices[ic++] = 8; mIndices[ic++] = 9; mIndices[ic++] = 10;
            mIndices[ic++] = 8; mIndices[ic++] = 10; mIndices[ic++] = 11;
            // cara de derecha
            mIndices[ic++] = 12; mIndices[ic++] = 13; mIndices[ic++] = 14;
            mIndices[ic++] = 12; mIndices[ic++] = 14; mIndices[ic++] = 15;
            // cara de arriba
            mIndices[ic++] = 16; mIndices[ic++] = 17; mIndices[ic++] = 18;
            mIndices[ic++] = 16; mIndices[ic++] = 18; mIndices[ic++] = 19;
            // cara de abajo
            mIndices[ic++] = 20; mIndices[ic++] = 22; mIndices[ic++] = 21;
            mIndices[ic++] = 20; mIndices[ic++] = 23; mIndices[ic++] = 22;
        }

        public void UpdateDraw(RenderCamera camera, int dt)
        {
            mShader.Update(mVertices, mIndices);
            mShader.Draw(camera.transformed, mTexture2D);
        }

        public void Free()
        {
            mShader = null;

            mIndices = null;
            mVertices = null;
            
        }

    }
}
