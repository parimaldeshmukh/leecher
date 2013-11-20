using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Leecher
{
    static class PhysicsEngine  //pass gameObjects as constructor argument while creating level
    {

        public static List<GameObject> objects;

        public static bool IsColliding(Rectangle thizBox, Keys keyPressed, Direction intendedDirection)
        {
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
                    toReturn = that.PlayerCollisionEffect(keyPressed, intendedDirection) || toReturn;
                });

                return toReturn;
            }
        }

        public static bool IsCollidingWith(GameObject thisObject,GameObject someObject)
        {
            return objects.Contains(someObject) && thisObject.getCollisionBox().Intersects(someObject.getCollisionBox());
        }
    }
}