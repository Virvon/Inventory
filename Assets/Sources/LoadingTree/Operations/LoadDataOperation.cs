using Assets.Sources.BaseLogic.Item;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.SaveLoadData;
using System.Collections.Generic;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadDataOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            BagData bagData = ServiceLocator.ServiceLocator.Get<ISaveLoadService>().TryLoad<BagData>();

            List<ItemType> inventoryItems = bagData == null ? new() : bagData.Items;

            bundle.Add(SharedBundleKeys.StartInventoryItems, inventoryItems);
        }
    }
}