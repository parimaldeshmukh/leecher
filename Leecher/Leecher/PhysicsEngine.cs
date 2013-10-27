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
        {/*
             return objects.Exists(delegate(GameObject that)
                 {
                     return thizBox.Intersects(that.getCollisionBox());
                 });*/

            List<GameObject> collidingObjects = objects.FindAll(delegate(GameObject that)
            {
                return thizBox.Intersects(that.getCollisionBox());
                
            });



            if (collidingObjects == null)
                return false;
            else
            {
                bool toReturn = false;
                collidingObjects.ForEach(delegate(GameObject that) {
                    toReturn = that.PlayerCollisionEffect() || toReturn;
                });

                return toReturn;
                //return collidingObject.PlayerCollisionEffect();
            }
        }
    }

    //public static void PhysicsEngine.HandleCollision(Player player, List<GameObject> gameObjects)
    // for object which is colliging, call object.HandleCollision(player)


}