using Assets.Sources.BaseLogic.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Bag.View
{
    public class BagView : MonoBehaviour
    {
        private CellView[] _cells;

        public event Action<ItemObject> ItemAddTried;

        private void Awake() =>
            _cells = GetComponentsInChildren<CellView>();

        public void Initialize(IReadOnlyList<ItemObject> items)
        {
            foreach (CellView cell in _cells)
            {
                foreach (ItemObject item in items)
                {
                    if (cell.ItemType != item.Type)
                        continue;

                    cell.Initialize(item);
                }
            }
        }

        public void TryAdd(ItemObject item) =>
            ItemAddTried?.Invoke(item);

        public void Change(IReadOnlyList<ItemObject> items)
        {
            foreach (CellView cell in _cells)
            {
                foreach (ItemObject item in items)
                {
                    if (cell.ItemType != item.Type)
                        continue;

                    cell.TryChange(item);
                }
            }
        }

        public void Remove(ItemObject item)
        {
            foreach (CellView cell in _cells)
            {
                if (cell.Item == item)
                    cell.Remove();
            }
        }
    }
}
