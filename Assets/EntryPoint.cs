using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private SpaceInvaders.Bootstrap _spaceInvadersBootstrap;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        _spaceInvadersBootstrap.Run(_canvas);
    }
}
