using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.BaseLogic.Item.Components;
using Assets.Sources.InputService;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic
{
    public class ItemPositioner : IDisposable
    {
        private const float RaycastDistance = 100;
        private const int ItemDistance = 3;

        private readonly Camera _camera;
        private readonly IInputService _inputService;

        private ItemObject _movementItem;

        public ItemPositioner(Camera camera, IInputService inputService)
        {
            _camera = camera;
            _inputService = inputService;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.ClickEnded += OnClickEnded;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Dragged -= OnDragged;
            _inputService.ClickEnded -= OnClickEnded;
        }

        private void OnClicked(Vector2 position)
        {
            if (Physics.Raycast(GetRay(position), out RaycastHit hitInfo, RaycastDistance)
                && hitInfo.transform.TryGetComponent(out ItemObject itemObject)
                && itemObject.Get<PhysicalMovementComponent>().Fixed == false)
            {
                _movementItem = itemObject;
                _movementItem.Get<PhysicalMovementComponent>().SetGravity(false);
            }
        }

        private void OnDragged(Vector2 position)
        {
            if (_movementItem == null)
                return;

            Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(position.x, position.y, ItemDistance));

            _movementItem.Get<PhysicalMovementComponent>().Move(worldPosition);
        }

        private void OnClickEnded(Vector2 position)
        {
            if (_movementItem == null)
                return;

            _movementItem.Get<PhysicalMovementComponent>().SetGravity(true);

            RaycastHit[] hits = Physics.RaycastAll(GetRay(position), RaycastDistance);

            foreach(RaycastHit hit in hits)
            {
                if(hit.transform.TryGetComponent(out Bag.BagObject bag))
                {
                    bag.Get<BagView>().TryAdd(_movementItem);

                    break;
                }
            }                

            _movementItem = null;
        }

        private Ray GetRay(Vector2 position) =>
            _camera.ScreenPointToRay(position);
    }
}
