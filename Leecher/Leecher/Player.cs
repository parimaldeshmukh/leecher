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
        Rectangle box;
        Texture2D texture;

        public Player(Texture2D tex, int xPos, int yPos)
        {
            texture = tex;
            x = xPos;
            y = yPos;
            isJumping = false;
            width = 70;
            height = 110;
            box = new Rectangle(x, y, width, height);
        }

        public Rectangle getCollisionBox()
        {
            return box;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, width, height), Color.White);
        }

        public void Update(KeyboardState state, GameTime gameTime, List<GameObject> gameObjects)
        { 
            if (isJumping)
            {
                timeSinceJumpStart += gameTime.ElapsedGameTime;

                if (timeSinceJumpStart.TotalSeconds < 1.2)          // magic number 1.2 for duration of jump :(
                {
                    if (!PhysicsEngine.IsColliding(new Rectangle(x, y - jumpDelta, width, height))) MoveUp();
                    else timeSinceJumpStart += new TimeSpan(1, 1, 1, 1, 1);             // dirty, adding a day to timeSinceJumpStart so that he starts dropping :(
                }

                else
                {
                    if (!PhysicsEngine.IsColliding(new Rectangle(x, y + jumpDelta, width, height))) MoveDown();
                    else
                    {
                        isJumping = false;
                    }
                }

               
            }

            else if(state.IsKeyDown(Keys.Space))
            {
                isJumping = true;
                timeSinceJumpStart = TimeSpan.Zero;
                if (!PhysicsEngine.IsColliding(new Rectangle(x, y - jumpDelta, width, height))) MoveUp();
            }

            if (!isJumping && !PhysicsEngine.IsColliding(new Rectangle(x, y + jumpDelta, width, height))) MoveDown();
            UpdateHorizontalMovement(state, gameObjects);
        }

       

        private void UpdateHorizontalMovement(KeyboardState state, List<GameObject> gameObjects)
        {
            if (state.IsKeyDown(Keys.Right))
            {
                if (!PhysicsEngine.IsColliding(new Rectangle(x + deltaMovement, y, width, height))) MoveRight();
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                if (!PhysicsEngine.IsColliding(new Rectangle(x - deltaMovement, y, width, height))) MoveLeft();
            }
        }

        private void MoveLeft()
        {
            x -= deltaMovement;
        }

        private void MoveRight()
        {
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

        public bool PlayerCollisionEffect() { return false; }
    }
}
