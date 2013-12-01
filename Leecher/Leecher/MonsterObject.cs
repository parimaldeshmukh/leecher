﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    class MonsterObject : GameObject
    {

        int x, y, width, height, deltaX;
        Texture2D text;
        Point frameSize = new Point(35, 62), currentFrame = new Point(0, 0);

        public MonsterObject(Texture2D texture, int xPos, int yPos, int Width, int Height, int delta = -3)
        {
            text = texture;
            x = xPos;
            y = yPos;
            width = Width;
            height = Height;
            deltaX = delta;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(text, new Rectangle(x, y, width, height), Color.White);
            spriteBatch.Draw(text, new Rectangle(x, y, width, height), new Rectangle(
                              frameSize.X * currentFrame.X,
                              frameSize.Y * currentFrame.Y,
                              frameSize.X,
                              frameSize.Y),
                              Color.White);
        }

        public Rectangle getCollisionBox()
        {
            return new Rectangle(x, y, width, height);
        }

        public bool PlayerCollisionEffect(Keys keyPresseed, Direction intendedDirection) 
        {
            // reset level
            return false;
        }


        // this will trigger all GameObject effects the same as if player collides :(
        public void Update()
        {
            if (PhysicsEngine.IsColliding(new Rectangle(x + deltaX, y, width, height), Keys.None, deltaX > 0 ? Direction.Right : Direction.Left))
            {
                deltaX = -deltaX;
                currentFrame.Y = (currentFrame.Y + 1) % 2;
            }

            else
            {
                x += deltaX;
                currentFrame.X = (currentFrame.X + 1) % 3;
            }
        }
    }
}
