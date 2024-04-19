using UnityEngine;

namespace SpaceInvaders
{
    public static class PlayerProjectileFactory
    {
        private static SpaceObjectCollection _collection;

        static PlayerProjectileFactory()
        {
            _collection = ServiceLocator.Get<SpaceObjectCollection>();
        }

        public static PlayerProjectile Create(Vector2 position)
        {
            PlayerProjectile projectile = _collection.AddObject<PlayerProjectile>(position);
            return projectile;
        }
    }
}