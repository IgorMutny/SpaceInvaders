using UnityEngine;

namespace SpaceInvaders
{
    public class ArmyMovement
    {
        private Timer _timer;
        private Enemy[,] _enemies;
        private LevelInfo _info;
        private GameRules _rules;

        private int _direction;
        private float _speed;

        public ArmyMovement(Enemy[,] enemies, LevelInfo info)
        {
            _enemies = enemies;
            _info = info;

            _timer = ServiceLocator.Get<Timer>();
            _rules = ServiceLocator.Get<GameRules>();
            _direction = 1;
            _timer.Tick += OnTick;


            SetSpeed();
        }

        public void Destroy()
        {
            _timer.Tick -= OnTick;
        }

        private void OnTick()
        {
            bool hasEnemies = false;

            foreach (var enemy in _enemies)
            {
                if (enemy != null && enemy.IsActive == true)
                {
                    hasEnemies = true;
                    enemy.Position += Vector2.right * _direction * _speed * Time.fixedDeltaTime;
                }
            }

            if (hasEnemies == true)
            {
                CheckBoundsReached();
            }
        }

        public void SetSpeed()
        {
            Enemy firstEnemy = GetFirstEnemy();
            Enemy lastEnemy = GetLastEnemy();

            if (firstEnemy != null && lastEnemy != null)
            {
                float armyWidth =
                    lastEnemy.Position.x - firstEnemy.Position.x + firstEnemy.Size.x;

                _speed = (_rules.ScreenSize.x - armyWidth) / _info.DescendingInterval;
            }
            else
            {
                _speed = 0;
            }
        }

        private void CheckBoundsReached()
        {
            Enemy headEnemy;
            switch (_direction)
            {
                case 1: headEnemy = GetLastEnemy(); break;
                case -1: headEnemy = GetFirstEnemy(); break;
                default: throw new System.Exception("Invalid direction!");
            }

            if (headEnemy != null && headEnemy.IsOutOfBounds() == true)
            {
                _direction = -_direction;
                Descend();
            }
        }

        private void Descend()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy != null && enemy.IsActive == true)
                {
                    enemy.Position += Vector2.down * _rules.DescendingDistance;
                }
            }
        }

        private Enemy GetLastEnemy()
        {
            for (int i = _info.EnemiesAmount.x - 1; i >= 0; i--)
            {
                for (int j = _info.EnemiesAmount.y - 1; j >= 0; j--)
                {
                    if (_enemies[i, j].IsActive == true)
                    {
                        return _enemies[i, j];
                    }
                }
            }

            return null;
        }

        private Enemy GetFirstEnemy()
        {
            for (int i = 0; i < _info.EnemiesAmount.x; i++)
            {
                for (int j = 0; j < _info.EnemiesAmount.y; j++)
                {
                    if (_enemies[i, j].IsActive == true)
                    {
                        return _enemies[i, j];
                    }
                }
            }

            return null;
        }
    }
}