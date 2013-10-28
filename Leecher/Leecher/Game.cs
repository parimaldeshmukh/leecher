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
        LevelOne levelOne;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;
            levelOne = new LevelOne();
        }

        protected override void Initialize()
        {
            levelOne.Initialise();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            levelOne.LoadContent(GraphicsDevice, graphics, Content);
            
            
        }

        protected override void UnloadContent()
        {
            levelOne.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!levelOne.Update(gameTime)) base.Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            levelOne.Draw(GraphicsDevice);
            base.Draw(gameTime);
        }


    }
}
