using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSpace
{
    class Camera : GameComponent
    {
        // Attributes
        private const float NearPlaneDistance = 0.05f;
        private const float FarPlaceDistance = 1000.0f;

        private Vector3 cameraPosition;
        private Vector3 cameraRotation;
        private Vector3 cameraLookAt;

        private float cameraSpeed;

        // Properties
        public Vector3 Position
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                UpdateLookAt();
            }
        }

        public Vector3 Rotation
        {
            get { return cameraRotation; }
            set
            {
                cameraRotation = value;
                UpdateLookAt();
            }
        }

        public Matrix Projection
        {
            get;
            protected set;
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(cameraPosition, cameraLookAt, Vector3.Up);
            }
        }

        public Camera (Game game, Vector3 position, Vector3 rotation, float speed)
            : base(game)
        {
            cameraSpeed = speed;

            // Setup projection matrix
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Game.GraphicsDevice.Viewport.AspectRatio,
                NearPlaneDistance,
                FarPlaceDistance
                );

            // Set camera position and rotation
            MoveTo(position, rotation);
        }

        // Set camera'S position and rotation
        private void MoveTo(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        // Update the "Look At Vector"
        private void UpdateLookAt()
        {
            // Build a rotation matrix
            Matrix rotationMatrix = Matrix.CreateRotationX(cameraRotation.X) * Matrix.CreateRotationY(cameraRotation.Y);
            // Build look at offset vector
            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
            // Update our camer's look at vector
            cameraLookAt = cameraPosition + lookAtOffset;
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
