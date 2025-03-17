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
            ServiceLocator.ServiceLocator.Resgister<IInputService>(new InputService.InputService());
            ServiceLocator.ServiceLocator.Resgister<ISaveLoadService>(new SaveLoadService());
            ServiceLocator.ServiceLocator.Resgister(new DisposeService());
        }
    }
}