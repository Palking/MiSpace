using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSpace
{
    class Triangle
    {
        private VertexBuffer triangleBuffer;
        private GraphicsDevice device;

        private Vector3 v1, v2, v3;

        public Triangle(GraphicsDevice device, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;

            this.device = device;

            BuildTriangleBuffer();
        }

        private void BuildTriangleBuffer()
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();
            vertexList.Add(new VertexPositionColor(v1, Color.Green));
            vertexList.Add(new VertexPositionColor(v2, Color.Green));
            vertexList.Add(new VertexPositionColor(v3, Color.Green));

            triangleBuffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, vertexList.Count, BufferUsage.None);
            triangleBuffer.SetData(vertexList.ToArray());
        }

        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;

            // Loop through and draw each vertex
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(triangleBuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, triangleBuffer.VertexCount);
            }
        }

        public void Draw(Camera camera, BasicEffect effect, VertexBuffer buffer)
        {
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;

            // Loop through and draw each vertex
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(buffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, buffer.VertexCount);
            }
        }
    }
}
