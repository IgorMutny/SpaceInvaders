using System;

namespace SpaceInvaders
{
    public class Level
    {
        private LevelInfo _info;
        private Player _player;
        private Army _army;
        private SpaceObjectCollection _collection;
        private bool _isFinished;

        public event Action Victory;
        public event Action Defeat;

        public Level(LevelInfo info)
        {
            _info = info;

            _army = new Army(_info);
            _army.AllEnemiesDestroyed += InvokeVictory;

            _player = PlayerFactory.Create();
            _player.Destroyed += InvokeDefeat;

            _collection = ServiceLocator.Get<SpaceObjectCollection>();
            _isFinished = false;
        }

        public void Destroy()
        {
            _army.AllEnemiesDestroyed -= InvokeVictory;
            _player.Destroyed -= InvokeDefeat;

            _army.Destroy();
            _army = null;

            _collection.Clear();
        }

        private void InvokeVictory()
        {
            if (_isFinished == false)
            {
                Victory?.Invoke();
                _isFinished = true;
            }
        }

        private void InvokeDefeat()
        {
            if (_isFinished == false)
            {
                Defeat?.Invoke();
                _isFinished = true;
            }
        }
    }
}