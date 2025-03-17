using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Sources.BaseLogic.Item
{
    public class ItemBuilder
    {
        private readonly Dictionary<ItemType, ItemConfiguration> _itemConfigurations;

        public ItemBuilder()
        {
            _itemConfigurations = Resources.LoadAll<ItemConfiguration>("").ToDictionary(value => value.Type, value => value);
        }

        public void Create(ItemType type)
        {
            if (_itemConfigurations.TryGetValue(type, out ItemConfiguration configuration) == false)
                throw new Exception();

            ItemObject item = Object.Instantiate(configuration.Prefab, configuration.StartPosition, Quaternion.identity);
            item.Initialize(configuration);

            item.Add(new PhysicalMovementComponent(item.GetComponent<Rigidbody>()));
        }
    }
}
