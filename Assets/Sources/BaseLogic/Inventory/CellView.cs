﻿using Assets.Sources.BaseLogic.Item;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;

        private Vector3 _position;

        public ItemObject Item { get; private set; }

        public ItemType ItemType => _itemType;

        private void Awake()
        {
            _position = GetComponent<Transform>().position;
        }

        public void Initialize(ItemObject item)
        {
            Item = item;

            Item.Get<ParentChangerComponent>().Set(transform, true);
        }

        public void TryChange(ItemObject item)
        {
            if (Item == item)
                return;

            Item = item;

            Item.Get<ParentChangerComponent>().Set(transform);
        }

        public void Remove() 
        {
            Item.Get<ParentChangerComponent>().Reset();
            Item = null;
        }
    }
}
