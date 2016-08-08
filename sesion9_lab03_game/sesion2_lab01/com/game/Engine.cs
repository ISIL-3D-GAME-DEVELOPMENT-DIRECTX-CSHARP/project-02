
using Sesion2_Lab01.com.game.custom_props;
using Sesion2_Lab01.com.isil.graphics;
using Sesion2_Lab01.com.isil.utils;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game
{
    public class Engine
    {

        private Player mPlayer;
        private RoadManager mRoad;
        private PropsManager mProps;

        private RenderCamera mCamera;
        private Device mDevice;

        private GameHud mGameHud;
        private NSkyBox3D mSkyBox;
        private NPlaneGraphic3D mFloor;

        private NKeyboardHandler mKeyboard;

        public Device Device                { get { return mDevice; } }
        public RenderCamera RenderCamera    { get { return mCamera; } }
        public PropsManager PropsManager    { get { return mProps; } }
        public Player Player                { get { return mPlayer; } }
        public GameHud GameHud              { get { return mGameHud; } }

        public Engine(Device device, RenderCamera camera)
        {
            mDevice = device;
            mCamera = camera;  
        }

        public void Initialize(GameHud hud)
        {
            mGameHud = hud;

            mPlayer = new Player(this);

            mRoad = new RoadManager(this);

            for (int i = 0; i < 1000; i++)
            {
                mRoad.AddRoad(0, -65f, i * -300);
            }

            mProps = new PropsManager(this);
            mProps.AddProp("Content/otros/castle/castle.obj", 300, -100, -1000, Matrix.Scaling(5f));
            //mProps.AddProp("Content/otros/mountain/mount.obj", 1400, -500, -4000, Matrix.Scaling(500f)).Size = 2000f;
            mProps.AddProp("Content/otros/farmhouse/Farmhouse OBJ.obj", -250, -120, -4000, Matrix.RotationY(-3.14f) * Matrix.Scaling(10)).Size = 200;

            Random rnd = new Random();
            
            for (int i = 0; i < 100; i++)
            {
                mProps.AddProp("Content/otros/palm/MY_PALM.obj", 
                    -64 - rnd.Next(20), 
                    -130 - rnd.Next(40), 
                    -200 * i,
                    Matrix.RotationY(-3.14f - ((float)rnd.NextDouble() * 1f)) * Matrix.Scaling(0.22f));
            }

            for (int i = 0; i < 10; i++)
            {
                mProps.AddCustomProp(new PlayerCoin(this, -25, -20, -200 * i));
            }

            for (int i = 0; i < 10; i++)
            {
                mProps.AddCustomProp(new PlayerCoin(this, 25, -20, -4000 + (-200 * i)));
            }


            mProps.AddCustomProp(new Barrier(this, -13, -38, -500, -0.4f));
            mProps.AddCustomProp(new Barrier(this, -14, -38, -1200, -0.3f));
            mProps.AddCustomProp(new Barrier(this, 14, -38, -1300, 0.3f));
            mProps.AddCustomProp(new Barrier(this, -13, -38, -1900, 0f));


            mProps.AddCustomProp(new Barrier(this, 13, -38, -2600, 0.4f));
            mProps.AddCustomProp(new Barrier(this, 13, -38, -3500, 0f));
            mProps.AddCustomProp(new Barrier(this, 13, -38, -4200, 0.8f));
            mProps.AddCustomProp(new Barrier(this, -13, -38, -4300, 0.8f));

            mKeyboard = new NKeyboardHandler(NativeApplication.instance.RenderForm, this.OnKeyDown, this.OnKeyUp);

            mSkyBox = new NSkyBox3D("Content/skybox/spSkybox.png", 4, 3, 50000);
            mSkyBox.X = 0;
            mSkyBox.Y = 0;
            mSkyBox.Z = 0;

            mFloor = new NPlaneGraphic3D("Content/textures/spGrass_tile.png", 
        -50000, -120, -50000, 18000);
            mFloor.RotationX = (float)Math.PI / 2f;
            mFloor.ScaleX = mFloor.ScaleY = mFloor.ScaleZ = 100000f;
        }

        private void OnKeyDown(int keycode) {
            mPlayer.OnKeyDown(keycode);
        }

        private void OnKeyUp(int keycode)
        {
            mPlayer.OnKeyUp(keycode);
        }

        public void Update(int dt)
        {
            mPlayer.Update(dt);
            mRoad.Update(dt);
            mProps.Update(dt);
        }

        public void Draw(int dt)
        {
            mPlayer.Draw(dt);
            mRoad.Draw(dt);
            mProps.Draw(dt);

            mFloor.Draw(mCamera, dt);
            mSkyBox.UpdateDraw(mCamera, dt);
        }
    }
}
