using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Sources.BaseLogic.Item
{
    public class ItemBuilder
    {
        private const string ConfigurationsPath = "Configurations";

        private readonly Dictionary<Guid, ItemConfiguration> _itemConfigurations;

        public ItemBuilder()
        {
            _itemConfigurations = Resources.LoadAll<ItemConfiguration>(ConfigurationsPath).ToDictionary(value => value.Identifier, value => value);
        }

        public IReadOnlyList<Guid> AllIdentifiers => _itemConfigurations.Keys.ToList();

        public ItemObject Create(Guid identifier)
        {
            if (_itemConfigurations.TryGetValue(identifier, out ItemConfiguration configuration) == false)
                throw new Exception();

            ItemObject item = Object.Instantiate(configuration.Prefab, configuration.StartPosition, Quaternion.identity);
            item.Initialize(configuration);

            Rigidbody rigidbody = item.GetComponent<Rigidbody>();
            ParentChangerComponent parentChanger = new(rigidbody, item, item.GetComponent<Collider>());

            item.Add(parentChanger);
            item.Add(new PhysicalMovementComponent(rigidbody, parentChanger));

            return item;
        }
    }
}
