using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSpace
{
    class Rectangle
    {
        Vector3 v1, v2, v3, v4;
        List<Triangle> vertexList;

        public Rectangle(GraphicsDevice device, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            
            vertexList = new List<Triangle>();
            vertexList.Add(new Triangle(device, v1, v3, v2));
            vertexList.Add(new Triangle(device, v3, v4, v2));
        }

        public void Draw(Camera camera, BasicEffect effect)
        {
            foreach(Triangle triangle in vertexList)
            {
                triangle.Draw(camera, effect);
            }
        }
    }
}
