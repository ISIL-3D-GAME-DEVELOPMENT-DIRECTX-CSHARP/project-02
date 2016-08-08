using CrossXDK.com.digitalkancer.modules.moderlLoaders.assimp;
using Sesion2_Lab01.com.isil.graphics;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game.custom_props
{
    public class PlayerCoin : BaseProps
    {
        private const string MODEL_PATH = "Content/otros/coin/coin.obj";

        private float mFactorCounter;
        private float mSavedY;
        private bool mHasCollision;

        private PrimitiveCube3D mDebugBox;

        public PlayerCoin(Engine engine, float x, float y, float z) : base(engine) {

            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Initialize(mEngine.PropsManager.SharedShader, mEngine.PropsManager.GetOrLoadModel(PlayerCoin.MODEL_PATH, 
                Matrix.RotationX(-1.6f) * Matrix.Scaling(6)));   
        }

        public override void Initialize(Shader3DProgram shader, NModel model) {
            base.Initialize(shader, model);

            mFactorCounter = 0f;
            mHasCollision = false;

            mDebugBox = new PrimitiveCube3D(15f, 15f, 4f, Color.Green, mEngine.RenderCamera);
            mDebugBox.X = mX;
            mDebugBox.Y = mY;
            mDebugBox.Z = mZ;
        }

        public override void Update(int dt) {
            base.Update(dt);

            if (!mHasCollision) {
                mFactorCounter += 0.01f * (float)dt;

                float pos_y = (float)Math.Sin(mFactorCounter);

                mY = pos_y * 3f;

                if (PrimitiveCube3D.CheckCollision(mDebugBox, mEngine.Player.CollisionBox)) {
                    mHasCollision = true;
                    mSavedY = mY;
                }
            }
            else {
                mY += 4f;
                mZ -= mEngine.Player.CurrentSpeed * dt;
                mTintColor.W -= 0.1f;
                this.RotationY += 0.5f;
                this.ScaleX = this.ScaleY = this.ScaleZ -= 0.08f;

                if (mY > (mSavedY + 90)) {
                    this.IsWaitingForDelete = true;

                   // mEngine.Player.RemoveHeart();
                }
            }
        }

        public override void Draw(int dt)
        {
            mDebugBox.X = mX;
            mDebugBox.Y = mY;
            mDebugBox.Z = mZ;

            if (mEngine.RenderCamera.Frustum.CheckCube(mDebugBox))
            {
                NativeApplication.instance.FillMode = SharpDX.Direct3D11.FillMode.Wireframe;

                //mDebugBox.UpdateDraw(dt);

                NativeApplication.instance.FillMode = SharpDX.Direct3D11.FillMode.Solid;

                base.Draw(dt);
            } 
        }

        public override void Free() {
            mDebugBox.Free();
            mDebugBox = null;

            base.Free();
        }
    }
}
