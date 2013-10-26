using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Leecher
{
    static class PhysicsEngine  //pass gameObjects as constructor argument while creating level
    {
        public static bool IsColliding(Rectangle thizBox, List<GameObject> objects)
        {
             return objects.Exists(delegate(GameObject that)
                 {
                     return thizBox.Intersects(that.getCollisionBox());
                 });

        }
    }

    //public static void PhysicsEngine.HandleCollision(Player player, List<GameObject> gameObjects)
    // for object which is colliging, call object.HandleCollision(player)


}