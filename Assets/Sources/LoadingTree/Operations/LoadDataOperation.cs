using Assets.Sources.BaseLogic.Item;
using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.SaveLoadData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadDataOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            BagData bagData = ServiceLocator.ServiceLocator.Get<ISaveLoadService>().TryLoad<BagData>();

            Guid[] itemIdentifiers = bagData == null ? new Guid[0] : bagData.ItemIdentifiers.Select(value => new Guid(value)).ToArray();

            bundle.Add(SharedBundleKeys.StartInventoryItems, itemIdentifiers);
        }
    }
}