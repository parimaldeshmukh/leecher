using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Leecher
{
    static class PhysicsEngine
    {
        public static bool IsColliding(Rectangle thizBox, List<GameObject> objects)
        {
             return objects.Exists(delegate(GameObject that)
                 {
                     return thizBox.Intersects(that.getCollisionBox());
                 });

        }
    }
}