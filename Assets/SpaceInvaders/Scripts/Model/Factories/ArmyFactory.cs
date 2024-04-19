using UnityEngine;

namespace SpaceInvaders
{
    public static class ArmyFactory
    {
        private static SpaceObjectCollection _collection;
        private static GameRules _rules;

        static ArmyFactory()
        {
            _collection = ServiceLocator.Get<SpaceObjectCollection>();
            _rules = ServiceLocator.Get<GameRules>();
        }

        public static Enemy[,] Create(LevelInfo info)
        {

            Vector2Int enemiesAmount = info.EnemiesAmount;
            Vector2Int center = _rules.ArmyCenter;
            Vector2Int distanceBetweenEnemies = _rules.DistanceBetweenEnemies;

            Enemy[,] enemies = new Enemy[enemiesAmount.x, enemiesAmount.y];

            Vector2Int[,] positions
                = ArmyPositions.Get(enemiesAmount, center, distanceBetweenEnemies);

            for (int i = 0; i < positions.GetLength(0); i++)
            {
                for (int j = 0; j < positions.GetLength(1); j++)
                {
                    enemies[i, j] = _collection.AddObject<Enemy>(positions[i, j]);
                }
            }

            return enemies;
        }


    }
}