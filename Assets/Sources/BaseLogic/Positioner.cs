using Assets.Sources.BaseLogic.Bag;
using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic
{
    public class Positioner : IDisposable
    {
        private const float RaycastDistance = 100;

        private readonly Camera _camera;
        private readonly IInputService _inputService;
        private readonly BagView _bagView;

        private ItemObject _movementItem;

        public Positioner(Camera camera, IInputService inputService, BagView bagView)
        {
            _camera = camera;
            _inputService = inputService;
            _bagView = bagView;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.ClickEnded += OnClickEnded;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
        }

        private void OnClicked(Vector2 position)
        {
            if (Physics.Raycast(GetRay(position), out RaycastHit hitInfo, RaycastDistance)
                && hitInfo.transform.TryGetComponent(out ItemObject itemObject)
                && itemObject.Get<PhysicalMovementComponent>().CanMoved)
            {
                _movementItem = itemObject;
                _movementItem.Get<PhysicalMovementComponent>().SetGravity(false);
            }
        }

        private void OnDragged(Vector2 position)
        {
            if (_movementItem == null)
                return;

            Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));

            _movementItem.Get<PhysicalMovementComponent>().Move(worldPosition);
        }

        private void OnClickEnded(Vector2 position)
        {
            if (_movementItem == null)
                return;

            _movementItem.Get<PhysicalMovementComponent>().SetGravity(true);

            if (Physics.Raycast(GetRay(position), out RaycastHit hitInfo, RaycastDistance) && hitInfo.transform.TryGetComponent(out BagObject bag))
                bag.Get<BagView>().TryAdd(_movementItem);

            _movementItem = null;
        }

        private Ray GetRay(Vector2 position) =>
            _camera.ScreenPointToRay(position);
    }
}
