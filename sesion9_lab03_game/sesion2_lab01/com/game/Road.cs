using CrossXDK.com.digitalkancer.modules.moderlLoaders.assimp;
using Sesion2_Lab01.com.isil.shader.d3d;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game
{
    public class Road
    {

        private NModel mModel3DBetter;
        private Shader3DProgram mShaderProgram;

        private Engine mEngine;

        private float mX;
        private float mY;
        private float mZ;

        public float X
        {
            get { return mX; }
            set
            {
                mX = value;
            }
        }

        public float Y
        {
            get { return mY; }
            set
            {
                mY = value;
            }
        }

        public float Z
        {
            get { return mZ; }
            set
            {
                mZ = value;
            }
        }

        public Road(Engine engine, Shader3DProgram shader, NModel model) {
            mEngine = engine;

            // cargamos y construimos nuestro Shader
            mShaderProgram = shader;
            mModel3DBetter = model;
        }

        public void Update(int dt)
        {

        }

        public void Draw(int dt)  {
           // mShaderProgram.RotationY += 0.01f;

            float dx = mEngine.RenderCamera.Position.X - mX;
            float dy = mEngine.RenderCamera.Position.Y - mY;
            float dz = mEngine.RenderCamera.Position.Z - mZ;
            float distancia = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

            if (distancia <= (mEngine.RenderCamera.FarPlane + 300f)) {
                mShaderProgram.X = mX;
                mShaderProgram.Y = mY;
                mShaderProgram.Z = mZ;
                mModel3DBetter.Draw(mEngine.RenderCamera.transformed, dt);
            }
        }
    }
}
