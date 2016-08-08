using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX;

using Device = SharpDX.Direct3D11.Device;
using D3DBuffer = SharpDX.Direct3D11.Buffer;
using D3DMapFlags = SharpDX.Direct3D11.MapFlags;
using System.Runtime.InteropServices;
using SharpDX.Direct3D;
using Sesion2_Lab01.com.isil.shader.d3d;

namespace Sesion2_Lab01 {
    public struct dtVertexColor
    {
        public float X;
        public float Y;
        public float Z;
        public float R;
        public float G;
        public float B;
        public float A;

        public dtVertexColor(float x, float y, float z, float r, float g, float b, float a)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public dtVertexColor(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.R = 1f;
            this.G = 1f;
            this.B = 1f;
            this.A = 1f;
        }
    }

    public class Shader3DPrimitiveProgram {

        public float mRotationX;
        public float mRotationY;
        public float mRotationZ;

        private Device mDevice;
        private DeviceContext mDeviceContext;

        private VertexShader mVertexShader;
        private PixelShader mPixelShader;
        private InputLayout mInputLayout;

        private D3DBuffer mMiscInputBuffer;
        private BufferDescription mMiscInputBufferDescription;

        private D3DBuffer mVertexBuffer;
        private D3DBuffer mIndexBuffer;
        private VertexBufferBinding mVertexBufferBinding;

        private Matrix mWorld;
        private Matrix mRotationXMatrix;
        private Matrix mRotationYMatrix;
        private Matrix mRotationZMatrix;

        private int mIndicesCount;

        public VertexShader VertexShader    { get { return mVertexShader; } }
        public PixelShader PixelShader      { get { return mPixelShader; } }
        public InputLayout InputLayout      { get { return mInputLayout; } }

        public float X {
            get { return mWorld.M41; }
            set { mWorld.M41 = value; }
        }

        public float Y {
            get { return mWorld.M42; }
            set { mWorld.M42 = value; }
        }

        public float Z
        {
            get { return mWorld.M43; }
            set { mWorld.M43 = value; }
        }

        public float ScaleX {
            get { return mWorld.M11; }
            set { mWorld.M11 = value; }
        }

        public float ScaleY {
            get { return mWorld.M22; }
            set { mWorld.M22 = value; }
        }

        public float RotationX {
            get { return mRotationX; }
            set {
                if (mRotationX != value) {
                    mRotationX = value;

                    mRotationXMatrix = Matrix.RotationX(mRotationX);
                }
            }
        }

        public float RotationY {
            get { return mRotationY; }
            set {
                if (mRotationY != value) {
                    mRotationY = value;

                    mRotationYMatrix = Matrix.RotationY(mRotationY);
                }
            }
        }

        public float RotationZ {
            get { return mRotationZ; }
            set {
                if (mRotationZ != value) {
                    mRotationZ = value;

                    mRotationZMatrix = Matrix.RotationZ(mRotationZ);
                }
            }
        }

        public Shader3DPrimitiveProgram(Device device)
        {
            mDevice = device;
            mDeviceContext = device.ImmediateContext;

            mWorld = mRotationXMatrix = mRotationYMatrix = mRotationZMatrix = Matrix.Identity;
        }

        public void Load(string path) {
            CompilationResult vertexShaderByteCode = ShaderBytecode.CompileFromFile(path,
                "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            mVertexShader = new VertexShader(mDevice, vertexShaderByteCode);

            // compilamos nuestro fragment shader
            CompilationResult pixelShaderByteCode = ShaderBytecode.CompileFromFile(path,
                "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            mPixelShader = new PixelShader(mDevice, pixelShaderByteCode);

            ShaderSignature shaderSignature = ShaderSignature.GetInputSignature(vertexShaderByteCode);

            // creamos el contenedor de nuestras variables de entrada
            InputElement[] inputElements = new InputElement[2];
            // segun nuestro shader es: Posicion de los vertices
            inputElements[0] = new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0);
            // segun nuestro shader es: El color de cada vertice
            inputElements[1] = new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 12, 0);

            // construimos el Input Layout, le pasamos el Shader Signature que contiene la referencia
            // de nuestro Shader, el Input Element que contiene nuestro parametros de entrada, y 
            // mediante esto crea un Objeto para poder enviarlo a la tarjeta de video para poder usarlo
            mInputLayout = new InputLayout(mDevice, shaderSignature, inputElements);

            // ahora creamos una Descripcion para almacenar la informacion de nuestro nuevo bloque
            // de parametros en nuestro Shader
            mMiscInputBufferDescription.Usage = ResourceUsage.Dynamic;
            mMiscInputBufferDescription.SizeInBytes = Utilities.SizeOf<Shader3DPrimitiveInputParameters>();
            mMiscInputBufferDescription.BindFlags = BindFlags.ConstantBuffer;
            mMiscInputBufferDescription.CpuAccessFlags = CpuAccessFlags.Write;
            mMiscInputBufferDescription.OptionFlags = 0;
            mMiscInputBufferDescription.StructureByteStride = 0;

            // creamos un Buffer para almacenar nuestro parametros sueltos
            mMiscInputBuffer = new D3DBuffer(mDevice, mMiscInputBufferDescription);
        }

        public void SetConstantInputParameter<T>(D3DBuffer inputBuffer,
            int subResource, int slot, ref T parametersStruct) {

            // Retrieve the Data Stream from the Shader, so now can we access the Memory Pointer
            // to modify the Input Parameters from the Shader
            DataStream outData = null;
            mDevice.ImmediateContext.MapSubresource(inputBuffer,
                MapMode.WriteDiscard, D3DMapFlags.None, out outData);

            // Now that we set the new values, let's say to the Memory Pointer that we
            // changed the values and update the Memory Pointer
            Marshal.StructureToPtr(parametersStruct, outData.DataPointer, true);

            // Now we re-enable the Buffer
            mDevice.ImmediateContext.UnmapSubresource(inputBuffer, subResource);
            // Now send the Buffer to the Vertex Shader
            mDevice.ImmediateContext.VertexShader.SetConstantBuffer(slot, inputBuffer);
        }

        public void Update(dtVertexColor[] vertices, ushort[] indices)
        {
            // ahora creamos nuestro Buffer para poder almacenar los Vertice de una manera
            // que la tarjeta de video pueda leer y transferir los vertices a los Shaders
            if (mVertexBuffer != null)
            {
                mVertexBuffer.Dispose();
                mVertexBuffer = null;
            }

            mVertexBuffer = D3DBuffer.Create<dtVertexColor>(mDevice, BindFlags.VertexBuffer, vertices);

            // ahora mandamos nuestro Buffer a la tarjeta de video para que lo pueda transferir 
            // al Shader
            mVertexBufferBinding.Buffer = mVertexBuffer;
            mVertexBufferBinding.Stride = sizeof(float) * 7;
            mVertexBufferBinding.Offset = 0;

            mIndicesCount = indices.Length;

            // ahora vamos a definir nuestros vertices y mandar al GPU
            if (mIndexBuffer != null)
            {
                mIndexBuffer.Dispose();
                mIndexBuffer = null;
            }

            mIndexBuffer = D3DBuffer.Create(mDevice, BindFlags.IndexBuffer, indices);
        }

        public void Draw(Matrix transformation) {
            transformation = mRotationXMatrix * mRotationYMatrix * mRotationZMatrix * mWorld * transformation;
            transformation.Transpose();

            Shader3DPrimitiveInputParameters inputParameters = Shader3DPrimitiveInputParameters.EMPTY;
            inputParameters.transformation = transformation;

            // Set the Shader Constant Parameters
            SetConstantInputParameter<Shader3DPrimitiveInputParameters>(mMiscInputBuffer,
                0, 0, ref inputParameters);

            // mandamos vertices al GPU
            mDeviceContext.InputAssembler.SetVertexBuffers(0, mVertexBufferBinding);

            // mandamos el index buffer
            mDeviceContext.InputAssembler.SetIndexBuffer(mIndexBuffer, Format.R16_UInt, 0);

            mDeviceContext.InputAssembler.InputLayout = mInputLayout;
            // definimos ahora de que manera se va a tratar la data que entra y como se dibuja
            mDeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            // aqui vamos a transferir el Vertex y Fragment Shader que ya habiamos creado
            mDeviceContext.VertexShader.Set(mVertexShader);
            mDeviceContext.PixelShader.Set(mPixelShader);

            // dibujamos nuestros vertices!
            mDeviceContext.DrawIndexed(mIndicesCount, 0, 0);
        }

        public void Free()
        {
            mDevice = null;
            mDeviceContext = null;

            if (mVertexShader != null) { mVertexShader.Dispose(); }
            mVertexShader = null;

            if (mPixelShader != null) { mPixelShader.Dispose(); }
            mPixelShader = null;

            if (mInputLayout != null) { mInputLayout.Dispose(); }
            mInputLayout = null;

            if (mMiscInputBuffer != null) { mMiscInputBuffer.Dispose(); }
            mMiscInputBuffer = null;

            if (mVertexBuffer != null) { mVertexBuffer.Dispose(); }
            mVertexBuffer = null;

            if (mIndexBuffer != null) {  mIndexBuffer.Dispose(); }
            mIndexBuffer = null;
        }
    }
}
