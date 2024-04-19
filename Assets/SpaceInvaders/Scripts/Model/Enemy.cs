using UnityEngine;

namespace SpaceInvaders
{
    public class Enemy : SpaceObject
    {
        public Enemy() : base()
        {
            Size = new Vector2Int(12, 8);
            DestroyingTypes.Add(typeof(PlayerProjectile));
        }

        public void Shoot()
        {
            EnemyProjectileFactory.Create(Position);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}