using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    class LevelOne : Level
    {
        List<GameObject> gameobjects;
        Texture2D background, character, theCreator, openComment, closeComment, brick;
        SpriteBatch spriteBatch;
        int screenHeight, screenWidth;
        Player player;

        public LevelOne() {
            gameobjects = new List<GameObject>();
        }

        public void Initialise()
        { 
        }

        public void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;

            background = content.Load<Texture2D>(@"background");
            character = content.Load<Texture2D>(@"test_subject_no_2");
            theCreator = content.Load<Texture2D>(@"theCreator");
            openComment = content.Load<Texture2D>(@"open_comment");
            closeComment = content.Load<Texture2D>(@"close_comment");
            brick = content.Load<Texture2D>(@"brick");

            player = new Player(character, 10, screenHeight - 130);

            gameobjects.Add(new ExitObject(content.Load<Texture2D>(@"exit"), screenWidth - 90, screenHeight - 80, 50, 60));
            gameobjects.Add(new CollidableObject(content.Load<Texture2D>(@"tree"), screenWidth - 320, screenHeight - 400, 200, 380, 200, 100));
            gameobjects.Add(new CollidableObject(content.Load<Texture2D>(@"branch"), screenWidth - 370, screenHeight - 320, 50, 60));
            gameobjects.Add(new CollidableObject(content.Load<Texture2D>(@"bird"), screenWidth - 170, screenHeight - 440, 40, 40));
            gameobjects.Add(new CollectibleObject(openComment, 300, 100, 40, 40));
            gameobjects.Add(new CollectibleObject(closeComment, screenWidth - 300, screenHeight - 100, 40, 40));

            gameobjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 20));
            gameobjects.Add(new Ledge(brick, 320, 480, screenHeight - 150));
            gameobjects.Add(new Ledge(brick, screenWidth / 2, screenWidth / 2 + 160, screenHeight - 250));
            gameobjects.Add(new Ledge(brick, screenWidth / 2 - 80, screenWidth / 2 + 80, screenHeight - 400));
            gameobjects.Add(new Ledge(brick, screenWidth / 2 - 180, screenWidth / 2 - 100, screenHeight - 500));

            
        }

        public LevelState Update(GameTime gameTime)
        {
            PhysicsEngine.objects = gameobjects;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return LevelState.Exited;

            player.Update(Keyboard.GetState(), gameTime, gameobjects);

            if (gameobjects.Exists(delegate(GameObject gameObject)
            {
                return gameObject.GetType() == typeof(ExitObject);
            })) return LevelState.InProgress;

            return LevelState.Completed;
        }

        public void UnloadContent()
        {
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);


            Rectangle backgroundContainer = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundContainer, Color.White);
            spriteBatch.Draw(theCreator, new Rectangle(30, 10, 120, 120), Color.White);

            DrawStatics();
            player.Draw(spriteBatch);

            spriteBatch.End();
            
        }

        private void DrawStatics()
        {
            gameobjects.ForEach(
                delegate(GameObject gameObject)
                {
                    gameObject.Draw(spriteBatch);
                });
        }

    }
}
