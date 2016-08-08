using CrossXDK.com.digitalkancer.modules.moderlLoaders.assimp;
using Sesion2_Lab01.com.isil.graphics;
using Sesion2_Lab01.com.isil.shader.d3d;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesion2_Lab01.com.game
{
    public class Player
    {
        
        private NModel mModel3DBetter;
        private Shader3DProgram mShaderProgram;

        private Engine mEngine;
        private PrimitiveCube3D mDebugBox;

        private bool mIsLeft;
        private bool mIsRight;
        private bool mIsUp;
        private bool mCanJump;
        private bool mIsJumping;
        private bool mHitWithBarrier;

        private float mX;
        private float mY;
        private float mInitY;
        private float mZ;
        private float mJumpingFactor;
        private float mJumpSpeed;
        private float mGravity;
        private float mTopSpeed;
        private float mAcceleration;
        private float mCurrentSpeed;
        private float mSteeringFactor;

        private int mLifes;

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

        public PrimitiveCube3D CollisionBox { get { return mDebugBox; } }
        public float CurrentSpeed { get { return mCurrentSpeed; } }

        public Player(Engine engine)
        {
            mEngine = engine;
            mEngine.RenderCamera.CanControlInput = false;

            mInitY = 0f;
            mIsLeft = false;
            mIsRight = false;
            mIsUp = false;
            mCanJump = false;
            mIsJumping = false;
            mHitWithBarrier = false;
            mTopSpeed = 0.5f;
            mAcceleration = 0.01f;
            mCurrentSpeed = 0f;
            mSteeringFactor = 0.25f;
            mJumpSpeed = 10f;
            mGravity = 1.4f;
            mJumpingFactor = 0f;

            mLifes = 3;
            mEngine.GameHud.SetHeartQuantity(mLifes);

            // cargamos y construimos nuestro Shader
            mShaderProgram = new Shader3DProgram(mEngine.Device);
            mShaderProgram.Load("Content/Fx_PrimitiveTexture3D.fx");
            this.Z = -200f;
            //mShaderProgram.Z = -500f;

            NCrossModelLoader cl = new NCrossModelLoader();

            // Pivot configurado
            Matrix initTrans = Matrix.RotationY(3.14f) * Matrix.Translation(0, -100, 0) * Matrix.Scaling(0.4f);

            mModel3DBetter = cl.Load("Content/game_example/auto/Cobra RX55.obj", initTrans);
            mModel3DBetter.SetShader(mShaderProgram);

            mDebugBox = new PrimitiveCube3D(15f, 40f, 15f, Color.Green, mEngine.RenderCamera);
            mDebugBox.X = mX;
            mDebugBox.Y = mY;
            mDebugBox.Z = mZ + 180;

        }

        public void OnKeyDown(int keyCode) {
            switch ((Keys)keyCode)
            {
                case Keys.Left:
                    mIsLeft = true;
                    break;
                case Keys.Right:
                    mIsRight = true;
                    break;
                case Keys.Up:
                    mIsUp = true;
                    break;
                case Keys.Space:
                    if (!mIsJumping && !mHitWithBarrier) {
                        mCanJump = true;
                    }
                    break;
            }
        }

        public void OnKeyUp(int keyCode) {
            switch ((Keys)keyCode)
            {
                case Keys.Left:
                    mIsLeft = false;
                    break;
                case Keys.Right:
                    mIsRight = false;
                    break;
                case Keys.Up:
                    mIsUp = false;
                    break;
            }
        }

        public void RemoveHeart()
        {
            mLifes--;

            mEngine.GameHud.SetHeartQuantity(mLifes);
        }

        public void HitBarrier()
        {
            mCurrentSpeed = -mTopSpeed;
            mHitWithBarrier = true;
        }


        public void Update(int dt)
        {
            mEngine.RenderCamera.Position = new Vector3(this.X, this.Y + 20, this.Z + 260);
            //mEngine.RenderCamera.UpDownRotation = -0.25f;
          //  mEngine.RenderCamera.UpdateViewMatrix();
        }

        public void Draw(int dt) {
            if (mIsUp) {
                mCurrentSpeed += mAcceleration;
                mCurrentSpeed = mCurrentSpeed >= mTopSpeed ? mTopSpeed : mCurrentSpeed;
            }
            else {
                if (!mHitWithBarrier) {
                    mCurrentSpeed -= mAcceleration;
                    mCurrentSpeed = mCurrentSpeed <= 0 ? 0 : mCurrentSpeed;
                }
            }

            if (mHitWithBarrier) {
                mCurrentSpeed += mAcceleration;
                mCurrentSpeed = mCurrentSpeed >= 0 ? 0 : mCurrentSpeed;

                if (mCurrentSpeed == 0)
                {
                    mHitWithBarrier = false;
                }
            }

            mZ -= mCurrentSpeed * (float)dt;

            if (mIsRight && !mHitWithBarrier) {
                mX += (mCurrentSpeed * mSteeringFactor) * (float)dt;
                mX = mX > 25 ? 25 : mX;
            }

            if (mIsLeft && !mHitWithBarrier) {
                mX -= (mCurrentSpeed * mSteeringFactor) * (float)dt;
                mX = mX < -30 ? -30 : mX;
            }

            if (mCanJump) {
                mCanJump = false;
                mIsJumping = true;

                mJumpingFactor = mJumpSpeed;
            }

            if (mIsJumping) {
                mY += mJumpingFactor;
                mJumpingFactor -= mGravity;

                mShaderProgram.RotationZ += 0.6f;

                if (mY < mInitY)
                {
                    mY = mInitY;
                    mIsJumping = false;
                    mShaderProgram.RotationZ = 0f;
                    mShaderProgram.ScaleX = mShaderProgram.ScaleY = mShaderProgram.ScaleZ = 1f;
                }
            }

            mShaderProgram.X = mX;
            mShaderProgram.Y = mY - 20;
            mShaderProgram.Z = mZ + 250;

            mModel3DBetter.Draw(mEngine.RenderCamera.transformed, dt);

            NativeApplication.instance.FillMode = SharpDX.Direct3D11.FillMode.Wireframe;

            mDebugBox.X = mX;
            mDebugBox.Y = mY;
            mDebugBox.Z = mZ + 180;
            //mDebugBox.UpdateDraw(dt);

            NativeApplication.instance.FillMode = SharpDX.Direct3D11.FillMode.Solid;
        }
    }
}
