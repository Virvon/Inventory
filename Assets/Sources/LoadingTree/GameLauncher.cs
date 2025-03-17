using Assets.Sources.LoadingTree.SharedDataBundle;
using System.Collections.Generic;

namespace Assets.Sources.LoadingTree
{
    public class GameLauncher
    {
        private readonly IReadOnlyList<IOperation> _operations;

        public GameLauncher(IReadOnlyList<IOperation> operations)
        {
            _operations = operations;
        }

        public void Launch(SharedBundle bundle)
        {
            foreach(IOperation operation in _operations)
                operation.Run(bundle);
        }
    }
}
