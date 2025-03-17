using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    public class ItemObject : EntityObject
    {
        private ItemConfiguration _configuration;

        public ItemType Type => _configuration.Type;
        public string Name => _configuration.Name;
        public Guid Identifire => _configuration.Identifier;

        public void Initialize(ItemConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
