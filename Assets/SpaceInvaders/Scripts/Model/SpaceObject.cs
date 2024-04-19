using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public abstract class SpaceObject : IReadOnlySpaceObject
    {
        public bool IsActive { get; private set; }
        public string Tag { get; private set; }
        public Vector2 Size { get; protected set; }
        public Vector2 Position { get { return _position; } set { SetPosition(value); } }
        public Vector2 Speed { get; set; }
        protected bool DestroyOnOutOfBounds { get; set; }
        protected bool ReturnOnOutOfBounds { get; set; }
        public List<Type> DestroyingTypes { get; private set; }

        private Timer _timer;
        private GameRules _rules;
        private Vector2 _position;

        public event Action Moved;
        public event Action Destroyed;

        public SpaceObject()
        {
            _rules = ServiceLocator.Get<GameRules>();
            _timer = ServiceLocator.Get<Timer>();
            _timer.Tick += OnTick;

            IsActive = true;
            Tag = GetType().Name;
            DestroyingTypes = new List<Type>();
        }

        public bool IsOutOfBounds()
        {
            return _position.x - Size.x / 2 <= 0
                || _position.x + Size.x / 2 >= _rules.ScreenSize.x
                || _position.y - Size.y / 2 <= 0
                || _position.y + Size.y / 2 >= _rules.ScreenSize.y;
        }

        public virtual void Destroy()
        {
            IsActive = false;
            _timer.Tick -= OnTick;
            Destroyed?.Invoke();
        }

        private void OnTick()
        {
            if (Speed != Vector2.zero)
            {
                Vector2 prevPosition = _position;
                _position += Speed;
                OnPositionChanged(prevPosition);
            }
        }

        private void SetPosition(Vector2 position)
        {
            Vector2 prevPosition = _position;
            _position = position;
            OnPositionChanged(prevPosition);
        }

        private void OnPositionChanged(Vector2 prevPosition)
        {
            Moved?.Invoke();
            Collisions.Check(this);

            if (IsOutOfBounds() == true)
            {
                if (ReturnOnOutOfBounds == true)
                {
                    _position = prevPosition;
                }

                if (DestroyOnOutOfBounds == true)
                {
                    ServiceLocator.Get<SpaceObjectCollection>().DestroyObject(this);
                }
            }
        }
    }
}