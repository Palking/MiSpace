using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private Vector3 mouseRotationBuffer;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

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

            previousMouseState = Mouse.GetState();
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

        // Method that simulates movement
        private Vector3 PreviewMove(Vector3 amount)
        {
            // create a rotate matrix
            Matrix rotate = Matrix.CreateRotationY(cameraRotation.Y);

            // create a movement vector
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);

            return cameraPosition + movement;
        }

        // Method that actually moves the camera
        private void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), Rotation);
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // handle keyboard movement
            KeyboardState ks = Keyboard.GetState();
            Vector3 moveVector = Vector3.Zero;
            float moveValue = 1f;

            if (ks.IsKeyDown(Keys.Q))
            {
                moveVector.Y = moveValue;
            }

            if (ks.IsKeyDown(Keys.E))
            {
                moveVector.Y = -moveValue;
            }

            if (ks.IsKeyDown(Keys.W))
            {
                moveVector.Z = moveValue;
            }

            if (ks.IsKeyDown(Keys.S))
            {
                moveVector.Z = -moveValue;
            }

            if (ks.IsKeyDown(Keys.A))
            {
                moveVector.X = moveValue;
            }

            if (ks.IsKeyDown(Keys.D))
            {
                moveVector.X = -moveValue;
            }

            if (moveVector != Vector3.Zero)
            {
                // normalize that vector
                // so that we dont move faster diagonally
                moveVector.Normalize();

                moveVector *= dt * cameraSpeed;

                Move(moveVector);
            }

            // handle mouse movement
            currentMouseState = Mouse.GetState();

            float deltaX;
            float deltaY;
            float mouseSpeed = 0.1f;
            float maxYAngle = 75.0f;

            if (currentMouseState != previousMouseState)
            {
                // Cache mouse location
                deltaX = currentMouseState.X - (Game.GraphicsDevice.Viewport.Width / 2);
                deltaY = currentMouseState.Y - (Game.GraphicsDevice.Viewport.Height / 2);

                mouseRotationBuffer.X -= mouseSpeed * deltaX * dt;
                mouseRotationBuffer.Y -= mouseSpeed * deltaY * dt;

                if (mouseRotationBuffer.Y < MathHelper.ToRadians(-maxYAngle))
                {
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(-maxYAngle));
                }

                if(mouseRotationBuffer.Y > MathHelper.ToRadians(maxYAngle))
                {
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(maxYAngle));
                }

                // z coordinate have to be 0
                Rotation = new Vector3(-MathHelper.Clamp(mouseRotationBuffer.Y, MathHelper.ToRadians(-maxYAngle), MathHelper.ToRadians(maxYAngle)),
                    MathHelper.WrapAngle(mouseRotationBuffer.X), 0);

                // reset delta values
                deltaX = 0;
                deltaY = 0;
            }


            //BUG:
            //      - Throws Exception when game is closed: System.NullReferenceException
            //          only when closed by 'X' or ESC, not alt + f4
            Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);

            previousMouseState = currentMouseState;

            base.Update(gameTime);
        }
    }
}