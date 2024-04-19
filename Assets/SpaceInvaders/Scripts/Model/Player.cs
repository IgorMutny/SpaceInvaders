using UnityEngine;

namespace SpaceInvaders
{
    public class Player : SpaceObject
    {
        private IInput _input;
        private Timer _timer;
        private TimerSignal _allowShootingSignal;
        private float _shootingInterval;
        private bool _canShoot;
        private float _speedValue;

        public Player() : base()
        {
            Size = new Vector2Int(14, 8);
            DestroyingTypes.Add(typeof(EnemyProjectile));
            DestroyingTypes.Add(typeof(Enemy));
            ReturnOnOutOfBounds = true;

            _timer = ServiceLocator.Get<Timer>();
            _input = ServiceLocator.Get<IInput>();

            _input.Moving += SetMovement;
            _input.Shooting += TryShoot;

            _canShoot = true;
            GameRules rules = ServiceLocator.Get<GameRules>();
            _shootingInterval = rules.PlayerReloadTime;
            _speedValue = rules.PlayerSpeed;
        }

        public override void Destroy()
        {
            base.Destroy();

            _input.Moving -= SetMovement;
            _input.Shooting -= TryShoot;

            _timer.RemoveSignal(_allowShootingSignal);
            _allowShootingSignal = null;
        }

        private void SetMovement(int direction)
        {
            if (IsActive == false)
            {
                return;
            }

            Speed = Vector2.right * direction * _speedValue * Time.fixedDeltaTime;
        }

        private void TryShoot()
        {
            if (IsActive == false)
            {
                return;
            }

            if (_canShoot == true)
            {
                PlayerProjectileFactory.Create(Position);
                _canShoot = false;
                _allowShootingSignal = _timer.AddSignal(_shootingInterval, AllowShooting);
            }
        }

        private void AllowShooting()
        {
            _timer.RemoveSignal(_allowShootingSignal);
            _allowShootingSignal = null;
            _canShoot = true;
        }
    }
}