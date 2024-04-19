using System;
using UnityEngine;

namespace SpaceInvaders
{
    public static class Collisions
    {
        private static SpaceObjectCollection _collection;

        static Collisions()
        {
            _collection = ServiceLocator.Get<SpaceObjectCollection>();
        }

        public static void Check(SpaceObject obj)
        {
            foreach (SpaceObject other in _collection.GetAllObjects())
            {
                if (other != obj)
                {
                    if (other != null && other.IsActive == true)
                    {
                        if (AreCollided(obj, other) == true)
                        {
                            TryDestroy(obj, other);
                            TryDestroy(other, obj);
                        }
                    }
                }
            }
        }

        private static bool AreCollided(SpaceObject obj, SpaceObject other)
        {
            Vector2 objPoint0 = GetPoint0(obj);
            Vector2 objPoint1 = GetPoint1(obj);
            Vector2 otherPoint0 = GetPoint0(other);
            Vector2 otherPoint1 = GetPoint1(other); ;

            bool doIntersectAtX =
                    DoProjectionsIntersect(
                        objPoint0.x, objPoint1.x, otherPoint0.x, otherPoint1.x);

            bool doIntersectAtY =
                DoProjectionsIntersect(
                    objPoint0.y, objPoint1.y, otherPoint0.y, otherPoint1.y);

            bool result = doIntersectAtX && doIntersectAtY;

            return result;
        }

        private static Vector2 GetPoint0(SpaceObject obj)
        {
            float x = obj.Position.x - obj.Size.x / 2;
            float y = obj.Position.y - obj.Size.y / 2;

            return new Vector2(x, y);
        }

        private static Vector2 GetPoint1(SpaceObject obj)
        {
            float x = obj.Position.x + obj.Size.x / 2;
            float y = obj.Position.y + obj.Size.y / 2;

            return new Vector2(x, y);
        }

        private static bool DoProjectionsIntersect(
            float this0, float this1, float other0, float other1)
        {
            bool result = (this0 >= other1 && this1 <= other0)
                || (this0 <= other1 && this1 >= other0);

            return result;
        }

        private static void TryDestroy(SpaceObject obj, SpaceObject other)
        {
            foreach (Type type in obj.DestroyingTypes)
            {
                if (type == other.GetType())
                {
                    _collection.DestroyObject(obj);
                }
            }
        }
    }
}