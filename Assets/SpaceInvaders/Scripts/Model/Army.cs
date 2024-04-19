using System;

namespace SpaceInvaders
{
    public class Army
    {
        private LevelInfo _info;

        private Enemy[,] _enemies;
        private ArmyMovement _movement;
        private ArmyShooting _shooting;

        public event Action AllEnemiesDestroyed;

        public Army(LevelInfo info)
        {
            _info = info;

            _enemies = ArmyFactory.Create(_info);
            foreach (Enemy enemy in _enemies)
            {
                enemy.Destroyed += OnEnemyDestroyed;
            }

            _movement = new ArmyMovement(_enemies, _info);
            _shooting = new ArmyShooting(_enemies, _info);
        }

        public void Destroy()
        {
            _movement.Destroy();
            _shooting.Destroy();

            foreach (Enemy enemy in _enemies)
            {
                enemy.Destroyed -= OnEnemyDestroyed;
            }

            _enemies = null;
        }

        private void OnEnemyDestroyed()
        {
            _movement.SetSpeed();

            foreach (Enemy enemy in _enemies)
            { 
                if (enemy != null && enemy.IsActive == true)
                {
                    return;
                }
            }

            AllEnemiesDestroyed?.Invoke();
        }
    }
}