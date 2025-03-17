using Assets.Sources.InputService;
using Assets.Sources.LoadingTree.ServiceLocator;
using Assets.Sources.LoadingTree.SharedDataBundle;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class BagBuilder
    {
        private const string BagPath = "Bag";
        private const string UiBagViewPath = "UiBagView";

        private readonly SharedBundle _sharedBundle;

        public BagBuilder(SharedBundle sharedBundle)
        {
            _sharedBundle = sharedBundle;
        }

        public void Create()
        {
            Bag bagPrefab = Resources.Load<Bag>(BagPath);
            UiBagView uiBagViewPrefab = Resources.Load<UiBagView>(UiBagViewPath);

            Bag bag = Object.Instantiate(bagPrefab);
            UiBagView uiBagView = Object.Instantiate(uiBagViewPrefab);

            uiBagView.Initialize(ServiceLocator.Get<IInputService>());

            Inventory inventory = new(new List<CellData>
            {
                new CellData(Item.ItemType.Gun, 1),
                new CellData(Item.ItemType.Pistol, 1),
                new CellData(Item.ItemType.Tool, 1),
            });

            BagViewController inventoryController = new(inventory, bag.Get<BagView>());
            UiBagPresenter uiBagPresenter = new(inventory, uiBagView, ServiceLocator.Get<IInputService>(), bag.Get<ClickResieverComponent>());

            bag.Get<ClickResieverComponent>().Initialize(ServiceLocator.Get<IInputService>(), _sharedBundle.Get<Camera>(SharedBundleKeys.Camera));

            _sharedBundle.Add(SharedBundleKeys.BagView, bag.Get<BagView>());
        }
    }
}
