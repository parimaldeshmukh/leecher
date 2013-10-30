using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    interface GameObject
    {
        void Draw(SpriteBatch spriteBatch);
        Microsoft.Xna.Framework.Rectangle getCollisionBox();
        bool PlayerCollisionEffect(Keys keyPresseed, Direction intendedDirection);
    }
}
