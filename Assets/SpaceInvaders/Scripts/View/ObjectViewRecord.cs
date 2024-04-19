using System;
using UnityEngine;

namespace SpaceInvaders
{
    [Serializable]
    public struct ObjectViewRecord
    {
        public string Tag;
        public GameObject Sample;

        public ObjectViewRecord(string tag, GameObject sample)
        {
            Tag = tag;
            Sample = sample;
        }
    }
}