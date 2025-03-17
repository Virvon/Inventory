using Assets.Sources.InputService;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.DisposeService;
using Assets.Sources.Services.SaveLoadData;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadServicesOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            IInputService inputService = new InputService.InputService();
            ISaveLoadService saveLoadService = new SaveLoadService();
            DisposeService disposeService = new();

            ServiceLocator.ServiceLocator.Resgister(inputService);
            ServiceLocator.ServiceLocator.Resgister(saveLoadService);
            ServiceLocator.ServiceLocator.Resgister(disposeService);

            bundle.Add(SharedBundleKeys.InputService, inputService);
            bundle.Add(SharedBundleKeys.SaveLoadService, saveLoadService);
            bundle.Add(SharedBundleKeys.DisposeService, disposeService);
        }
    }
}