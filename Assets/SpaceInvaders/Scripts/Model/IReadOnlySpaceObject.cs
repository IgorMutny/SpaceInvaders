using System;
using UnityEngine;

namespace SpaceInvaders
{
    public interface IReadOnlySpaceObject
    {
        public string Tag { get; }
        public Vector2 Position { get; }

        public event Action Moved;
    }
}