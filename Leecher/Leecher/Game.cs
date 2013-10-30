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
        Level level;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;
            level = new LevelOne();
        }

        protected override void Initialize()
        {
            level.Initialise();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            level.LoadContent(GraphicsDevice, graphics, Content);
        }

        protected override void UnloadContent()
        {
            level.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!level.Update(gameTime)) base.Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            level.Draw(GraphicsDevice);
            base.Draw(gameTime);
        }


    }
}
