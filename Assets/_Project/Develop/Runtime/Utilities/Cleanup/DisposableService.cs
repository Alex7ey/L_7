using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.Cleanup
{
    public class DisposableService : IDisposable
    {
        private List<IDisposable> _disposables = new();

        public IReadOnlyList<IDisposable> Disposables => _disposables;

        public void Add(IDisposable disposable) => _disposables.Add(disposable);

        public void Dispose()
        {
            foreach(IDisposable disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}
