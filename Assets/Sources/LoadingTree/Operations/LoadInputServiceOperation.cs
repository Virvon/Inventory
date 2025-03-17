using Assets.Sources.InputService;
using Assets.Sources.LoadingTree.SharedDataBundle;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadInputServiceOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            ServiceLocator.ServiceLocator.Resgister<IInputService>(new InputService.InputService());
        }
    }
}