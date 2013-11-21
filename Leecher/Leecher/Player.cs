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
        public int x, y, width, height;
        bool isJumping;
        TimeSpan timeSinceJumpStart;
        int deltaMovement = 2;
        int jumpDelta = 2;
        Texture2D texture;

        Vector2 Position; 
        Point frameSize = new Point(51, 71);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(3, 4);

        KeyboardState currentState;
        KeyboardState theKeyboardState;
        KeyboardState oldKeyboardState;


        enum State
        {
            Walking,
            Jump
        }


        State mCurrentState = State.Walking;

        TimeSpan nextFrameInterval = TimeSpan.FromSeconds((float)1 / 16);

        TimeSpan nextFrame;

        public Player(Texture2D tex, int xPos, int yPos)
        {
            texture = tex;
            x = xPos;
            y = yPos;
            isJumping = false;
            width = 70;
            height = 110;
            Position = new Vector2(x, y);
        }

        public Rectangle getCollisionBox()
        {
            return new Rectangle(x,y,width, height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(
                                frameSize.X * currentFrame.X,
                                frameSize.Y * currentFrame.Y,
                                frameSize.X,
                                frameSize.Y),
                                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            
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

                currentState = Keyboard.GetState();
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
                    
                }

               
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
            frameSize = new Point(51, 71);
            currentFrame.Y = 1;
            currentFrame.X++;
            if (currentFrame.X > 2)
                currentFrame.X = 0;
            x -= deltaMovement;
            Position = new Vector2(x, y);

        }

        private void MoveRight()
        {
            frameSize = new Point(51, 71);
            currentFrame.Y = 0;
            currentFrame.X++;
            if (currentFrame.X > 2)
                currentFrame.X = 0;
            x += deltaMovement;
            Position = new Vector2(x, y);
        }

        private void MoveDown()
        {
            y += jumpDelta;
            Position = new Vector2(x, y);
        }

        private void MoveUp()
        {
            y -= jumpDelta;
            Position = new Vector2(x, y);
        }

        public bool PlayerCollisionEffect(Keys keyPressed, Direction intendedDirection) { return false; }
    }
}
