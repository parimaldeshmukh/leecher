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
        const int totalLives = 3;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = true;
            level = new LevelOne(totalLives, new GameTime());
            levels = new List<Level>();
            levels.Add(level);
            levels.Add(new LevelTwo(totalLives, new GameTime()));
            levels.Add(new LevelThree(totalLives, new GameTime()));

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
            Tuple<LevelState,int> tuple = level.Update(gameTime);
            LevelState levelState = tuple.Item1;
            if (levelState == LevelState.Exited) base.Exit();
            else if (levelState == LevelState.Completed)
            {
                int index = (levels.FindIndex(delegate(Level current) { return level == current; }) + 1)%3;
                level = levels.ElementAt(index);
                level.init(tuple.Item2, gameTime);
            }
            else if (levelState == LevelState.NoLivesLeft) {
                level = levels.ElementAt(0);
                level.init(totalLives, gameTime);
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
