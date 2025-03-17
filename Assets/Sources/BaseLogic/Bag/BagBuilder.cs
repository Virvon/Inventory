using Assets.Sources.BaseLogic.Bag.Model;
using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.DisposeService;
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
        private readonly DisposeService _disposeService;
        private readonly IInputService _inputService;
        private readonly ISaveLoadService _saveLoadService;

        public BagBuilder(SharedBundle sharedBundle, DisposeService disposeService, IInputService inputService, ISaveLoadService saveLoadService)
        {
            _sharedBundle = sharedBundle;
            _disposeService = disposeService;
            _inputService = inputService;
            _saveLoadService = saveLoadService;
        }

        public void Create(List<ItemObject> items)
        {
            BagObject bagPrefab = Resources.Load<BagObject>(BagPath);
            UiBagView uiBagViewPrefab = Resources.Load<UiBagView>(UiBagViewPath);

            BagObject bagObject = Object.Instantiate(bagPrefab);
            UiBagView uiBagView = Object.Instantiate(uiBagViewPrefab);

            uiBagView.Initialize(_inputService);

            Model.Bag bagModel = new(_cellsData, items);

            BagViewController bagViewController = new(bagModel, bagObject.Get<BagView>());
            UiBagPresenter uiBagPresenter = new(bagModel, uiBagView, _inputService, bagObject.Get<ClickResieverComponent>());

            bagObject.Get<ClickResieverComponent>().Initialize(_inputService, _sharedBundle.Get<Camera>(SharedBundleKeys.Camera));

            BagCaretaker bagCaretaker = new(bagModel, _saveLoadService);

            _sharedBundle.Add(SharedBundleKeys.BagModel, bagModel);

            _disposeService.Register(bagViewController, uiBagPresenter, bagCaretaker);
        }
    }
}
