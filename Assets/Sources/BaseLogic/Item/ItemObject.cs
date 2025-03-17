using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    public class ItemObject : EntityObject
    {
        private ItemConfiguration _configuration;

        public ItemType Type => _configuration.Type;
        public string Name => _configuration.Name;

        public void Initialize(ItemConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SetPositionAndParent(Vector3 position, Transform parent = null)
        {
            transform.position = position;
            transform.parent = parent;
        }
    }
}
