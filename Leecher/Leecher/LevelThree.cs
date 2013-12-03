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
        int screenHeight, screenWidth, livesLeft, updateCount;
        Player player;
        Texture2D background, brick, character, life, bugZilla, scene, creator, devDialog, playerDialog;
        SoundEffect jump;
        MonsterObject bug, dragon;
        ExitObject exit;
        List<Texture2D> story;
        bool isStartup, knowsPowerup = false;
        double initAt;
        CollectibleObject helmet;

        public LevelThree(int livesLeft, GameTime gameTime)
        {
            gameObjects = new List<GameObject>();
            this.livesLeft = livesLeft;
            story = new List<Texture2D>();
            isStartup = true;
            initAt = gameTime.TotalGameTime.TotalSeconds;
        }

        public void Initialise()
        {
        }

        public void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;

            life = content.Load<Texture2D>(@"life");

            character = content.Load<Texture2D>(@"sprite_sheet_arms");
            jump = content.Load<SoundEffect>(@"jump");
            player = new Player(character, 10, 410, jump);

            devDialog = content.Load<Texture2D>(@"dialog_43");
            playerDialog = content.Load<Texture2D>(@"dialog_41");

            creator = content.Load<Texture2D>(@"theCreator");

            dragon = new MonsterObject(content.Load<Texture2D>(@"dragon"), screenWidth / 2 - 250, screenHeight - 580, 550, 250, 0);
            dragon.setFrameSize(539, 389);
            dragon.setCollisionBox(screenWidth / 2 - 120, screenHeight - 580, 240, 250);

            spriteBatch = new SpriteBatch(graphicsDevice);
            brick = content.Load<Texture2D>(@"brick");
            background = content.Load<Texture2D>(@"background");

            bugZilla = content.Load<Texture2D>(@"bug");
            bug = new MonsterObject(bugZilla, screenWidth / 2, screenHeight - 220, 70, 100);

            exit = new ExitObject(content.Load<Texture2D>(@"exit"), screenWidth - 90, screenHeight - 260, 50, 60);

            helmet = new CollectibleObject(content.Load<Texture2D>(@"helmet"), 250, 300, 70, 70);

            gameObjects.Add(helmet);
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
            gameObjects.Add(new Ledge(brick, screenWidth /2 - 280, screenWidth/2 + 280, screenHeight - 330));

            gameObjects.Add(exit);

            LoadStoryBoards(content);
        }

        private void LoadStoryBoards(ContentManager content)
        {
            story.Add(content.Load<Texture2D>(@"storyboard-4/scene_41"));
            story.Add(content.Load<Texture2D>(@"storyboard-4/scene_42"));

            scene = story.ElementAt(0);
        }

        public Tuple<LevelState,int> Update(GameTime gameTime)
        {

            if (isStartup)
            {
                //if (Keyboard.GetState().GetPressedKeys().Length != 0)
                    //isStartup = false;
                UpdateStory(gameTime);
                return Tuple.Create(LevelState.InProgress, livesLeft);
            }

            PhysicsEngine.objects = gameObjects;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return Tuple.Create(LevelState.Exited, livesLeft);

            if (!knowsPowerup && !gameObjects.Exists(x => x.GetType() == typeof(CollectibleObject)))
            {
                knowsPowerup = true;
                updateCount = 50;
            }


            if (!gameObjects.Exists(delegate(GameObject gameObject)
            {
                return gameObject.GetType() == typeof(ExitObject);
            })) return Tuple.Create(LevelState.Completed, livesLeft);

            if (PhysicsEngine.IsCollidingWith(player.getCollisionBox(), bug) || PhysicsEngine.IsCollidingWith(player.getCollisionBox(), dragon) && dragon.isFatal())
            {
                if (livesLeft > 1) init(livesLeft - 1, gameTime);
                else return Tuple.Create(LevelState.NoLivesLeft, livesLeft);
            }

            if(Keyboard.GetState().IsKeyDown(Keys.X)) {
                player.deltaMovement = 39;
            }

            bug.Update(gameTime);
            dragon.Update(gameTime);
            player.Update(Keyboard.GetState(), gameTime, gameObjects);

            player.deltaMovement = 13;

            return Tuple.Create(LevelState.InProgress, livesLeft);
        }

        private void UpdateStory(GameTime gameTime)
        {
            double secondsOne = gameTime.TotalGameTime.TotalSeconds;

            if (secondsOne != initAt && ( secondsOne - initAt) % 3 == 0)
            {
                if (story.IndexOf(scene) + 1 == story.Count) isStartup = false;
                else //if (gameTime.TotalGameTime.TotalSeconds != 0)
                    scene = story.ElementAt(story.IndexOf(scene) + 1);
            }
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();

            if (isStartup) { DrawStory(spriteBatch); }
            else
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                gameObjects.ForEach(x => x.Draw(spriteBatch));
                bug.Draw(spriteBatch);
                dragon.Draw(spriteBatch);

                spriteBatch.Draw(creator, new Rectangle(screenWidth / 2 - 50, 5, 100, 100), Color.White);

                if (updateCount > 0)
                {
                    updateCount--;
                    spriteBatch.Draw(playerDialog, new Rectangle(250, 250, 100, 100), Color.White);
                    spriteBatch.Draw(devDialog, new Rectangle(screenWidth/2 + 50, 30, 100, 100), Color.White);
                }

                for (int index = 0; index < livesLeft; index++)
                {
                    spriteBatch.Draw(life, new Rectangle(screenWidth - 40 * (index + 1), 0, 40, 40), Color.White);
                }

                player.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private void DrawStory(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(scene, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
        }

        public void UnloadContent()
        {
        }

        public void init(int livesLeft, GameTime gameTime)
        {
            initAt = gameTime.TotalGameTime.TotalSeconds;
            this.livesLeft = livesLeft;
            PhysicsEngine.objects = gameObjects;
            bug = new MonsterObject(bugZilla, screenWidth / 2, screenHeight - 220, 70, 100);
            player = new Player(character, 10, 410, jump);
            gameObjects.Add(exit);
            gameObjects.Add(helmet);
            knowsPowerup = false;
            updateCount = 0;
        }
    }
}
