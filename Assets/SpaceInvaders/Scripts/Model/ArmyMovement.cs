using UnityEngine;

namespace SpaceInvaders
{
    public class ArmyMovement
    {
        private Timer _timer;
        private TimerSignal _strafingSignal;

        private Enemy[,] _enemies;
        private LevelInfo _info;
        private GameRules _rules;

        private int _direction;

        public ArmyMovement(Enemy[,] enemies, LevelInfo info)
        {
            _enemies = enemies;
            _info = info;

            _timer = ServiceLocator.Get<Timer>();
            _rules = ServiceLocator.Get<GameRules>();
            _direction = 1;

            CreateStrafingSignal();
        }

        public void CreateStrafingSignal()
        {
            if (_strafingSignal != null)
            {
                _timer.RemoveSignal(_strafingSignal);
                _strafingSignal = null;
            }

            float period = GetStrafePeriod();
            _strafingSignal = _timer.AddSignal(period, Move, true);
        }

        public void Destroy()
        {
            _timer.RemoveSignal(_strafingSignal);
            _strafingSignal = null;
        }

        private void Move()
        {
            bool hasEnemies = false;

            foreach (Enemy enemy in _enemies)
            {
                if (enemy != null && enemy.IsActive == true)
                {
                    enemy.Position += Vector2Int.right * _direction;
                    hasEnemies = true;
                }
            }

            if (hasEnemies == true)
            {
                CheckBoundsReached();
            }
        }

        private float GetStrafePeriod()
        {
            Enemy firstEnemy = GetFirstEnemy();
            Enemy lastEnemy = GetLastEnemy();

            if (firstEnemy == null || lastEnemy == null)
            {
                return 0;
            }

            float armyWidth =
                lastEnemy.Position.x - firstEnemy.Position.x + firstEnemy.Size.x;

            float result = _info.DescendingInterval / (_rules.ScreenSize.x - armyWidth);
            return result;
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
                _direction = - _direction;
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