using UnityEngine;

namespace SpaceInvaders
{
    [CreateAssetMenu(menuName = "SpaceInvaders/Game Rules")]
    public class GameRules : ScriptableObject, IService
    {
        [field: SerializeField] public LevelInfo[] Levels { get; private set; }
        [field: SerializeField] public Vector2Int ScreenSize { get; private set; }
        [field: SerializeField] public Vector2Int PlayerPosition { get; private set; }
        [field: SerializeField] public int PlayerSpeed { get; private set; }
        [field: SerializeField] public float PlayerReloadTime { get; private set; }
        [field: SerializeField] public Vector2Int ArmyCenter { get; private set; }
        [field: SerializeField] public Vector2Int DistanceBetweenEnemies { get; private set; }
        [field: SerializeField] public int DescendingDistance { get; private set; }
        [field: SerializeField] public int PlayerProjectileSpeed { get; private set; }
        [field: SerializeField] public int EnemyProjectileSpeed { get; private set; }
    }
}