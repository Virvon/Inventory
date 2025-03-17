using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemObject : EntityObject
    {
        private ItemConfiguration _configuration;

        public ItemType Type => _configuration.Type;
        public string Name => _configuration.Name;
        public Guid Identifire => _configuration.Identifier;

        private void Awake() =>
            Add(GetComponent<Rigidbody>());

        public void Initialize(ItemConfiguration configuration)
        {
            _configuration = configuration;

            Get<Rigidbody>().mass = _configuration.Weight;
        }
    }
}
