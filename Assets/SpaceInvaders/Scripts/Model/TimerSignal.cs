using System;
using UnityEngine;

namespace SpaceInvaders
{
    public class TimerSignal
    {
        private readonly float _period;
        private Action _action;
        private bool _repeating;

        private float _counter;
        private bool _isActive;

        public event Action ShouldBeRemoved;

        public TimerSignal(float period, Action action, bool repeating)
        {
            _period = period;
            _action = action;
            _repeating = repeating;

            _counter = _period;
            _isActive = true;
        }

        public void OnUpdate()
        {
            if (_isActive == false)
            {
                return;
            }

            _counter -= Time.fixedDeltaTime;

            if (_counter <= 0)
            {
                _action();

                if (_repeating == true)
                {
                    _counter += _period;
                }
                else
                {
                    ShouldBeRemoved?.Invoke();
                }
            }
        }
    }
}