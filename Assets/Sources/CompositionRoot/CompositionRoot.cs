using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.Inventory;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using Assets.Sources.LoadingTree;
using Assets.Sources.LoadingTree.Piplines;
using Assets.Sources.LoadingTree.ServiceLocator;
using Assets.Sources.LoadingTree.SharedDataBundle;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        private readonly ItemType[] _items = new ItemType[]
        {
            ItemType.Gun,
            ItemType.Pistol,
            ItemType.Tool,
        };

        [SerializeField] private Camera _camera;

        private IInputService _inputService;
        private SharedBundle _sharedBundle;
        private List<ItemObject> _createdItems;

        private void Awake()
        {
            BagLoadingPipline gameLoadingPipline = new BagLoadingPipline();

            _sharedBundle = new();

            _sharedBundle.Add(SharedBundleKeys.Camera, _camera);

            GameLauncher gameLauncher = new(gameLoadingPipline.GetOperations());

            gameLauncher.Launch(_sharedBundle);

            CreateItems();
            CreateBag();
           

            Positioner positioner = new(_sharedBundle.Get<Camera>(SharedBundleKeys.Camera), ServiceLocator.Get<IInputService>(), _sharedBundle.Get<BagView>(SharedBundleKeys.BagView));

            _inputService = ServiceLocator.Get<IInputService>();

            //SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void Update()
        {
            _inputService.Update();
        }

        private void CreateItems()
        {
            _createdItems = new();

            ItemBuilder itemBuilder = _sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder);

            foreach (ItemType type in _items)
            {
                ItemObject item = itemBuilder.Create(type);

                _createdItems.Add(item);
            }
        } 
        
        private void CreateBag()
        {
            BagBuilder bagBuilder = _sharedBundle.Get<BagBuilder>(SharedBundleKeys.InventoryBuilder);
            List<ItemType> itemTypes = _sharedBundle.Get<List<ItemType>>(SharedBundleKeys.StartInventoryItems);
            List<ItemObject> bagItems = new();

            foreach(ItemType type in itemTypes)
            {
                if (_createdItems.Any(value => value.Type == type))
                    bagItems.Add(_createdItems.First(value => value.Type == type));
                else
                    bagItems.Add(_sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder).Create(type));
            }

            bagBuilder.Create(bagItems);
        }
    }
}
