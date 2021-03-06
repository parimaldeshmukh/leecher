﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    class CollidableObject : GameObject
    {
        Texture2D texture;
        int x, y, height, width;
        Rectangle box, collisionBox;

        public Rectangle getCollisionBox()
        {
            return collisionBox; 
        }

        public CollidableObject(Texture2D tex, int posX, int posY, int objectWidth, int objectHeight, int collisionWidth=0, int collisionHeight=0)
        {
            texture = tex;
            x = posX;
            y = posY;
            width = objectWidth;
            height = objectHeight;
            box = new Rectangle(x, y, width, height);

            if (collisionWidth == 0 || collisionHeight == 0)
                collisionBox = box;
            else collisionBox = new Rectangle(x, y, collisionWidth, collisionHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, box, Color.White);
        }


        public bool PlayerCollisionEffect(Keys keyPressed, Direction intendedDirection) // return false to allow player to pass through this object, return true for a real collision
        {
            return true;
        }
    }
}