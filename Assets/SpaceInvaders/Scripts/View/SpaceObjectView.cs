using UnityEngine;

namespace SpaceInvaders
{
    public class SpaceObjectView : MonoBehaviour
    {
        private IReadOnlySpaceObject _model;

        public void Initialize(IReadOnlySpaceObject model)
        {
            _model = model;
            SetPosition();

            _model.Moved += SetPosition;
        }

        private void SetPosition()
        {
            Vector3 position = new Vector3(_model.Position.x, _model.Position.y, 0);
            ((RectTransform)transform).localPosition = position;
        }

        private void OnDestroy()
        {
            _model.Moved -= SetPosition;
        }
    }
}