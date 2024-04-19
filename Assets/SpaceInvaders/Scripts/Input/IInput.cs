using System;

namespace SpaceInvaders
{
    public interface IInput: IService
    {
        public event Action<int> Moving;
        public event Action Shooting;
    }
}