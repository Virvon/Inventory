﻿using Assets.Sources.BaseLogic.Item;

namespace Assets.Sources.BaseLogic.Bag.Model
{
    public struct CellData
    {
        public ItemType ItemType;
        public int Count;

        public CellData(ItemType itemType, int count)
        {
            ItemType = itemType;
            Count = count;
        }
    }
}
