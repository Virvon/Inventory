using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.Inventory;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using Assets.Sources.LoadingTree;
using Assets.Sources.LoadingTree.Piplines;
using Assets.Sources.LoadingTree.ServiceLocator;
using Assets.Sources.LoadingTree.SharedDataBundle;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private IInputService _inputService;

        private void Awake()
        {
            GameLoadingPipline gameLoadingPipline = new GameLoadingPipline();

            SharedBundle sharedBundle = new();

            sharedBundle.Add(SharedBundleKeys.Camera, _camera);

            GameLauncher gameLauncher = new(gameLoadingPipline.GetOperations());

            gameLauncher.Launch(sharedBundle);

            sharedBundle.Get<BagBuilder>(SharedBundleKeys.InventoryBuilder).Create();
            ItemBuilder itemBuilder = sharedBundle.Get<ItemBuilder>(SharedBundleKeys.ItemBuilder);

            itemBuilder.Create(ItemType.Gun);
            itemBuilder.Create(ItemType.Pistol);
            itemBuilder.Create(ItemType.Tool);

            Positioner positioner = new(sharedBundle.Get<Camera>(SharedBundleKeys.Camera), ServiceLocator.Get<IInputService>(), sharedBundle.Get<BagView>(SharedBundleKeys.BagView));
            _inputService = ServiceLocator.Get<IInputService>();

            //SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void Update()
        {
            _inputService.Update();
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            throw new NotImplementedException();
        }      
    }
}
