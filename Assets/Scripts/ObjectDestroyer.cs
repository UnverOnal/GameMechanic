using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

//We can't reuse the extra parts, so we must destroy them.
//Destroying gameObjects by batching in one frame is more efficient
//than the destroying them one by one in terms of garbage management.
public class ObjectDestroyer
{
    private readonly List<GameObject> _trashList = new();

    private readonly int _maximumTrashSize;
    private readonly float _delayForDestroyExtraParts;

    public ObjectDestroyer(int maximumTrashSize, float delayForDestroyExtraParts)
    {
        _maximumTrashSize = maximumTrashSize;
        _delayForDestroyExtraParts = delayForDestroyExtraParts;
    }

    public async void Trash(GameObject objectToTrash)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_delayForDestroyExtraParts));

        objectToTrash.SetActive(false);
        _trashList.Add(objectToTrash);

        if (_trashList.Count >= _maximumTrashSize)
        {
            for (var i = 0; i < _trashList.Count; i++)
                Object.Destroy(_trashList[i]);

            _trashList.Clear();
        }
    }
}