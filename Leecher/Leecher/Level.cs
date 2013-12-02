using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Leecher
{
    interface Level
    {
        void Initialise();
        void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, ContentManager content);
        Tuple<LevelState, int> Update(GameTime gameTime);
        void Draw(GraphicsDevice graphicsDevice);
        void UnloadContent();
        void init(int livesLeft);
    }
}
