using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Bag.View
{
    public class UiBagView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private IInputService _inputService;
        private UiCellView[] _cells;

        public event Action<ItemObject> ItemRemoved;

        private void Awake()
        {
            _cells = GetComponentsInChildren<UiCellView>();
        }

        public void Initialize(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Change(IEnumerable<ItemObject> items)
        {
            foreach (UiCellView cell in _cells)
            {
                foreach (ItemObject item in items)
                {
                    if (cell.ItemType == item.Type)
                        cell.TryChange(item);
                }
            }
        }

        public void Remove(ItemObject item)
        {
            foreach (UiCellView cell in _cells)
            {
                if (cell.Item == item)
                    cell.Remove();
            }
        }

        private void OnClickEnded(Vector2 position)
        {
            foreach (UiCellView cell in _cells)
            {
                if (cell.CheackHandleIntersection(position) == false)
                    continue;

                if (cell.Item == null)
                    return;

                ItemRemoved?.Invoke(cell.Item);

                return;
            }
        }

        public void Show()
        {
            _canvas.enabled = true;
            _inputService.ClickEnded += OnClickEnded;
        }

        public void Hide()
        {
            _canvas.enabled = false;
            _inputService.ClickEnded -= OnClickEnded;
        }
    }
}
