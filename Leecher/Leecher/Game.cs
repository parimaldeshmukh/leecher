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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, brick, exitSign, tree, bird, ladder;
        int screenHeight, screenWidth;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;          
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            background = Content.Load<Texture2D>(@"background");
            brick = Content.Load<Texture2D>(@"brick");
            exitSign = Content.Load<Texture2D>(@"exit");
            tree = Content.Load<Texture2D>(@"tree");
            bird = Content.Load<Texture2D>(@"bird");
            ladder = Content.Load<Texture2D>(@"ladder");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            Rectangle backgroundContainer = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Begin();
            
            spriteBatch.Draw(background, backgroundContainer, Color.White);

            for (int brickIndex = 0; brickIndex < screenWidth; brickIndex += 40)
            {
                spriteBatch.Draw(brick, new Rectangle(brickIndex, screenHeight - 20, 40, 20), Color.White);
            }


            //legde 1
            spriteBatch.Draw(brick, new Rectangle(280+150, screenHeight - 150, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(280+110, screenHeight - 150, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(280+70, screenHeight - 150, 40, 20), Color.White);


            //ledge 2
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2+80, screenHeight - 250, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2+40, screenHeight - 250, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2, screenHeight - 250, 40, 20), Color.White);

            
            //ledge 3
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2, screenHeight - 400, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2+40, screenHeight - 400, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2+80, screenHeight - 400, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2 -40, screenHeight - 400, 40, 20), Color.White);
            spriteBatch.Draw(brick, new Rectangle(screenWidth / 2 -80, screenHeight - 400, 40, 20), Color.White);
            


            spriteBatch.Draw(ladder, new Rectangle(screenWidth / 2 , screenHeight - 600, 40, 200), Color.White);
            spriteBatch.Draw(exitSign, new Rectangle(screenWidth - 90, screenHeight - 80, 50, 60), Color.White);
            spriteBatch.Draw(tree, new Rectangle(screenWidth - 370, screenHeight - 400, 240, 380), Color.White);
            spriteBatch.Draw(bird, new Rectangle(screenWidth - 170, screenHeight - 440, 40, 40), Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
