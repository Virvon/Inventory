using Assets.Sources.BaseLogic.Item;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class UiCellView : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;

        private TMP_Text _text;

        public ItemObject Item { get; private set; }

        public ItemType ItemType => _itemType;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void TryChange(ItemObject item)
        {
            if (Item == item)
                return;

            Item = item;

            _text.text = item.Name;
        }

        public void Remove()
        {
            Item = null;
            _text.text = string.Empty;
        }

        public bool CheackHandleIntersection(Vector2 handlePosition)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_text.rectTransform, handlePosition);
        }
    }
}
