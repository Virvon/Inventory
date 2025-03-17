using Assets.Sources.InputService;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class ClickResieverComponent : MonoBehaviour
    {
        private const float RaycastDistance = 100;

        private IInputService _inputService;
        private Camera _camera;

        public event Action Clicked;

        public void Initialize(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;

            _inputService.Clicked += OnClicked;
        }

        private void OnDestroy()
        {
            _inputService.Clicked -= OnClicked;
        }

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistance)
                && hitInfo.transform.TryGetComponent(out ClickResieverComponent clickResiever)
                && clickResiever == this)
                Clicked?.Invoke();
        }
    }
}
