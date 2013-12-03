using System;
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
        Rectangle collisionBox;

        public MonsterObject(Texture2D texture, int xPos, int yPos, int Width, int Height, int delta = -3)
        {
            text = texture;
            x = xPos;
            y = yPos;
            width = Width;
            height = Height;
            deltaX = delta;
            collisionBox = Rectangle.Empty;
        }

        public void setFrameSize(int x, int y)
        {
            frameSize = new Point(x,y);
        }

        public void setCollisionBox(int x, int y, int width, int height)
        {
            collisionBox = new Rectangle(x, y, width, height);
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
            return collisionBox == Rectangle.Empty? new Rectangle(x, y, width, height) : collisionBox;
        }

        public bool PlayerCollisionEffect(Keys keyPresseed, Direction intendedDirection) 
        {
            // reset level
            return false;
        }
        // this will trigger all GameObject effects the same as if player collides :(

        
        public void Update(GameTime gameTime)
        {
            if (PhysicsEngine.IsColliding(new Rectangle(x + deltaX, y, width, height), Keys.None, deltaX > 0 ? Direction.Right : Direction.Left))
            {
                deltaX = -deltaX;
                currentFrame.Y = (currentFrame.Y + 1) % 2;
            }

            else
            {
                x += deltaX;
                currentFrame.X = deltaX!=0? (currentFrame.X + 1) % 3 : gameTime.TotalGameTime.TotalSeconds % 2 == 0? (currentFrame.X + 1) % 3: currentFrame.X;
            }

           
        }

        internal bool isFatal()
        {
            if (currentFrame.X != 2) return true;
            return false;
        }
    }
}
