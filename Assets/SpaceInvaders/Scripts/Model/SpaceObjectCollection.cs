using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public class SpaceObjectCollection : IService, IReadOnlySpaceObjectCollection
    {
        private List<SpaceObject> _objects = new List<SpaceObject>();

        public event Action<SpaceObject> ObjectCreated;
        public event Action<SpaceObject> ObjectDestroyed;

        public T AddObject<T>(Vector2 position)
            where T : SpaceObject, new()
        {
            T spaceObject = new T();
            spaceObject.Position = position;

            _objects.Add(spaceObject);
            ObjectCreated?.Invoke(spaceObject);

            return spaceObject;
        }

        public void DestroyObject(SpaceObject spaceObject)
        {
            if (spaceObject.IsActive == true)
            {
                spaceObject.Destroy();
                ObjectDestroyed?.Invoke(spaceObject);
                _objects.Remove(spaceObject);
            }
        }

        public List<SpaceObject> GetAllObjects()
        {
            return new List<SpaceObject>(_objects);
        }

        public void Clear()
        {
            for(int i = _objects.Count - 1; i >= 0; i--)
            {
                if (_objects[i] != null)
                {
                    DestroyObject(_objects[i]);
                }
            }

            _objects.Clear();
        }
    }
}