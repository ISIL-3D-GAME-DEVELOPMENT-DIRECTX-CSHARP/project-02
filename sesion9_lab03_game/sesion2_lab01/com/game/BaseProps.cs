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
    public class BaseProps
    {

        protected NModel mModel3DBetter;
        protected Shader3DProgram mShaderProgram;

        protected Engine mEngine;

        protected float mX;
        protected float mY;
        protected float mZ;
        protected float mRotationX;
        protected float mRotationY;
        protected float mRotationZ;
        protected float mScaleX;
        protected float mScaleY;
        protected float mScaleZ;
        protected Vector4 mTintColor;

        public float Size;
        public bool IsWaitingForDelete;

        public Vector4 TintColor
        {
            get { return mTintColor; }
            set { mTintColor = value; }
        }

        public virtual float X
        {
            get { return mX; }
            set
            {
                mX = value;
            }
        }

        public virtual float Y
        {
            get { return mY; }
            set
            {
                mY = value;
            }
        }

        public virtual float Z
        {
            get { return mZ; }
            set
            {
                mZ = value;
            }
        }

        public virtual float RotationX
        {
            get { return mRotationX; }
            set
            {
                mRotationX = value;
            }
        }

        public virtual float RotationY
        {
            get { return mRotationY; }
            set
            {
                mRotationY = value;
            }
        }

        public virtual float RotationZ
        {
            get { return mRotationZ; }
            set
            {
                mRotationZ = value;
            }
        }

        public virtual float ScaleX
        {
            get { return mScaleX; }
            set
            {
                mScaleX = value;
            }
        }

        public virtual float ScaleY
        {
            get { return mScaleY; }
            set
            {
                mScaleY = value;
            }
        }

        public virtual float ScaleZ
        {
            get { return mScaleZ; }
            set
            {
                mScaleZ = value;
            }
        }

        public BaseProps(Engine engine)
        {
            mEngine = engine;

            this.IsWaitingForDelete = false;
        }

        public virtual void Initialize(Shader3DProgram shader, NModel model)
        {
            // cargamos y construimos nuestro Shader
            mShaderProgram = shader;
            mModel3DBetter = model;

            mTintColor = Vector4.One;
            mScaleX = mScaleY = mScaleZ = 1f;
        }

        public virtual void Update(int dt)
        {

        }

        public virtual void Draw(int dt)
        {
            // mShaderProgram.RotationY += 0.01f;

            float dx = mEngine.RenderCamera.Position.X - mX;
            float dy = mEngine.RenderCamera.Position.Y - mY;
            float dz = mEngine.RenderCamera.Position.Z - mZ;
            float distancia = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

            if (distancia <= (mEngine.RenderCamera.FarPlane + this.Size))
            {
                mShaderProgram.X = mX;
                mShaderProgram.Y = mY;
                mShaderProgram.Z = mZ;
                mShaderProgram.ScaleX = mScaleX;
                mShaderProgram.ScaleY = mScaleY;
                mShaderProgram.ScaleZ = mScaleZ;
                mShaderProgram.RotationX = mRotationX;
                mShaderProgram.RotationY = mRotationY;
                mShaderProgram.RotationZ = mRotationZ;

                Vector4 old_tint_color = mShaderProgram.TintColor;
                mShaderProgram.TintColor = mTintColor;

                mModel3DBetter.Draw(mEngine.RenderCamera.transformed, dt);

                mShaderProgram.TintColor = old_tint_color;
            }
        }

        public virtual void Free() {
            mShaderProgram = null;
            mModel3DBetter = null;
            mEngine = null;
        }
    }
}
