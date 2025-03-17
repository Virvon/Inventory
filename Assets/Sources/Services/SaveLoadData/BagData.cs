using Assets.Sources.BaseLogic.Item;
using System;
using System.Collections.Generic;

namespace Assets.Sources.Services.SaveLoadData
{
    [Serializable]
    public class BagData : IMemento
    {
        public List<ItemType> Items;

        public BagData(List<ItemType> items)
        {
            Items = items;
        }

        public BagData GetBagData()
        {
            return this;
        }
    }
}
