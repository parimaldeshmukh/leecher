using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    class LevelTwo : Level
    {
        List<GameObject> collidableObjects;
        Texture2D background, character, theCreator, brick, shark, code, ground, ledge_error, portal1, portal2, cursor;
        SpriteBatch spriteBatch;
        int screenHeight, screenWidth, cursorX, cursorY;
        Player player;
        bool portalsBeingPlaced = false;

        public LevelTwo() {
            collidableObjects = new List<GameObject>();
        }

        public void Initialise()
        { 
        }

        public void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;

            background = content.Load<Texture2D>(@"background");
            character = content.Load<Texture2D>(@"test_subject");
            theCreator = content.Load<Texture2D>(@"theCreator");
            brick = content.Load<Texture2D>(@"brick");
            shark = content.Load<Texture2D>(@"shark");
            code = content.Load<Texture2D>(@"code");
            ground = content.Load<Texture2D>(@"ground");
            ledge_error = content.Load<Texture2D>(@"ledge_error");
            portal1 = content.Load<Texture2D>(@"portal1");
            portal2 = content.Load<Texture2D>(@"portal2");
            cursor = content.Load<Texture2D>(@"Cursor");

            player = new Player(character, 10, 300);

            collidableObjects.Add(new CollidableObject(content.Load<Texture2D>(@"exit"), screenWidth - 90, 400, 50, 60));

            for(int i = 0; i < 241; i=i+20)           
            collidableObjects.Add(new Ledge(brick, 0, 360, 460+i));

            for (int i = 0; i < 241; i = i + 20)
                collidableObjects.Add(new Ledge(brick, 880, screenWidth, 460 + i));


        }

        public LevelState Update(GameTime gameTime)
        {
            PhysicsEngine.objects = collidableObjects;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return LevelState.Exited;

            if (portalsBeingPlaced)
                {
                    //place second portal
                    if (Keyboard.GetState().IsKeyDown(Keys.Z))
                    {
                        collidableObjects.Add(new PortalObject(portal2, cursorX, cursorY, 50, 60));
                        portalsBeingPlaced = false;
                    }

                    else if (Keyboard.GetState().IsKeyDown(Keys.Up)) cursorY -= 5;
                    else if (Keyboard.GetState().IsKeyDown(Keys.Down)) cursorY += 5;
                    else if (Keyboard.GetState().IsKeyDown(Keys.Left)) cursorX -= 5;
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right)) cursorX += 5;

                }

            else if(Keyboard.GetState().IsKeyDown(Keys.X))
                {
                    // place first portal
                    collidableObjects.RemoveAll(x => x.GetType() == typeof(PortalObject) );
                    collidableObjects.Add(new PortalObject(portal1, player.x + 100, player.y, 50, 60));
                    cursorX = player.x + 100;
                    cursorY = player.y;
                    portalsBeingPlaced = true;
                }
            

            else
            {
                player.Update(Keyboard.GetState(), gameTime, collidableObjects);
            }
            return LevelState.InProgress;
        }

        public void UnloadContent()
        {
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);


            Rectangle backgroundContainer = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundContainer, Color.White);
            spriteBatch.Draw(theCreator, new Rectangle(1050, 30, 120, 120), Color.White);
            spriteBatch.Draw(shark, new Rectangle(400, 520, 480, 200), Color.White);
            spriteBatch.Draw(code, new Rectangle(0, 0, 400, 200), Color.White);
            if(portalsBeingPlaced) spriteBatch.Draw(cursor, new Rectangle(cursorX, cursorY, 50, 60), Color.White);

            spriteBatch.Draw(ledge_error, new Rectangle(450, 320, 400, 40), Color.White);

            DrawStatics();
            player.Draw(spriteBatch);

            spriteBatch.End();
            
        }

        private void DrawStatics()
        {
            collidableObjects.ForEach(
                delegate(GameObject gameObject)
                {
                    gameObject.Draw(spriteBatch);
                });
        }
    }
}
