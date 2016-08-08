using Sesion2_Lab01.com.isil.graphics;
using Sesion2_Lab01.com.isil.shader.d2d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game
{
    public class GameHud {

        private Engine mEngine;

        private NSprite2D mSP_Heart_1;
        private NSprite2D mSP_Heart_2;
        private NSprite2D mSP_Heart_3;
        private ShaderTextureProgram mSTP;

        public GameHud() { }

        public void Initialize(Engine engine) {
            mEngine = engine;

            mSTP = new ShaderTextureProgram(mEngine.Device);
            mSTP.Load("Content/Fx_TexturePrimitive.fx");

            mSP_Heart_1 = new NSprite2D("Content/hud/spHeart.png", 0, 0);
            mSP_Heart_1.SetShader(mSTP);
            mSP_Heart_1.ScaleX = mSP_Heart_1.ScaleY = 0.3f;
            mSP_Heart_1.X = 5;
            mSP_Heart_1.Y = 5;

            mSP_Heart_2 = new NSprite2D("Content/hud/spHeart.png", 0, 0);
            mSP_Heart_2.SetShader(mSTP);
            mSP_Heart_2.ScaleX = mSP_Heart_2.ScaleY = 0.3f;
            mSP_Heart_2.X = 55;
            mSP_Heart_2.Y = 5;

            mSP_Heart_3 = new NSprite2D("Content/hud/spHeart.png", 0, 0);
            mSP_Heart_3.SetShader(mSTP);
            mSP_Heart_3.ScaleX = mSP_Heart_3.ScaleY = 0.3f;
            mSP_Heart_3.X = 105;
            mSP_Heart_3.Y = 5;
        }

        public void SetHeartQuantity(int lifes) {
            switch (lifes)
            {
            case 3:
                mSP_Heart_1.ChangeColor(1f, 1f, 1f, 1f);
                mSP_Heart_2.ChangeColor(1f, 1f, 1f, 1f);
                mSP_Heart_3.ChangeColor(1f, 1f, 1f, 1f);
                break;
            case 2:
                mSP_Heart_1.ChangeColor(1f, 1f, 1f, 1f);
                mSP_Heart_2.ChangeColor(1f, 1f, 1f, 1f);
                mSP_Heart_3.ChangeColor(0.4f, 0.4f, 0.4f, 1f);
                break;
            case 1:
                mSP_Heart_1.ChangeColor(1f, 1f, 1f, 1f);
                mSP_Heart_2.ChangeColor(0.4f, 0.4f, 0.4f, 1f);
                mSP_Heart_3.ChangeColor(0.4f, 0.4f, 0.4f, 1f);
                break;
            case 0:
                mSP_Heart_1.ChangeColor(0.4f, 0.4f, 0.4f, 1f);
                mSP_Heart_2.ChangeColor(0.4f, 0.4f, 0.4f, 1f);
                mSP_Heart_3.ChangeColor(0.4f, 0.4f, 0.4f, 1f);
                break;
            }
        }

        public void Update(int dt)
        {

        }

        public void Draw(int dt)
        {
            mEngine.RenderCamera.ChangeCameraTo(RenderCamera.ORTHOGRAPHIC);

            mSP_Heart_1.Draw(mEngine.RenderCamera, dt);
            mSP_Heart_2.Draw(mEngine.RenderCamera, dt);
            mSP_Heart_3.Draw(mEngine.RenderCamera, dt);

            mEngine.RenderCamera.ChangeCameraTo(RenderCamera.PERSPECTIVE);
        }
    }
}
