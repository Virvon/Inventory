using Assets.Sources.BaseLogic.Bag;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.DisposeService;
using Assets.Sources.Services.SaveLoadData;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadBuildersOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            bundle.Add(SharedBundleKeys.BagBuidler, new BagBuilder(
                bundle,
                ServiceLocator.ServiceLocator.Get<DisposeService>(),
                ServiceLocator.ServiceLocator.Get<IInputService>(),
                ServiceLocator.ServiceLocator.Get<ISaveLoadService>()));

            bundle.Add(SharedBundleKeys.ItemBuilder, new ItemBuilder());
        }
    }
}
