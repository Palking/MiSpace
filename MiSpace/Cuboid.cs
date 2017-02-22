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

        public Vector3 Position { get; private set; }
        public BoundingBox boundingBox { get; private set; }

        private GraphicsDevice device;
        List<Rectangle> vertexList;

        /// <summary>
        /// Creates a new cuboid with the given size at 0,0,0 in the world.
        /// </summary>
        /// <param name="device">GraphicsDevice used for drawing.</param>
        /// <param name="width">Width in Z direction.</param>
        /// <param name="height">Height in Y direction.</param>
        /// <param name="length">Length in X direction.</param>
        public Cuboid (GraphicsDevice device, float width, float height, float length)
        {
            this.device = device;
            this.height = height;
            this.width = width;
            this.length = length;

            this.Position = Vector3.Zero;
            vertexList = new List<Rectangle>();

            BuildCubeBuffer();
        }

        /// <summary>
        /// Creates a new cuboid with the given size at the given position.
        /// </summary>
        /// <param name="device">GraphicsDevice used for drawing.</param>
        /// <param name="position">Position of the new cuboid.</param>
        /// <param name="width">Width in Z direction.</param>
        /// <param name="height">Height in Y direction.</param>
        /// <param name="length">Length in X direction.</param>
        public Cuboid(GraphicsDevice device, Vector3 position, float width, float height, float length)
            : this(device, width, height, length)
        {
            this.Position = position;
            BuildCubeBuffer();
        }

        private void BuildCubeBuffer()
        {
            vertexList.Clear();

            Vector3 p1 = new Vector3(-width / 2, height / 2, length / 2);
            Vector3 p2 = new Vector3(-width / 2, -height / 2, length / 2);
            Vector3 p3 = new Vector3(width / 2, -height / 2, length / 2);
            Vector3 p4 = new Vector3(width / 2, height / 2, length / 2);
            Vector3 p5 = new Vector3(-width / 2, height / 2, -length / 2);
            Vector3 p6 = new Vector3(-width / 2, -height / 2, -length / 2);
            Vector3 p7 = new Vector3(width / 2, -height / 2, -length / 2);
            Vector3 p8 = new Vector3(width / 2, height / 2, -length / 2);

            //Transform points to absolute coordinates
            p1 += Position;
            p2 += Position;
            p3 += Position;
            p4 += Position;
            p5 += Position;
            p6 += Position;
            p7 += Position;
            p8 += Position;

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

        private BoundingBox UpdateBoundingBox(Matrix worldMatrix)
        {
            //Get min and max (opposite corners) of the cuboid relativ to position
            Vector3 min = new Vector3(-length / 2, height / 2, -width / 2);
            Vector3 max = new Vector3(length / 2, -height / 2, width / 2);

            //Either (doesnt need the worldMatrix passed)
            Vector3 absMin = min + Position;
            Vector3 absMax = max + Position;

            //Or
            //worldMatrix *= Matrix.CreateTranslation(Position);
            //Vector3 transformedMin = Vector3.Transform(min, worldMatrix);
            //Vector3 transformedMax = Vector3.Transform(max, worldMatrix);

            return new BoundingBox(absMin, absMax);
        }
    }
}