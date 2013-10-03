﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Leecher
{
    class Ledge : GameObject
    {
        List<StaticObject> bricks = new List<StaticObject>();
        int from, to, atHeight;
        Texture2D texture;
        Rectangle collisionBox;

        public Ledge(Texture2D tex, int ledgeFrom, int ledgeTo, int ledgeAtHeight)
        {
            texture = tex;
            from = ledgeFrom;
            to = ledgeTo;
            atHeight = ledgeAtHeight;

            collisionBox = new Rectangle(from, atHeight, to - from, 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = from; index <= to; index += 40)
            {
                spriteBatch.Draw(texture, new Rectangle(index, atHeight, 40, 20), Color.White);
            }
        }

        public bool CollidesWith(GameObject other)
        {
            return false;
        }

    }
}