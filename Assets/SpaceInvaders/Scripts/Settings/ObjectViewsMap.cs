using UnityEngine;

namespace SpaceInvaders
{
    [CreateAssetMenu(menuName = "SpaceInvaders/Object Views Map")]
    public class ObjectViewsMap : ScriptableObject
    {
        [SerializeField] private ObjectViewRecord[] _records;

        public GameObject Get(string tag)
        {
            foreach (var record in _records)
            { 
                if (record.Tag == tag)
                {
                    return record.Sample;
                }
            }

            throw new System.Exception("Prefab of type {tag} does not exist!");
        }
    }
}