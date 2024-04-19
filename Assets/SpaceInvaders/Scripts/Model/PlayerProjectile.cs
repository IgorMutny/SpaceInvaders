using UnityEngine;

namespace SpaceInvaders
{
    public class PlayerProjectile : SpaceObject
    {
        public PlayerProjectile() : base()
        {
            Size = new Vector2Int(2, 4);
            DestroyingTypes.Add(typeof(Enemy));

            float speedValue = ServiceLocator.Get<GameRules>().PlayerProjectileSpeed;
            Speed = Vector2.up * speedValue * Time.fixedDeltaTime;

            DestroyOnOutOfBounds = true;
        }
    }
}