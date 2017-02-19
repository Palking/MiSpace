using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiSpace
{
    class Sphere
    {
        private VertexBuffer triangleBuffer;
        private GraphicsDevice device;
        private IndexBuffer indexBuffer;
        private float radius;

        public Sphere(GraphicsDevice device, float radius)
        {
            this.radius = radius;
            this.device = device;
            BuildSphereBuffer();
        }

        private void BuildSphereBuffer()
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();

            int latitudeBands = 10;
            int longituteBands = 10;
            int counter = 0;

            for (int i = 0; i <= latitudeBands; i++)
            {
                double theta = i * Math.PI / latitudeBands;
                double sinTheta = Math.Sin(theta);
                double cosTheta = Math.Cos(theta);
                
                for (int j = 0; j <= longituteBands; j++)
                {
                    double phi = j * 2 * Math.PI / longituteBands;
                    double x = sinTheta * Math.Sin(phi);
                    double y = cosTheta;
                    double z = sinTheta * Math.Cos(phi);
                    // 0 1 2  3 4 5  6 7 8
                    // a a a  b b b  a a a   
                    // -      -      -
                    // j / 3) round down) % 2
                    // - - -           
                    if (Math.Floor((decimal) (j / 4)) % 2 == 0)
                        vertexList.Add(new VertexPositionColor(new Vector3((float)(radius * x), (float)(radius * y), (float)(radius * z)), Color.Green));
                    else
                        vertexList.Add(new VertexPositionColor(new Vector3((float)(radius * z), (float)(radius * x), (float)(radius * y)), Color.Red));

                } 
            }
            
            triangleBuffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, vertexList.Count, BufferUsage.None);
            triangleBuffer.SetData(vertexList.ToArray());

            List<short> indexList = new List<short>();
            for (int i = 0; i < latitudeBands; i++)
            {
                for (int j = 0; j < longituteBands; j++)
                {
                    int idx0 = (i * (longituteBands + 1)) + j;
                    int idx1 = idx0 + longituteBands + 1;
                    int idx2 = idx0 + 1;
                    int idx3 = idx1 + 1;
 
                    indexList.Add((short)idx0);
                    indexList.Add((short)idx1);
                    indexList.Add((short)idx2);
                    indexList.Add((short)idx3);
                }
            }

            indexBuffer = new IndexBuffer(device, typeof(short), indexList.Count, BufferUsage.WriteOnly);
            indexBuffer.SetData(indexList.ToArray());
        }

        public void Draw(Camera camera, BasicEffect effect)
        {
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;
            device.SetVertexBuffer(triangleBuffer);
            device.Indices = indexBuffer;
            // Loop through and draw each vertex
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

//                device.DrawIndexedPrimitives(PrimitiveType.TriangleList,0, 0, triangleBuffer.VertexCount, 0, 100*100);
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, (int) (triangleBuffer.VertexCount * 1.5f));
            }
        }
    }
}
