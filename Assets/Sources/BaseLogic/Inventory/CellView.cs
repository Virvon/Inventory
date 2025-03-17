using Assets.Sources.BaseLogic.Item;
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

        public void TryChange(ItemObject item)
        {
            if (Item == item)
                return;

            Item = item;

            Debug.LogWarning("Need changing");
            Item.SetPositionAndParent(_position, transform);
            Item.GetComponent<Rigidbody>().isKinematic = true;
        }

        public void Remove() 
        {
            Item.SetPositionAndParent(Item.transform.position);
            Item.GetComponent<Rigidbody>().isKinematic = false;
            Item = null;
        }
    }
}
