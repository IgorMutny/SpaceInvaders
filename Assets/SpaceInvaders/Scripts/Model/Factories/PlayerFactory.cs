using UnityEngine;

namespace SpaceInvaders
{
    public static class PlayerFactory
    {
        private static SpaceObjectCollection _collection;
        private static GameRules _rules;

        static PlayerFactory()
        {
            _collection = ServiceLocator.Get<SpaceObjectCollection>();
            _rules = ServiceLocator.Get<GameRules>();
        }

        public static Player Create()
        {
            Vector2Int position = _rules.PlayerPosition;
            Player player = _collection.AddObject<Player>(position);

            return player;
        }
    }
}