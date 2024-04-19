using UnityEngine;

namespace SpaceInvaders
{
    public static class ArmyPositions
    {
        public static Vector2Int[,] Get(
            Vector2Int enemiesAmount,
            Vector2Int center,
            Vector2Int distanceBetweenEnemies)
        {
            Vector2Int[,] result = new Vector2Int[enemiesAmount.x, enemiesAmount.y];
            Vector2Int startPoint = GetStartPoint(enemiesAmount, center, distanceBetweenEnemies);

            for (int i = 0; i < enemiesAmount.x; i++)
            {
                for (int j = 0; j < enemiesAmount.y; j++)
                {
                    int x = startPoint.x + distanceBetweenEnemies.x * (i - 1);
                    int y = startPoint.y + distanceBetweenEnemies.y * (j - 1);
                    result[i, j] = new Vector2Int(x, y);
                }
            }

            return result;
        }

        private static Vector2Int GetStartPoint(
            Vector2Int enemiesAmount,
            Vector2Int center,
            Vector2Int distanceBetweenEnemies)
        {
            int armyWidth = distanceBetweenEnemies.x * (enemiesAmount.x - 1);
            int armyHeight = distanceBetweenEnemies.y * (enemiesAmount.y - 1);

            int startX = center.x - armyWidth / 2;
            int startY = center.y - armyHeight / 2;

            return new Vector2Int(startX, startY);
        }
    }
}