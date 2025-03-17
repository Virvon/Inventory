using Assets.Sources.BaseLogic.Inventory;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.LoadingTree.SharedDataBundle;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadBuildersOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            bundle.Add(SharedBundleKeys.InventoryBuilder, new BagBuilder(bundle));
            bundle.Add(SharedBundleKeys.ItemBuilder, new ItemBuilder());
        }
    }
}
