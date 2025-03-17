using Assets.Sources.BaseLogic.Item.Components;
using Assets.Sources.Services.DisposeService;
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

        private readonly Dictionary<string, ItemConfiguration> _itemConfigurations;
        private readonly DisposeService _disposeService;

        public ItemBuilder(DisposeService disposeService)
        {
            _disposeService = disposeService;

            _itemConfigurations = Resources.LoadAll<ItemConfiguration>(ConfigurationsPath).ToDictionary(value => value.Identifier, value => value);
        }

        public IReadOnlyList<string> AllIdentifiers => _itemConfigurations.Keys.ToList();

        public ItemObject Create(string identifier)
        {
            if (_itemConfigurations.TryGetValue(identifier, out ItemConfiguration configuration) == false)
                throw new Exception();

            ItemObject item = Object.Instantiate(configuration.Prefab, configuration.StartPosition, Quaternion.identity);
            item.Initialize(configuration);

            Rigidbody rigidbody = item.GetComponent<Rigidbody>();
            ParentChangerComponent parentChanger = new(rigidbody, item, item.GetComponent<Collider>());

            PhysicalMovementComponent physicalMovementComponent = new(rigidbody);
            MovementControllerComponent movementController = new(parentChanger, physicalMovementComponent);

            item.Add(parentChanger);
            item.Add(physicalMovementComponent);
            item.Add(movementController);

            _disposeService.Register(movementController);

            return item;
        }
    }
}
