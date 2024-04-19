using UnityEngine;

namespace SpaceInvaders
{
    [CreateAssetMenu(menuName = "SpaceInvaders/Level Info")]
    public class LevelInfo : ScriptableObject
    {
        [field: SerializeField] public Vector2Int EnemiesAmount { get; private set; }
        [field: SerializeField] public float DescendingInterval { get; private set; }
        [field: SerializeField] public float ShootingInterval { get; private set; }
    }
}
