using Assets.Sources.BaseLogic.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class BagView : MonoBehaviour
    {
        private CellView[] _cells;

        public event Action<ItemObject> ItemAddTried;

        private void Awake() =>
            _cells = GetComponentsInChildren<CellView>();

        public void TryAdd(ItemObject item) =>
            ItemAddTried?.Invoke(item);

        public void Change(IReadOnlyList<ItemObject> items)
        {
            for(int i = 0; i < _cells.Length; i++)
            {
                for(int j = 0; j < items.Count; j++)
                {
                    if (_cells[i].ItemType != items[j].Type)
                        continue;

                    _cells[i].TryChange(items[j]);
                }
            }
        }

        public void Remove(ItemObject item)
        {
            foreach(CellView cell in _cells)
            {
                if (cell.Item == item)
                    cell.Remove();
            }
        }
    }
}
