using Assets.Sources.LoadingTree.SharedDataBundle;
using Assets.Sources.Services.SaveLoadData;
using System.Linq;

namespace Assets.Sources.LoadingTree.Operations
{
    public class LoadDataOperation : IOperation
    {
        public void Run(SharedBundle bundle)
        {
            BagData bagData = bundle.Get<ISaveLoadService>(SharedBundleKeys.SaveLoadService).TryLoad<BagData>();

            string[] itemIdentifiers = bagData == null ? new string[0] : bagData.ItemIdentifiers.Select(value => new string(value)).ToArray();

            bundle.Add(SharedBundleKeys.StartInventoryItems, itemIdentifiers);
        }
    }
}