using System;
using UnityEngine;

namespace SpaceInvaders
{
    public class KeyboardInput : MonoBehaviour, IInput
    {
        private readonly string _horizontalAxis = "Horizontal";
        private readonly string _fireAxis = "Fire1";

        public event Action<int> Moving;
        public event Action Shooting;

        private void Update()
        {
            float horizontalInput = Input.GetAxisRaw(_horizontalAxis);
            Moving?.Invoke((int)horizontalInput);

            if (Input.GetAxisRaw(_fireAxis) != 0)
            {
                Shooting?.Invoke();
            }
        }
    }
}