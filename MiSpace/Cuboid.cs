using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MiSpace
{
    class Cuboid
    {
        private float height;
        private float width;
        private float length;

        private GraphicsDevice device;
        List<Rectangle> vertexList;

        public Cuboid (GraphicsDevice device, float width, float height, float length)
        {
            this.device = device;
            this.height = height;
            this.width = width;
            this.length = length;

            vertexList = new List<Rectangle>();

            BuildCubeBuffer();
        }
    
        private void BuildCubeBuffer()
        {
            Vector3 p1 = new Vector3(-width, height, length);
            Vector3 p2 = new Vector3(-width, -height, length);
            Vector3 p3 = new Vector3(width, -height, length);
            Vector3 p4 = new Vector3(width, height, length);
            Vector3 p5 = new Vector3(-width, height, -length);
            Vector3 p6 = new Vector3(-width, -height, -length);
            Vector3 p7 = new Vector3(width, -height, -length);
            Vector3 p8 = new Vector3(width, height, -length);

            vertexList.Add(new Rectangle(device, p1, p2, p4, p3));
            vertexList.Add(new Rectangle(device, p8, p7, p5, p6));
            vertexList.Add(new Rectangle(device, p1, p5, p2, p6));
            vertexList.Add(new Rectangle(device, p3, p7, p4, p8));
            vertexList.Add(new Rectangle(device, p1, p4, p5, p8));
            vertexList.Add(new Rectangle(device, p2, p6, p3, p7));
        }

        // Draw method
        public void Draw(Camera camera, BasicEffect effect)
        {
            foreach (Rectangle rectangle in vertexList)
            {
                rectangle.Draw(camera, effect);
            }
        }
    }
}
