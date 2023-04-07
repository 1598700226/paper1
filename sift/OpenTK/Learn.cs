using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sift.OpenTK
{
    public class Learn : GameWindow
    {

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;
        float[] vertices =
        {
            //Position          Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly float[] verticesColor =
        {
          // positions        // colors
          0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
         -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
          0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f    // top 
        };
        Shader shader;

        uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        Stopwatch timeWatch;

        public Learn(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {
            timeWatch = Stopwatch.StartNew();
            timeWatch.Start();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Key.Escape)) {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.DeleteBuffer(VertexBufferObject);
                Exit();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            shader = new Shader("D:\\csharp code\\sift\\sift\\OpenTK\\shaders\\shader.vert", "D:\\csharp code\\sift\\sift\\OpenTK\\shaders\\shader.frag"); 
            shader.Use();

            GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
  

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            /*
                        ElementBufferObject = GL.GenBuffer();
                        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            */

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Use();

/*            double time = timeWatch.Elapsed.TotalSeconds;
            float greenValue = ((float)Math.Sin(time) / 2.0f) + 0.5f;
            int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);*/
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            shader.Dispose();
        }

        public static void test () { 
            
        }

    }
}
