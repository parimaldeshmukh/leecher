using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Leecher
{
    class LevelThree : Level
    {
        SpriteBatch spriteBatch;
        List<GameObject> gameObjects;
        int screenHeight, screenWidth;
        Player player;
        Texture2D background, brick, character;
        SoundEffect jump;

        public LevelThree()
        {
            gameObjects = new List<GameObject>();
        }

        public void Initialise()
        {
        }

        public void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            character = content.Load<Texture2D>(@"sprite_sheet_arms");
            jump = content.Load<SoundEffect>(@"jump");
            player = new Player(character, 10, screenHeight - 310, jump);
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;

            spriteBatch = new SpriteBatch(graphicsDevice);
            brick = content.Load<Texture2D>(@"brick");
            background = content.Load<Texture2D>(@"background");


            gameObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 20));
            gameObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 40));
            gameObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 60));
            gameObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 80));
            gameObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 100));
            gameObjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 120));
            gameObjects.Add(new Ledge(brick, 0, screenWidth/2 - 120, screenHeight - 140));
            gameObjects.Add(new Ledge(brick, screenWidth/2 + 120, screenWidth, screenHeight - 140));
            gameObjects.Add(new Ledge(brick, 0, screenWidth / 2 - 120, screenHeight - 160));
            gameObjects.Add(new Ledge(brick, screenWidth / 2 + 120, screenWidth, screenHeight - 160));
            gameObjects.Add(new Ledge(brick, 0, screenWidth/2 - 280, screenHeight - 180));
            gameObjects.Add(new Ledge(brick, screenWidth/2 + 280, screenWidth, screenHeight - 180));
            gameObjects.Add(new Ledge(brick, 0, screenWidth / 2 - 280, screenHeight - 200));
            gameObjects.Add(new Ledge(brick, screenWidth / 2 + 280, screenWidth, screenHeight - 200));

            gameObjects.Add(new Ledge(brick, screenWidth /2 - 280, screenWidth/2 + 280, screenHeight - 320));
        }

        public LevelState Update(GameTime gameTime)
        {
            PhysicsEngine.objects = gameObjects;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return LevelState.Exited;

            player.Update(Keyboard.GetState(), gameTime, gameObjects);
            return LevelState.InProgress;
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            gameObjects.ForEach(x => x.Draw(spriteBatch));
            player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void UnloadContent()
        {
        }

        public void init()
        {
            PhysicsEngine.objects = gameObjects;
        }
    }
}
