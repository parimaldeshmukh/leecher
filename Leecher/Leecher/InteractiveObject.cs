using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Leecher
{
    class InteractiveObject : GameObject
    {
        Texture2D myDialog, playerDialog, devDialog;
        Texture2D texture;
        int x, y, height, width, updateCount = 0;
        Rectangle box, collisionBox, myPosition = Rectangle.Empty, playerPosition = Rectangle.Empty, devPosition = Rectangle.Empty;


        public Rectangle getCollisionBox()
        {
            return collisionBox; 
        }

        public InteractiveObject(Texture2D tex, int posX, int posY, int objectWidth, int objectHeight, int collisionWidth=0, int collisionHeight=0)
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

            if (updateCount > 0)
            {
                spriteBatch.Draw(myDialog, myPosition, Color.White);
                spriteBatch.Draw(playerDialog, playerPosition, Color.White);
                spriteBatch.Draw(devDialog, devPosition, Color.White);
                updateCount--;
            }
        }


        public bool PlayerCollisionEffect(Keys keyPressed, Direction intendedDirection) // return false to allow player to pass through this object, return true for a real collision
        {
            updateCount = 40;

            if (myPosition == Rectangle.Empty)
            {
                myPosition = new Rectangle(box.X, box.Y - 120, 120, 120);
            }

            if (playerPosition == Rectangle.Empty)
            {
                playerPosition = new Rectangle(box.X - 120, box.Y - 120, 120, 120);
            }

            if (devPosition == Rectangle.Empty)
            {
                devPosition = new Rectangle(165, 5, 120, 120);
            }
            return true;
        }

        public void setInteractions(Texture2D myInteraction, Texture2D playerInteraction, Texture2D devDialog)
        {
            myDialog = myInteraction;
            this.devDialog = devDialog;
            playerDialog = playerInteraction;
        }

        public void setPositions(Rectangle myPosition, Rectangle playerPosition, Rectangle devPosition)
        {
            this.myPosition = myPosition;
            this.playerPosition = playerPosition;
            this.devPosition = devPosition;
        }
        
    }
}