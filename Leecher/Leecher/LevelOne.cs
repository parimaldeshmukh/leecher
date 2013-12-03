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
    class LevelOne : Level
    {
        List<GameObject> gameobjects;
        Texture2D background, character, theCreator, openComment, closeComment, brick;
        SpriteBatch spriteBatch;
        int screenHeight, screenWidth, livesLeft;
        Player player;
        ExitObject exit;
        SoundEffect jump, collect;
        bool isStartup = true;
        GameTime initAt;
        List<Texture2D> story;
        Texture2D scene, life;
        
        

        public LevelOne(int livesLeft, GameTime gameTime) {
            gameobjects = new List<GameObject>();
            initAt = gameTime;
            story = new List<Texture2D>();
            this.livesLeft = livesLeft;
        }

        public void Initialise()
        { 
        }

        public void init(int livesLeft, GameTime gameTime)
        {
            initAt = gameTime;
            this.livesLeft = livesLeft;
            isStartup = true;
            gameobjects.Add(exit);
            player = new Player(character, 10, screenHeight - 130, jump);
            CollectibleObject temp = new CollectibleObject(openComment, 300, 100, 40, 40);
            temp.setSound(collect);
            gameobjects.Add(temp);
            temp = new CollectibleObject(closeComment, screenWidth - 300, screenHeight - 100, 40, 40);
            temp.setSound(collect);
            gameobjects.Add(temp);
            scene = story.ElementAt(0);
        }

        public void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            life = content.Load<Texture2D>(@"life");
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;

            background = content.Load<Texture2D>(@"background");
            theCreator = content.Load<Texture2D>(@"theCreator");
            openComment = content.Load<Texture2D>(@"open_comment");
            closeComment = content.Load<Texture2D>(@"close_comment");
            brick = content.Load<Texture2D>(@"brick");

            character = content.Load<Texture2D>("sprite_sheet_without");

            collect = content.Load<SoundEffect>(@"collect");
            jump = content.Load<SoundEffect>(@"jump");

            player = new Player(character, 10, screenHeight - 130, jump);

            

            exit = new ExitObject(content.Load<Texture2D>(@"exit"), screenWidth - 90, screenHeight - 80, 50, 60);
            gameobjects.Add(exit);
            gameobjects.Add(new CollidableObject(content.Load<Texture2D>(@"tree"), screenWidth - 320, screenHeight - 400, 200, 380, 200, 100));
            gameobjects.Add(new CollidableObject(content.Load<Texture2D>(@"branch"), screenWidth - 370, screenHeight - 320, 50, 60));

            InteractiveObject cow = new InteractiveObject(content.Load<Texture2D>(@"cow"), screenWidth - 220, screenHeight - 475, 120, 80);
            cow.setInteractions(content.Load<Texture2D>(@"dialog_12"), content.Load<Texture2D>(@"dialog_11"), content.Load<Texture2D>(@"dialog_13"));
            gameobjects.Add(cow);

            CollectibleObject temp = new CollectibleObject(openComment, 300, 100, 40, 40);
            temp.setSound(collect);
            gameobjects.Add(temp);
            temp = new CollectibleObject(closeComment, screenWidth - 300, screenHeight - 100, 40, 40);
            temp.setSound(collect);
            gameobjects.Add(temp);

            gameobjects.Add(new Ledge(brick, 0, screenWidth, screenHeight - 20));
            gameobjects.Add(new Ledge(brick, 320, 480, screenHeight - 150));
            gameobjects.Add(new Ledge(brick, screenWidth / 2, screenWidth / 2 + 160, screenHeight - 250));
            gameobjects.Add(new Ledge(brick, screenWidth / 2 - 80, screenWidth / 2 + 80, screenHeight - 400));
            gameobjects.Add(new Ledge(brick, screenWidth / 2 - 180, screenWidth / 2 - 100, screenHeight - 500));

            LoadStoryBoards(content);
            
        }

        private void LoadStoryBoards(ContentManager content)
        {
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_11"));
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_12"));
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_13"));
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_14"));
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_15"));
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_16"));
            story.Add(content.Load<Texture2D>(@"storyboard-1/scene_17"));

            story.Add(content.Load<Texture2D>(@"storyboard-2/scene_21"));
            story.Add(content.Load<Texture2D>(@"storyboard-2/scene_22"));
            story.Add(content.Load<Texture2D>(@"storyboard-2/scene_23"));
            story.Add(content.Load<Texture2D>(@"storyboard-2/scene_24"));
            story.Add(content.Load<Texture2D>(@"storyboard-2/scene_25"));

            scene = story.ElementAt(0);
        }

        public Tuple<LevelState,int> Update(GameTime gameTime)
        {
            if (isStartup) {
                if (Keyboard.GetState().GetPressedKeys().Length != 0) 
                    isStartup = false;
                UpdateStory(gameTime);
                return Tuple.Create(LevelState.InProgress, livesLeft); }
            else
            {
                PhysicsEngine.objects = gameobjects;

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    return Tuple.Create(LevelState.Exited, livesLeft);

                player.Update(Keyboard.GetState(), gameTime, gameobjects);

                if (gameobjects.Exists(delegate(GameObject gameObject)
                {
                    return gameObject.GetType() == typeof(ExitObject);
                })) return Tuple.Create(LevelState.InProgress, livesLeft);

                return Tuple.Create(LevelState.Completed, livesLeft);
            }
        }

        private void UpdateStory(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds % 3 == 0){
            if (story.IndexOf(scene) + 1 == story.Count) isStartup = false;
            else if(gameTime.TotalGameTime.TotalSeconds != 0)
                scene = story.ElementAt(story.IndexOf(scene) + 1);
            }
        }
        public void UnloadContent()
        {
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);


            Rectangle backgroundContainer = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Begin();

            if (isStartup) { DrawStory(spriteBatch); }
            else
            {
                spriteBatch.Draw(background, backgroundContainer, Color.White);
                spriteBatch.Draw(theCreator, new Rectangle(30, 10, 120, 120), Color.White);

                for (int index = 0; index < livesLeft; index++)
                {
                    spriteBatch.Draw(life, new Rectangle(screenWidth - 40 * (index+1), 0, 40, 40), Color.White);
                }

                DrawStatics();
                player.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private void DrawStory(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(scene, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
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
