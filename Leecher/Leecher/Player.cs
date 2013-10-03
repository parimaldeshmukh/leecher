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
        int jumpTickCount=999;
        int jumpDeltaY = 2;
        
        Texture2D texture;

        public Player(Texture2D tex, int xPos, int yPos)
        {
            texture = tex;
            x = xPos;
            y = yPos;
            isJumping = false;
            width = 70;
            height = 110;
        }

        public bool IsJumping {
            get { return isJumping; }
            set { isJumping = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, width, height), Color.White);
        }

        public bool CollidesWith(GameObject other)
        { return false; }


        public void Update(KeyboardState state, GameTime gameTime)
        { 
            if (state.IsKeyDown(Keys.Up) || isJumping)
            {
                if (isJumping)
                {
                    timeSinceJumpStart += gameTime.ElapsedGameTime;

                    if (timeSinceJumpStart.TotalSeconds < 1.2)
                    {
                        y -= jumpDeltaY;
                        jumpTickCount++;
                    }

                    else if (jumpTickCount > 1)
                    {
                        y += jumpDeltaY;
                        jumpTickCount--;
                    }

                    else
                    {
                        isJumping = false;
                    }
                }

                else
                {
                    isJumping = true;
                    timeSinceJumpStart = TimeSpan.Zero;
                    y += jumpDeltaY;
                    jumpTickCount = 0;
                }
            }

            if (state.IsKeyDown(Keys.Right))
            {
                x += jumpDeltaY;
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                x -= jumpDeltaY;
            }
        }
    
    }
}
