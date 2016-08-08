using CrossXDK.com.digitalkancer.modules.moderlLoaders.assimp;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesion2_Lab01.com.game
{
    public struct dtPropModelShaderContainer {
        public NModel Model;
    }

    public class PropsManager
    {

        public Dictionary<string, dtPropModelShaderContainer> mDataContainer;

        public List<BaseProps> mProps;

        private Engine mEngine;
        private Shader3DProgram mShader;

        public Shader3DProgram SharedShader { get { return mShader; } }

        public PropsManager(Engine engine)
        {
            mEngine = engine;
            mProps = new List<BaseProps>();
            mDataContainer = new Dictionary<string, dtPropModelShaderContainer>();

            mShader = new Shader3DProgram(mEngine.Device);
            mShader.Load("Content/Fx_PrimitiveTexture3D.fx");
        }

        public BaseProps AddProp(string path, float x, float y, float z, Matrix init_transformation) {
            BaseProps prop = new BaseProps(mEngine);
            prop.Initialize(mShader, this.GetOrLoadModel(path, init_transformation));
            prop.X = x;
            prop.Y = y;
            prop.Z = z;

            mProps.Add(prop);

            return prop;
        }

        public void AddCustomProp(BaseProps prop_to_register)
        {
            mProps.Add(prop_to_register);
        }

        public NModel GetOrLoadModel(string path, Matrix init_transformation) {
            // cargouna sola vez y lo reuso
            if (!mDataContainer.ContainsKey(path))
            {
                dtPropModelShaderContainer container;

                NCrossModelLoader cl = new NCrossModelLoader();
                container.Model = cl.Load(path, init_transformation);
                container.Model.SetShader(mShader);

                mDataContainer[path] = container;
            }

            dtPropModelShaderContainer container_model = mDataContainer[path];

            return container_model.Model;
        }

        public void Update(int dt)
        {
            for (int i = 0; i < mProps.Count; i++)
            {
                BaseProps prop = mProps[i];

                if (prop != null) {
                    if (prop.IsWaitingForDelete) {
                        prop.Free();
                        prop = null;

                        mProps.RemoveAt(i);
                        i--;
                    }
                    else {
                        prop.Update(dt);
                    }
                }
            }
        }

        public void Draw(int dt)
        {
            for (int i = 0; i < mProps.Count; i++)
            {
                mProps[i].Draw(dt);
            }
        }

    }
}
