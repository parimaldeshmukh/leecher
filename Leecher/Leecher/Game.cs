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
        List<Level> levels;

      

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;
            level = new LevelTwo();
            levels = new List<Level>();
            levels.Add(level);
            levels.Add(new LevelTwo());

            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
        }

        protected override void Initialize()
        {
            levels.ForEach(delegate(Level currentLevel)
            {
                currentLevel.Initialise();
            });
            base.Initialize();
        }

        protected override void LoadContent()
        {
            levels.ForEach(delegate(Level currentLevel)
            {
                currentLevel.LoadContent(GraphicsDevice, graphics, Content);
            });

            
        }

        protected override void UnloadContent()
        {
            levels.ForEach(delegate(Level currentLevel)
            {
                currentLevel.UnloadContent();
            });
        }

        protected override void Update(GameTime gameTime)
        {
            LevelState levelState = level.Update(gameTime);
            if (levelState == LevelState.Exited) base.Exit();
            else if (levelState == LevelState.Completed)
            {
                int index = (levels.FindIndex(delegate(Level current) { return level == current; }) + 1)%2;
                level = levels.ElementAt(index);
                level.init();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            


            level.Draw(GraphicsDevice);
            base.Draw(gameTime);


        }


    }
}
