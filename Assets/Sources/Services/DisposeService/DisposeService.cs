using System;
using System.Collections.Generic;

namespace Assets.Sources.Services.DisposeService
{
    public class DisposeService : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        public void Register(params IDisposable[] disposables) =>
            _disposables.AddRange(disposables);

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }
    }
}
