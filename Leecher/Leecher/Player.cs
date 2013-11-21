using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    class Player : GameObject
    {
        public int x, y, width, height, drawAdjustment;
        bool isJumping;
        TimeSpan timeSinceJumpStart;
        int deltaMovement = 13;
        int jumpDelta = 13;
        Texture2D texture;

        Point frameSize = new Point(60,88);
        Point currentFrame = new Point(0, 0);

        enum State
        {
            Walking,
            Jump
        }


        public Player(Texture2D tex, int xPos, int yPos, int adjustment)
        {
            texture = tex;
            x = xPos;
            y = yPos;
            isJumping = false;
            width = 70;
            height = 110;
            drawAdjustment = adjustment;
        }

        public Rectangle getCollisionBox()
        {
            return new Rectangle(x,y,width, height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, width, height), new Rectangle(
                                frameSize.X * currentFrame.X,
                                frameSize.Y * currentFrame.Y,
                                frameSize.X,
                                frameSize.Y - drawAdjustment),
                                Color.White);
            
            //spriteBatch.Draw(texture, new Rectangle(x, y, width, height), Color.White);
        }

        public void Update(KeyboardState state, GameTime gameTime, List<GameObject> gameObjects)
        { 
            if (isJumping)
            {
                timeSinceJumpStart += gameTime.ElapsedGameTime;

                if (timeSinceJumpStart.TotalSeconds < 1.2)          // magic number 1.2 for duration of jump :(
                {
                    if (!PhysicsEngine.IsColliding(new Rectangle(x, y - jumpDelta, width, height), Keys.None, Direction.Up)) MoveUp();
                    else timeSinceJumpStart += new TimeSpan(1, 1, 1, 1, 1);             // dirty, adding a day to timeSinceJumpStart so that he starts dropping :(
                }

                else
                {
                    if (!PhysicsEngine.IsColliding(new Rectangle(x, y + jumpDelta, width, height), Keys.None, Direction.Down)) MoveDown();
                    else
                    {
                        isJumping = false;
                    }
                }

               /* currentState = Keyboard.GetState();
                theKeyboardState = Keyboard.GetState();

                if (mCurrentState == State.Walking)
                {
                    currentFrame.X++;
                    if (currentFrame.X >= 3)
                        currentFrame.X = 0;
                }

                oldKeyboardState = theKeyboardState;

                if (currentState.IsKeyDown(Keys.Right))
                {
                    
                }

                if (currentState.IsKeyDown(Keys.Left))
                {
                    
                }*/

               
            }

            else if(state.IsKeyDown(Keys.Space))
            {
                isJumping = true;
                timeSinceJumpStart = TimeSpan.Zero;
                if (!PhysicsEngine.IsColliding(new Rectangle(x, y - jumpDelta, width, height), Keys.None, Direction.Up)) MoveUp();
            }

            if (!isJumping && !PhysicsEngine.IsColliding(new Rectangle(x, y + jumpDelta, width, height), Keys.None, Direction.Down)) MoveDown();
            UpdateHorizontalMovement(state, gameObjects);

            if (state.IsKeyDown(Keys.Up))
            {
                
                if (PhysicsEngine.IsColliding(new Rectangle(x, y - deltaMovement, width, height), Keys.Up, Direction.Up))
                {
                    MoveUp();
                }
            }
                
        }

       

        private void UpdateHorizontalMovement(KeyboardState state, List<GameObject> gameObjects)
        {
            if (state.IsKeyDown(Keys.Right))
            {
                if (!PhysicsEngine.IsColliding(new Rectangle(x + deltaMovement, y, width, height), Keys.Right, Direction.Right)) MoveRight();
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                if (!PhysicsEngine.IsColliding(new Rectangle(x - deltaMovement, y, width, height), Keys.Left, Direction.Left)) MoveLeft();
            }
        }

        private void MoveLeft()
        {
            currentFrame.Y = 1;
            currentFrame.X++;
            if (currentFrame.X > 2)
                currentFrame.X = 0;
            x -= deltaMovement;

        }

        private void MoveRight()
        {
            currentFrame.Y = 0;
            currentFrame.X++;
            if (currentFrame.X > 2)
                currentFrame.X = 0;
            x += deltaMovement;
        }

        private void MoveDown()
        {
            y += jumpDelta;
        }

        private void MoveUp()
        {
            y -= jumpDelta;
        }

        public bool PlayerCollisionEffect(Keys keyPressed, Direction intendedDirection) { return false; }
    }
}
