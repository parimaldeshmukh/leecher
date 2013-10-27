using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Leecher
{
    class Ledge : GameObject
    {
        List<CollidableObject> bricks = new List<CollidableObject>();
        int from, to, atHeight;
        Texture2D texture;
        private Rectangle box;

        public Rectangle getCollisionBox()
        {
           return box; 
        }

        public Ledge(Texture2D tex, int ledgeFrom, int ledgeTo, int ledgeAtHeight)
        {
            texture = tex;
            from = ledgeFrom;
            to = ledgeTo;
            atHeight = ledgeAtHeight;

            box = new Rectangle(from, atHeight, to - from, 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = from; index <= to; index += 40)
            {
                spriteBatch.Draw(texture, new Rectangle(index, atHeight, 40, 20), Color.White);
            }
        }

        public bool PlayerCollisionEffect()
        {
            return true;
        }
    }
}
