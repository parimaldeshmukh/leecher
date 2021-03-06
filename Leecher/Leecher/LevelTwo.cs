﻿using System;
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
    class LevelTwo : Level
    {
        List<GameObject> collidableObjects;
        Texture2D background, playerDialog, devStartDialog, character, theCreator, brick, shark, code, ground, ledge_error, portal1, portal2, cursor, life, devDialog;
        SpriteBatch spriteBatch;
        int screenHeight, screenWidth, cursorX, cursorY, livesLeft,updateCount=0;
        Player player;
        bool portalsBeingPlaced = false, isStartup= true;
        PortalObject portalOne, portalTwo;
        ExitObject exit;
        GameTime initAt;
        SoundEffect jump, dead;
        CollectibleObject gun;

        public LevelTwo(int livesLeft, GameTime gameTime) {
            collidableObjects = new List<GameObject>();
            initAt = gameTime;
            this.livesLeft = livesLeft;
        }

        public void Initialise()
        { 
        }

        public void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            life = content.Load<Texture2D>(@"life");
            background = content.Load<Texture2D>(@"background");
            character = content.Load<Texture2D>(@"sprite_sheet_arms");
            theCreator = content.Load<Texture2D>(@"theCreator");
            brick = content.Load<Texture2D>(@"brick");
            shark = content.Load<Texture2D>(@"shark");
            code = content.Load<Texture2D>(@"code");
            ground = content.Load<Texture2D>(@"ground");
            ledge_error = content.Load<Texture2D>(@"ledge_error");
            portal1 = content.Load<Texture2D>(@"portal1");
            portal2 = content.Load<Texture2D>(@"portal2");
            cursor = content.Load<Texture2D>(@"Cursor");
            jump = content.Load<SoundEffect>(@"jump");
            dead = content.Load<SoundEffect>(@"dead");

            playerDialog = content.Load<Texture2D>(@"dialog_21");
            devStartDialog = content.Load<Texture2D>(@"dialog_22");

            player = new Player(character, 10, 350, jump);

            exit = new ExitObject(content.Load<Texture2D>(@"exit"), screenWidth - 90, 400, 50, 60);
            devDialog = content.Load<Texture2D>(@"dialog_23");
            gun = new CollectibleObject(content.Load<Texture2D>(@"portal_gun"), 140, 350, 60, 40);
            gun.setSound(content.Load<SoundEffect>(@"collect"));
            collidableObjects.Add(gun);
            collidableObjects.Add(exit);

            for(int i = 0; i < 241; i=i+20)           
            collidableObjects.Add(new Ledge(brick, 0, 360, 460+i));

            for (int i = 0; i < 241; i = i + 20)
                collidableObjects.Add(new Ledge(brick, 880, screenWidth, 460 + i));


        }

        public Tuple<LevelState, int> Update(GameTime gameTime)
        {
            PhysicsEngine.objects = collidableObjects;


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return Tuple.Create(LevelState.Exited, livesLeft); 

            if (isStartup)
            {
                if (gameTime.TotalGameTime.TotalSeconds != 0 && gameTime.TotalGameTime.TotalSeconds % 5 == 0)
                    isStartup = false;
                return Tuple.Create(LevelState.InProgress, livesLeft);
            }

            if(!collidableObjects.Exists(x => x.GetType() == typeof(CollectibleObject)))
            {
                updateCount = 1;
            }

            
            if (player.y > screenHeight) {
                dead.Play();
                if (livesLeft > 1) init(livesLeft - 1, gameTime);
                else return Tuple.Create(LevelState.NoLivesLeft, livesLeft);;
            }
            if (portalsBeingPlaced)
                {
                    //place second portal
                    if (Keyboard.GetState().IsKeyDown(Keys.Z))
                    {
                        portalTwo = new PortalObject(portal2, cursorX, cursorY, 50, 60);
                        collidableObjects.Add(portalTwo);
                        portalsBeingPlaced = false;
                    }

                    else if (Keyboard.GetState().IsKeyDown(Keys.Up)) cursorY -= 10;
                    else if (Keyboard.GetState().IsKeyDown(Keys.Down)) cursorY += 10;
                    else if (Keyboard.GetState().IsKeyDown(Keys.Left)) cursorX -= 10;
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right)) cursorX += 10;

                }

            else if(Keyboard.GetState().IsKeyDown(Keys.X))
                {
                    // place first portal
                    collidableObjects.RemoveAll(x => x.GetType() == typeof(PortalObject) );
                    portalOne = new PortalObject(portal1, player.x + 100, player.y, 50, 60);
                    collidableObjects.Add(portalOne);
                    cursorX = player.x + 100;
                    cursorY = player.y;
                    portalsBeingPlaced = true;
                }
            
            else
            {
                bool portalsArePlaced = collidableObjects.Exists(x => x.GetType() == typeof(PortalObject));
                if (portalsArePlaced)
                {
                    Rectangle playerBox = player.getCollisionBox();
                    if (PhysicsEngine.IsCollidingWith(playerBox, portalOne))
                    {
                        player.x = portalTwo.getCollisionBox().Right + 10;
                        player.y = portalTwo.getCollisionBox().Bottom - player.height;
                    }
                    else if (PhysicsEngine.IsCollidingWith(playerBox, portalTwo))
                    {
                        player.x = portalOne.getCollisionBox().Left - 80;
                        player.y = portalOne.getCollisionBox().Bottom - player.height;
                    }

                    portalsBeingPlaced = false;
                }
                player.Update(Keyboard.GetState(), gameTime, collidableObjects);
            }


            if (collidableObjects.Exists(delegate(GameObject gameObject)
            {
                return gameObject.GetType() == typeof(ExitObject);
            })) return Tuple.Create(LevelState.InProgress, livesLeft);

            return Tuple.Create(LevelState.Completed, livesLeft);
        }

        public void init(int livesLeft, GameTime gameTime) {
            this.livesLeft = livesLeft;
            initAt = gameTime;
            collidableObjects.RemoveAll(x => x.GetType() == typeof(ExitObject));
            collidableObjects.Add(exit);
            player = new Player(character, 10, 350, jump);
            collidableObjects.RemoveAll(x => x.GetType() == typeof(PortalObject));
            collidableObjects.Add(gun);
            isStartup = true;
            updateCount = 0;
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

            if (isStartup)
            {
                spriteBatch.Draw(playerDialog, new Rectangle(10, 250, 100, 100), Color.White);
                spriteBatch.Draw(devStartDialog, new Rectangle(620, 50, 100, 100), Color.White);
            }

            spriteBatch.Draw(theCreator, new Rectangle(500, 30, 120, 120), Color.White);
            spriteBatch.Draw(shark, new Rectangle(400, 520, 480, 200), Color.White);
            spriteBatch.Draw(code, new Rectangle(0, 0, 400, 200), Color.White);
            if(portalsBeingPlaced) spriteBatch.Draw(cursor, new Rectangle(cursorX, cursorY, 50, 60), Color.White);
            if (updateCount != 0) spriteBatch.Draw(devDialog, new Rectangle(620, 70, 120, 120), Color.White); 
            spriteBatch.Draw(ledge_error, new Rectangle(450, 320, 400, 40), Color.White);

            for (int index = 0; index < livesLeft; index++)
            {
                spriteBatch.Draw(life, new Rectangle(screenWidth - 40 * (index + 1), 0, 40, 40), Color.White);
            }

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
