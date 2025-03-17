using Assets.Sources.BaseLogic.Bag;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.DisposeService;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadBuildersOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            bundle.Add(SharedBundleKeys.BagBuidler, new BagBuilder(bundle));
            bundle.Add(SharedBundleKeys.ItemBuilder, new ItemBuilder(bundle.Get<DisposeService>(SharedBundleKeys.DisposeService)));
        }
    }
}
