using Assets.Sources.LoadingTree.Operations;
using System.Collections.Generic;

namespace Assets.Sources.LoadingTree.Piplines
{
    public class BagLoadingPipline
    {
        public IReadOnlyList<IOperation> GetOperations()
        {
            return new List<IOperation>
            {
                new LoadServicesOperation(),
                new LoadDataOperation(),
                new LoadBuildersOperation(),
            };
        }
    }
}
