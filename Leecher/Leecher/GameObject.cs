using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Leecher
{
    interface GameObject
    {
        void Draw(SpriteBatch spriteBatch);
        bool CollidesWith(GameObject other);
    }
}
