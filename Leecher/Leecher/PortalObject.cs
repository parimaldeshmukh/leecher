using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    class PortalObject : GameObject
    {
        Texture2D texture;
        int x, y, height, width;
        Rectangle box;

        public Rectangle getCollisionBox()
        {
            return box;
        }

        public PortalObject(Texture2D tex, int posX, int posY, int objectWidth, int objectHeight)
        {
            texture = tex;
            x = posX;
            y = posY;
            width = objectWidth;
            height = objectHeight;
            box = new Rectangle(x, y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, box, Color.White);
        }


        public bool PlayerCollisionEffect(Keys keyPressed, Direction intendedDirection) // return false to allow player to pass through this object, return true for a real collision
        {
/*            GameObject other = PhysicsEngine.objects.Find(delegate(GameObject that)
            {
                return that.GetType() == typeof(PortalObject) && this != that;
            });
            */
            return false;
        }
    }
}
