using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Leecher
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, character, theCreator, openComment, closeComment, brick;
        int screenHeight, screenWidth;
        Player player;
        

        List<GameObject> collidableObjects;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;
            collidableObjects = new List<GameObject>();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;

            background = Content.Load<Texture2D>(@"background");
            character = Content.Load<Texture2D>(@"test_subject_no_2");
            theCreator = Content.Load<Texture2D>(@"theCreator");
            openComment = Content.Load<Texture2D>(@"open_comment");
            closeComment = Content.Load<Texture2D>(@"close_comment");
            brick = Content.Load<Texture2D>(@"brick");

            player = new Player(character, 10, screenHeight - 130);

            collidableObjects.Add(new CollidableObject(Content.Load<Texture2D>(@"exit"), screenWidth - 90, screenHeight - 80, 50, 60));
            collidableObjects.Add(new CollidableObject(Content.Load<Texture2D>(@"tree"), screenWidth - 320, screenHeight - 400, 200, 380));
            collidableObjects.Add(new CollidableObject(Content.Load<Texture2D>(@"branch"), screenWidth - 370, screenHeight - 320, 50, 60));
            collidableObjects.Add(new ClimbableObject(Content.Load<Texture2D>(@"ladder"), screenWidth / 2, screenHeight - 600, 40, 200));
            collidableObjects.Add(new CollidableObject(Content.Load<Texture2D>(@"bird"), screenWidth - 170, screenHeight - 440, 40, 40));


           
            collidableObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 20));
            collidableObjects.Add(new Ledge(brick, 320, 480, screenHeight - 150));
            collidableObjects.Add(new Ledge(brick, screenWidth / 2, screenWidth / 2 + 160, screenHeight - 250));
            collidableObjects.Add(new Ledge(brick, screenWidth / 2 - 80, screenWidth / 2 + 80, screenHeight - 400));
            collidableObjects.Add(new Ledge(brick, screenWidth / 2 - 120, screenWidth / 2 - 40, screenHeight - 600));

            PhysicsEngine.objects = collidableObjects;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(Keyboard.GetState(), gameTime, collidableObjects);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            Rectangle backgroundContainer = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundContainer, Color.White);
            spriteBatch.Draw(theCreator, new Rectangle(30, 10, 120, 120), Color.White);
            spriteBatch.Draw(openComment, new Rectangle(300, 100, 40, 40), Color.White);
            spriteBatch.Draw(closeComment, new Rectangle(screenWidth - 300,screenHeight -70, 40, 40), Color.White);

            

            DrawStatics();
            player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawStatics()
        {
            collidableObjects.ForEach(
                delegate(GameObject gameObject) {
                gameObject.Draw(spriteBatch);
            });
        }
    }
}
