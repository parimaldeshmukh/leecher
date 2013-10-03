using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leecher
{
    class PhysicsEngine
    {
        public void Update(Player player, List<GameObject> objects)
        {
            objects.Exists(delegate(GameObject gameObject)
            {
                return player.CollidesWith(gameObject);
            });

            //look for player touching feet on ground, else isJumping true and 999 ticks

        }
    }
}
