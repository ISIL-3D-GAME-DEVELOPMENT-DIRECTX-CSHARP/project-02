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
    public class Barrier : BaseProps
    {
        private const string MODEL_PATH = "Content/game_example/barriere/barriere.obj";

        private bool mHasCollision;

        private PrimitiveCube3D mDebugBox;

        public Barrier(Engine engine, float x, float y, float z, float rotationY)
            : base(engine)
        {

            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Initialize(mEngine.PropsManager.SharedShader, mEngine.PropsManager.GetOrLoadModel(Barrier.MODEL_PATH,
                Matrix.RotationY(-1.6f) * Matrix.Scaling(2) * Matrix.Translation(6, 0 , 0)));

            this.RotationY = rotationY;
        }

        public override void Initialize(Shader3DProgram shader, NModel model)
        {
            base.Initialize(shader, model);

            mHasCollision = false;

            mDebugBox = new PrimitiveCube3D(40f, 40f, 20f, Color.Green, mEngine.RenderCamera);
            mDebugBox.X = mX;
            mDebugBox.Y = mY;
            mDebugBox.Z = mZ;
        }

        public override void Update(int dt)
        {
            base.Update(dt);

            if (!mHasCollision)
            {
                if (PrimitiveCube3D.CheckCollision(mDebugBox, mEngine.Player.CollisionBox))
                {
                    //mHasCollision = true;
                    mEngine.Player.RemoveHeart();
                    mEngine.Player.HitBarrier();
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

        public override void Free()
        {
            mDebugBox.Free();
            mDebugBox = null;

            base.Free();
        }
    }
}
