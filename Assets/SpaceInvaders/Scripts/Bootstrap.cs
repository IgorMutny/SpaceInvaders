using UnityEngine;

namespace SpaceInvaders
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject _gameViewSample;
        [SerializeField] private GameRules _gameRules;

        public void Run(Canvas canvas)
        {
            Timer timer = GetComponent<Timer>();
            ServiceLocator.Register(timer);

            SpaceObjectCollection collection = new SpaceObjectCollection();
            ServiceLocator.Register(collection);

            ServiceLocator.Register(_gameRules);

            IInput input = GetComponent<KeyboardInput>();
            ServiceLocator.Register(input);

            Game game = new Game();
            ServiceLocator.Register(game);

            GameView gameView = Instantiate(_gameViewSample, canvas.transform)
                .GetComponent<GameView>();
            gameView.Initialize();
            gameView.SetSize(_gameRules.ScreenSize);

            game.Start();
        }
    }
}