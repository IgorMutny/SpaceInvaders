namespace SpaceInvaders
{
    public class Game : IService
    {
        private GameRules _rules;
        private Level _currentLevel;
        private int _currentLevelIndex = 0;

        public Game()
        {
            _rules = ServiceLocator.Get<GameRules>();
        }

        public void Start()
        {
            SetLevel(_currentLevelIndex);
        }

        private void SetLevel(int index)
        {
            if (_currentLevel != null)
            {
                DestroyLevel(index);
            }

            _currentLevelIndex = index;
            _currentLevel = new Level(_rules.Levels[index]);
            _currentLevel.Victory += SetNextLevel;
            _currentLevel.Defeat += () => SetLevel(index);
        }

        private void DestroyLevel(int index)
        {
            _currentLevel.Destroy();
            _currentLevel.Victory -= SetNextLevel;
            _currentLevel.Defeat -= () => SetLevel(index);
            _currentLevel = null;
        }

        private void SetNextLevel()
        {
            _currentLevelIndex += 1;

            if (_currentLevelIndex >= _rules.Levels.Length)
            {
                _currentLevelIndex = 0;
            }

            SetLevel(_currentLevelIndex);
        }
    }
}