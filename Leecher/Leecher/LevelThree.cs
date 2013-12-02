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
        int screenHeight, screenWidth, livesLeft;
        Player player;
        Texture2D background, brick, character, life, bugZilla;
        SoundEffect jump;
        MonsterObject bug, dragon;
        ExitObject exit;

        public LevelThree(int livesLeft)
        {
            gameObjects = new List<GameObject>();
            this.livesLeft = livesLeft;
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

           dragon = new MonsterObject(content.Load<Texture2D>(@"dragon"), screenWidth / 2 - 250, screenHeight - 600, 550, 250, 0);
           dragon.setFrameSize(539, 389);
           dragon.setCollisionBox(screenWidth / 2 - 120, screenHeight - 600, 240, 250);

            spriteBatch = new SpriteBatch(graphicsDevice);
            brick = content.Load<Texture2D>(@"brick");
            background = content.Load<Texture2D>(@"background");

            bugZilla = content.Load<Texture2D>(@"bug");
            bug = new MonsterObject(bugZilla, screenWidth / 2, screenHeight - 220, 70, 100);

            exit = new ExitObject(content.Load<Texture2D>(@"exit"), screenWidth - 90, screenHeight - 260, 50, 60);

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
            gameObjects.Add(new Ledge(brick, screenWidth /2 - 280, screenWidth/2 + 280, screenHeight - 350));

            gameObjects.Add(exit);
        }

        public Tuple<LevelState,int> Update(GameTime gameTime)
        {
            PhysicsEngine.objects = gameObjects;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return Tuple.Create(LevelState.Exited, livesLeft);

            if (!gameObjects.Exists(delegate(GameObject gameObject)
            {
                return gameObject.GetType() == typeof(ExitObject);
            })) return Tuple.Create(LevelState.Completed, livesLeft);

            if (PhysicsEngine.IsCollidingWith(player.getCollisionBox(), bug) || PhysicsEngine.IsCollidingWith(player.getCollisionBox(), dragon) && dragon.isFatal())
            {
                if (livesLeft > 1) init(livesLeft - 1);
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

        public void Draw(GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            gameObjects.ForEach(x => x.Draw(spriteBatch));
            bug.Draw(spriteBatch);
            dragon.Draw(spriteBatch);

            for (int index = 0; index < livesLeft; index++)
            {
                spriteBatch.Draw(life, new Rectangle(screenWidth - 40 * (index + 1), 0, 40, 40), Color.White);
            }

            player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void UnloadContent()
        {
        }

        public void init(int livesLeft)
        {
            this.livesLeft = livesLeft;
            PhysicsEngine.objects = gameObjects;
            bug = new MonsterObject(bugZilla, screenWidth / 2, screenHeight - 220, 70, 100);
            player = new Player(character, 10, 410, jump);
        }
    }
}
