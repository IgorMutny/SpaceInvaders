using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private ObjectViewsMap _viewMap;
        [SerializeField] private Transform _zeroPoint;

        private Dictionary<IReadOnlySpaceObject, SpaceObjectView> _objectViews
            = new Dictionary<IReadOnlySpaceObject, SpaceObjectView>();

        private IReadOnlySpaceObjectCollection _collection;

        public void Initialize()
        {
            _collection = ServiceLocator.Get<SpaceObjectCollection>();

            _collection.ObjectCreated += OnObjectCreated;
            _collection.ObjectDestroyed += OnObjectDestroyed;
        }

        private void OnDestroy()
        {
            _collection.ObjectCreated -= OnObjectCreated;
            _collection.ObjectDestroyed -= OnObjectDestroyed;
        }

        public void SetSize(Vector2Int size)
        {
            ((RectTransform)transform).sizeDelta = size;
        }

        public void OnObjectCreated(IReadOnlySpaceObject spaceObject)
        {
            GameObject sample = _viewMap.Get(spaceObject.Tag);
            SpaceObjectView objectView =
                Instantiate(sample, _zeroPoint).GetComponent<SpaceObjectView>();

            objectView.Initialize(spaceObject);
            _objectViews.Add(spaceObject, objectView);
        }

        public void OnObjectDestroyed(IReadOnlySpaceObject spaceObject)
        {
            SpaceObjectView objectView = _objectViews[spaceObject];
            Destroy(objectView.gameObject);
            _objectViews.Remove(spaceObject);
        }
    }
}