using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.Bag;
using Assets.Sources.BaseLogic.Bag.Model;
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
    public class CompositionRoot : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Camera _camera;

        private IInputService _inputService;
        
        private List<ItemObject> _createdItems;

        private void Awake()
        {
            SharedBundle sharedBundle = new();

            LaunchGame(sharedBundle);
            CreateScene(sharedBundle);
        }

        private void Update()
        {
            if(_inputService != null)
                _inputService.Update();
        }

        private void OnDestroy() =>
            ServiceLocator.Get<DisposeService>().Dispose();

        private void LaunchGame(SharedBundle sharedBundle)
        {
            BagLoadingPipline gameLoadingPipline = new();

            sharedBundle.Add(SharedBundleKeys.Camera, _camera);
            sharedBundle.Add(SharedBundleKeys.CoroutineRunner, this);

            GameLauncher gameLauncher = new(gameLoadingPipline.GetOperations());

            gameLauncher.Launch(sharedBundle);
        }

        private void CreateScene(SharedBundle sharedBundle)
        {
            CreateItems(sharedBundle);
            CreateBag(sharedBundle);

            ItemPositioner positioner = new(
                sharedBundle.Get<Camera>(SharedBundleKeys.Camera),
                ServiceLocator.Get<IInputService>());

            BagPostRequestCreator bagPostRequestCreator = new(
                sharedBundle.Get<Bag>(SharedBundleKeys.BagModel),
                sharedBundle.Get<ICoroutineRunner>(SharedBundleKeys.CoroutineRunner));

            _inputService = ServiceLocator.Get<IInputService>();

            ServiceLocator.Get<DisposeService>().Register(positioner, bagPostRequestCreator);
        }

        private void CreateItems(SharedBundle sharedBundle)
        {
            _createdItems = new();

            ItemBuilder itemBuilder = sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder);

            foreach (string identifier in itemBuilder.AllIdentifiers)
            {
                ItemObject item = itemBuilder.Create(identifier);

                _createdItems.Add(item);
            }
        } 
        
        private void CreateBag(SharedBundle sharedBundle)
        {
            BagBuilder bagBuilder = sharedBundle.Get<BagBuilder>(SharedBundleKeys.BagBuidler);
            string[] itemIdentifiers = sharedBundle.Get<string[]>(SharedBundleKeys.StartInventoryItems);
            List<ItemObject> bagItems = new();

            foreach(string identifier in itemIdentifiers)
            {
                if (_createdItems.Any(value => value.Identifire == identifier))
                    bagItems.Add(_createdItems.First(value => value.Identifire == identifier));
                else
                    bagItems.Add(sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder).Create(identifier));
            }

            bagBuilder.Create(bagItems);
        }
    }
}
