using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public class Timer : MonoBehaviour, IService
    {
        private List<TimerSignal> _signals = new List<TimerSignal>();
        private List<TimerSignal> _signalsToAdd = new List<TimerSignal>();
        private List<TimerSignal> _signalsToRemove = new List<TimerSignal>();

        public event Action Tick;

        private void FixedUpdate()
        {
            Tick?.Invoke();

            HandleSignalsToRemove();
            HandleActiveSignals();
            HandleSignalsToAdd();
        }

        public TimerSignal AddSignal(float period, Action action, bool repeating = false)
        {
            TimerSignal signal = new TimerSignal(period, action, repeating);
            _signalsToAdd.Add(signal);
            signal.ShouldBeRemoved += () => RemoveSignal(signal);
            return signal;
        }

        public void RemoveSignal(TimerSignal signal)
        {
            if (_signals.Contains(signal) == true)
            {
                signal.ShouldBeRemoved -= () => RemoveSignal(signal);
                _signalsToRemove.Add(signal);
            }
        }


        private void HandleSignalsToAdd()
        {
            foreach (TimerSignal signal in _signalsToAdd)
            {
                _signals.Add(signal);
            }

            _signalsToAdd.Clear();
        }

        private void HandleActiveSignals()
        {
            foreach (TimerSignal signal in _signals)
            {
                signal.OnUpdate();
            }
        }

        private void HandleSignalsToRemove()
        {
            foreach (TimerSignal signal in _signalsToRemove)
            {
                _signals.Remove(signal);
            }

            _signalsToRemove.Clear();
        }
    }
}