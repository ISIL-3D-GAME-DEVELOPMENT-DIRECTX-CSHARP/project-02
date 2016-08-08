using CrossXDK.com.digitalkancer.modules.moderlLoaders.assimp;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game
{
    public class RoadManager
    {

        public List<Road> mRoads;

        private Engine mEngine;

        private NModel mModel3DBetter;
        private Shader3DProgram mShaderProgram_Model;

        public RoadManager(Engine engine)
        {
            mEngine = engine;
            mRoads = new List<Road>();

            // cargamos shader unico
            mShaderProgram_Model = new Shader3DProgram(mEngine.Device);
            mShaderProgram_Model.Load("Content/Fx_PrimitiveTexture3D.fx");
            // cargamos modelo unico
            NCrossModelLoader cl = new NCrossModelLoader();
            Matrix initTrans = Matrix.RotationY(3.14f / 2f);
            mModel3DBetter = cl.Load("Content/game_example/Pista/Pista.obj", initTrans);
            mModel3DBetter.SetShader(mShaderProgram_Model);
        }

        public void AddRoad(float x, float y, float z)
        {
            Road road = new Road(mEngine, mShaderProgram_Model, mModel3DBetter);
            road.X = x;
            road.Y = y;
            road.Z = z;

            mRoads.Add(road);
        }

        public void Update(int dt)
        {
            for (int i = 0; i < mRoads.Count; i++)
            {
                mRoads[i].Update(dt);
            }
        }

        public void Draw(int dt)
        {
            for (int i = 0; i < mRoads.Count; i++)
            {
                mRoads[i].Draw(dt);
            }
        }

    }
}
