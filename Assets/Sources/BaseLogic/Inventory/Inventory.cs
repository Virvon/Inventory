using Assets.Sources.BaseLogic.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class Inventory
    {
        private readonly IReadOnlyList<CellData> _cellsData;
        private readonly List<ItemObject> _items;

        public Inventory(List<CellData> cellsData)
        {
            _cellsData = cellsData;
            _items = new();
        }

        public event Action ItemAdded;
        public event Action<ItemObject> ItemRemoved;

        public IReadOnlyList<ItemObject> Items => _items;

        public void TryAdd(ItemObject item)
        {
            ItemType itemType = item.Type;

            int itemsCount = _items.Where(item => item.Type == itemType).Count();

            if(TryGetCellsCount(itemType, out int count) && itemsCount < count)
            {
                _items.Add(item);
                ItemAdded?.Invoke();
            }
        }

        public void Remove(ItemObject item)
        {
            if (_items.Contains(item) == false)
                throw new Exception();

            _items.Remove(item);
            ItemRemoved?.Invoke(item);
        }

        private bool TryGetCellsCount(ItemType itemType, out int count)
        {
            count = 0;

            if (_cellsData.Any(value => value.ItemType == itemType) == false)
                return false;

            CellData cellData = _cellsData.First(slot => slot.ItemType == itemType);

            count = cellData.Count;

            return true;
        }
    }
}
