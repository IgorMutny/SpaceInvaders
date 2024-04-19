using System;

namespace SpaceInvaders
{
    public interface IReadOnlySpaceObjectCollection
    {
        public event Action<SpaceObject> ObjectCreated;
        public event Action<SpaceObject> ObjectDestroyed;
    }
}