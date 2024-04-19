using UnityEngine;

namespace SpaceInvaders
{
    public static class EnemyProjectileFactory
    {
        private static SpaceObjectCollection _collection;

        static EnemyProjectileFactory()
        {
            _collection = ServiceLocator.Get<SpaceObjectCollection>();
        }

        public static EnemyProjectile Create(Vector2 position)
        {
            EnemyProjectile projectile = _collection.AddObject<EnemyProjectile>(position);
            return projectile;
        }
    }
}