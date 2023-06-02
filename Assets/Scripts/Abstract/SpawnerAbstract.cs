
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpawnerAbstract
{
    protected readonly List<IDisposable> _disposables = new List<IDisposable>();
    protected readonly TransformContainers _trContainers;
    public SpawnerAbstract(TransformContainers trContainers){
        _trContainers = trContainers;
    }
    protected void AddDisposable(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }
    protected void DespawnDisposable(IDisposable disposable)
    {
        if (_disposables.Any())
        {
            disposable.Dispose();
            _disposables.Remove(disposable);
        }
    }
}
