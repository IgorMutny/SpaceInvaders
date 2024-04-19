using UnityEngine;

namespace SpaceInvaders
{
    public class EnemyProjectile : SpaceObject
    {
        public EnemyProjectile() : base()
        {
            Size = new Vector2Int(2, 4);
            DestroyingTypes.Add(typeof(Player));

            float speedValue = ServiceLocator.Get<GameRules>().EnemyProjectileSpeed;
            Speed = Vector2.down * speedValue * Time.fixedDeltaTime;

            DestroyOnOutOfBounds = true;
        }
    }
}