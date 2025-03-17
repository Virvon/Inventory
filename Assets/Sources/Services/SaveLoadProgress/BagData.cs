using Assets.Sources.BaseLogic.Item;
using System;
using System.Collections.Generic;

namespace Assets.Sources.Services.SaveLoadProgress
{
    [Serializable]
    public class BagData
    {
        public List<ItemType> _items;

        public BagData(List<ItemType> items)
        {
            _items = items;
        }
    }
}
