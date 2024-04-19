using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public class ArmyShooting
    {
        private Timer _timer;
        private TimerSignal _shootingSignal;

        private Enemy[,] _enemies;
        private LevelInfo _info;

        public ArmyShooting(Enemy[,] enemies, LevelInfo info)
        {
            _timer = ServiceLocator.Get<Timer>();
            _enemies = enemies;
            _info = info;

            _shootingSignal = _timer.AddSignal(_info.ShootingInterval, Shoot, true);
        }

        public void Destroy()
        {
            _timer.RemoveSignal(_shootingSignal);
            _shootingSignal = null;
        }

        private void Shoot()
        {
            Enemy shooter = GetRandomShooter();
            shooter.Shoot();
        }

        private Enemy GetRandomShooter()
        {
            List<Enemy> shooters = new List<Enemy>();

            for (int i = 0; i < _enemies.GetLength(0); i++)
            {
                for (int j = 0; j < _enemies.GetLength(1); j++)
                {
                    if (_enemies[i, j] != null && _enemies[i, j].IsActive == true)
                    {
                        shooters.Add(_enemies[i, j]);
                        break;
                    }
                }
            }

            int rnd = Random.Range(0, shooters.Count);
            return shooters[rnd];
        }
    }
}