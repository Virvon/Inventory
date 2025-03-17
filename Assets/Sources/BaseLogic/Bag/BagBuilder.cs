using Assets.Sources.BaseLogic.Bag.Model;
using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using Assets.Sources.LoadingTree.ServiceLocator;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.SaveLoadData;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Bag
{
    public class BagBuilder
    {
        private const string BagPath = "Bag";
        private const string UiBagViewPath = "UiBagView";

        private readonly List<CellData> _cellsData = new()
        {
            new CellData(ItemType.Gun, 1),
            new CellData(ItemType.Pistol, 1),
            new CellData(ItemType.Tool, 1),
        };

        private readonly SharedBundle _sharedBundle;

        public BagBuilder(SharedBundle sharedBundle) =>
            _sharedBundle = sharedBundle;

        public void Create(List<ItemObject> items)
        {
            BagObject bagPrefab = Resources.Load<BagObject>(BagPath);
            UiBagView uiBagViewPrefab = Resources.Load<UiBagView>(UiBagViewPath);

            BagObject bagObject = Object.Instantiate(bagPrefab);
            UiBagView uiBagView = Object.Instantiate(uiBagViewPrefab);

            uiBagView.Initialize(ServiceLocator.Get<IInputService>());

            Model.Bag bagModel = new(_cellsData, items);

            BagViewController inventoryController = new(bagModel, bagObject.Get<BagView>());
            UiBagPresenter uiBagPresenter = new(bagModel, uiBagView, ServiceLocator.Get<IInputService>(), bagObject.Get<ClickResieverComponent>());

            bagObject.Get<ClickResieverComponent>().Initialize(ServiceLocator.Get<IInputService>(), _sharedBundle.Get<Camera>(SharedBundleKeys.Camera));

            BagCaretaker caretaker = new(bagModel, ServiceLocator.Get<ISaveLoadService>());

            _sharedBundle.Add(SharedBundleKeys.BagView, bagObject.Get<BagView>());
            _sharedBundle.Add(SharedBundleKeys.BagModel, bagModel);
        }
    }
}
