using System;
using ScriptableObject;
using UnityEngine;
using Util;
using VContainer;

namespace Platform
{
    public class PlatformManager : IDisposable
    {
        public Vector3 CurrentPlatformCenter { get; private set; }
        
        private readonly GameObject[] _stacks;

        private readonly PlatformMover _platformMover;
        private readonly InputManager _inputManager;
        private readonly MeshCutter _meshCutter;

        private readonly ObjectDestroyer _objectDestroyer;

        [Inject]
        public PlatformManager(SceneResources sceneResources, InputManager inputManager, PlatformData platformData)
        {
            _stacks = sceneResources.stacks;
            
            _inputManager = inputManager;
            _platformMover = new PlatformMover(_stacks, 1, platformData.startingPlatformDistance, platformData.platformMovementDuration);
            _meshCutter = new MeshCutter();

            CurrentPlatformCenter = _platformMover.CurrentStack.GetCenter();

            _objectDestroyer = new ObjectDestroyer(platformData.maximumTrashSize, platformData.delayForDestroyExtraParts);

            _inputManager.OnTap += OnTap;
        }

        private void OnTap()
        {
            _platformMover.StopMoving();
            SlicePlatform(out var currentPlatform);
            CurrentPlatformCenter = currentPlatform.GetCenter();
            _platformMover.ActivateNext();
        }

        private void SlicePlatform(out GameObject currentPlatform)    
        {
            var currentStack = _platformMover.NextStack;
            var previousStack = _platformMover.CurrentStack;
            var extras = _meshCutter.Slice(previousStack, currentStack, out currentPlatform);
            for (int i = 0; i < extras.Length; i++)
            {
                var extra = extras[i];
                
                if(!extra) continue;
                
                extra.AddComponent<Rigidbody>();
                _objectDestroyer.Trash(extra);
            }

            currentPlatform.AddComponent<BoxCollider>();

            _platformMover.SetPrevious(currentPlatform);
        }

        public void Dispose()
        {
            _inputManager.OnTap -= OnTap;
        }
    }
}
