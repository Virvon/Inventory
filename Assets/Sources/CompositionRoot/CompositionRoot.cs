using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.Bag;
using Assets.Sources.BaseLogic.Bag.Model;
using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using Assets.Sources.LoadingTree;
using Assets.Sources.LoadingTree.Piplines;
using Assets.Sources.LoadingTree.ServiceLocator;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.CoroutineRunner;
using Assets.Sources.Services.DisposeService;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        private IInputService _inputService;
        private SharedBundle _sharedBundle;
        private List<ItemObject> _createdItems;

        private void Awake()
        {
            LaunchGame();
            CreateScene();
        }

        private void Update()
        {
            if(_inputService != null)
                _inputService.Update();
        }

        private void OnDestroy() =>
            ServiceLocator.Get<DisposeService>().Dispose();

        private void LaunchGame()
        {
            BagLoadingPipline gameLoadingPipline = new BagLoadingPipline();

            _sharedBundle = new();

            _sharedBundle.Add(SharedBundleKeys.Camera, _camera);
            _sharedBundle.Add(SharedBundleKeys.CoroutineRunner, _coroutineRunner);

            GameLauncher gameLauncher = new(gameLoadingPipline.GetOperations());

            gameLauncher.Launch(_sharedBundle);
        }

        private void CreateScene()
        {
            CreateItems();
            CreateBag();

            ItemPositioner positioner = new(_sharedBundle.Get<Camera>(SharedBundleKeys.Camera), ServiceLocator.Get<IInputService>());
            BagPostRequestCreator bagPostRequestCreator = new(_sharedBundle.Get<Bag>(SharedBundleKeys.BagModel), _sharedBundle.Get<ICoroutineRunner>(SharedBundleKeys.CoroutineRunner));

            _inputService = ServiceLocator.Get<IInputService>();

            ServiceLocator.Get<DisposeService>().Register(positioner, bagPostRequestCreator);
        }

        private void CreateItems()
        {
            _createdItems = new();

            ItemBuilder itemBuilder = _sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder);

            foreach (Guid identifier in itemBuilder.AllIdentifiers)
            {
                ItemObject item = itemBuilder.Create(identifier);

                _createdItems.Add(item);
            }
        } 
        
        private void CreateBag()
        {
            BagBuilder bagBuilder = _sharedBundle.Get<BagBuilder>(SharedBundleKeys.BagBuidler);
            Guid[] itemIdentifiers = _sharedBundle.Get<Guid[]>(SharedBundleKeys.StartInventoryItems);
            List<ItemObject> bagItems = new();

            foreach(Guid identifier in itemIdentifiers)
            {
                if (_createdItems.Any(value => value.Identifire == identifier))
                    bagItems.Add(_createdItems.First(value => value.Identifire == identifier));
                else
                    bagItems.Add(_sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder).Create(identifier));
            }

            bagBuilder.Create(bagItems);
        }
    }
}
