using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSpace
{
    class Camera
    {
        // This attribute represents the camera position
        Vector3 camPosition;
        //This attribute represents the camera target
        Vector3 camTarget;
        Matrix viewMatrix;
        Matrix worldMatrix;
        Matrix projectionMatrix;

        public void Initialize(Game1 game)
        {
            camTarget = new Vector3(0f, 0f, 0f);
            camPosition = new Vector3(0f, 20f, -10f);


            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                                    MathHelper.ToRadians(45f),
                                    game.GraphicsDevice.DisplayMode.AspectRatio, 1f, 1000f);

            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);
        }
    }
}
