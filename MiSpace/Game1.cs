﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiSpace
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        Floor floor;
        Cuboid cuboid;
        Cuboid movedCuboid;
        Cuboid movedCuboid2;
        Cuboid acadCube;
        Triangle triangle;
        Rectangle rectangle;
        BasicEffect effect;
        Sphere sphere;

        Model acadModel;
        Texture2D acadTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera(this, new Vector3(10f, 1f, 5f), Vector3.Zero, 5f);
            Components.Add(camera);    
            floor = new Floor(GraphicsDevice, 20, 20);
            effect = new BasicEffect(GraphicsDevice);
            //cuboid = new Cuboid(GraphicsDevice, 1, 1, 1);
            movedCuboid = new Cuboid(GraphicsDevice, new Vector3(3, 2, 2), 1, 1, 1);
            movedCuboid2 = new Cuboid(GraphicsDevice, new Vector3(5, 0, 4), 1, 1, 1);
            //sphere = new Sphere(GraphicsDevice, 10);
            base.Initialize();
            acadCube = new Cuboid(GraphicsDevice, new Vector3(5, 0, 4), 1, 1, 1);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            acadModel = Content.Load<Model>("models/player");
            acadTexture = Content.Load<Texture2D>("textures/tarmac_0");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            floor.Draw(camera, effect);
            //cuboid.Draw(camera, effect);
            //movedCuboid.Draw(camera, effect);
            //movedCuboid2.Draw(camera, effect);
            acadCube.DrawModel(acadModel, acadTexture, camera);
            //sphere.Draw(camera, effect);
            base.Draw(gameTime);   
        }
    }
}