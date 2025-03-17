using Assets.Sources.LoadingTree.Operations;
using System.Collections.Generic;

namespace Assets.Sources.LoadingTree.Piplines
{
    public class GameLoadingPipline
    {
        public IReadOnlyList<IOperation> GetOperations()
        {
            return new List<IOperation>
            {
                new LoadInputServiceOperation(),
                new LoadBuildersOperation(),
            };
        }
    }
}
