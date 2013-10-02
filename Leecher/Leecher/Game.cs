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
        Texture2D background, character;
        int screenHeight, screenWidth;
        Player player;
        

        List<StaticObject> objects;
        List<Ledge> ledges;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;
            objects = new List<StaticObject>();
            ledges = new List<Ledge>();
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

            player = new Player(character, 10, screenHeight - 130);

            objects.Add(new StaticObject(Content.Load<Texture2D>(@"exit"), screenWidth - 90, screenHeight - 80, 50, 60));
            objects.Add(new StaticObject(Content.Load<Texture2D>(@"tree"), screenWidth - 320, screenHeight - 400, 200, 380));
            objects.Add(new StaticObject(Content.Load<Texture2D>(@"branch"), screenWidth - 370, screenHeight - 320, 50, 60));
            objects.Add(new StaticObject(Content.Load<Texture2D>(@"ladder"), screenWidth / 2, screenHeight - 600, 40, 200));
            objects.Add(new StaticObject(Content.Load<Texture2D>(@"bird"), screenWidth - 170, screenHeight - 440, 40, 40));


            Texture2D brick = Content.Load<Texture2D>(@"brick");
            ledges.Add(new Ledge(brick, 0, screenWidth, screenHeight - 20));
            ledges.Add(new Ledge(brick, 350, 430, screenHeight - 150));
            ledges.Add(new Ledge(brick, screenWidth / 2, screenWidth / 2 + 80, screenHeight - 250));
            ledges.Add(new Ledge(brick, screenWidth / 2 - 80, screenWidth / 2 + 80, screenHeight - 400));
            ledges.Add(new Ledge(brick, screenWidth / 2 - 120, screenWidth / 2 - 40, screenHeight - 600));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(Keyboard.GetState(), gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            Rectangle backgroundContainer = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundContainer, Color.White);
            DrawStatics();

            player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawStatics()
        {

            objects.ForEach(
                delegate(StaticObject gameObject) {
                gameObject.Draw(spriteBatch);
            });

            ledges.ForEach(
                delegate(Ledge ledge)
                {
                    ledge.Draw(spriteBatch);
                });
        }
    }
}
