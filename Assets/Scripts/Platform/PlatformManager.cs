using System;
using GameResource;
using ScriptableObject;
using UnityEngine;
using Util;
using VContainer;

namespace Platform
{
    public class PlatformManager : IDisposable, IReloadable
    {
        public event Action OnPerfectTap;
        public Vector3 CurrentPlatformCenter { get; private set; }
        
        private readonly GameObject[] _stacks;

        private PlatformMover _platformMover;
        private readonly InputManager _inputManager;
        private readonly MeshCutter _meshCutter;
        private readonly ObjectDestroyer _objectDestroyer;

        private readonly PlatformData _platformData;

        [Inject]
        public PlatformManager(SceneResources sceneResources, InputManager inputManager, PlatformData platformData)
        {
            _stacks = sceneResources.stacks;
            _platformData = platformData;
            
            _inputManager = inputManager;
            _meshCutter = new MeshCutter();
            
            _objectDestroyer = new ObjectDestroyer(platformData.maximumTrashSize, platformData.delayForDestroyExtraParts);
        }
        
        public void Initialize()
        {
            _inputManager.OnTap += OnTap;
            
            CurrentPlatformCenter = _platformMover.CurrentStack.GetCenter();

            _platformMover = new PlatformMover(_stacks, 1, _platformData.startingPlatformDistance, _platformData.platformMovementDuration);
        }

        private void OnTap()
        {
            _platformMover.StopMoving(out var isCloseEnough);
            var nextStack = _platformMover.NextStack;
            
            if (isCloseEnough)
                OnPerfectTap?.Invoke();
            else
                SlicePlatform(out nextStack);
            
            CurrentPlatformCenter = nextStack.GetCenter();
            _platformMover.ActivateNext();
        }

        private void SlicePlatform(out GameObject nextStack)    
        {
            var currentStackOr = _platformMover.NextStack;
            var previousStack = _platformMover.CurrentStack;

            var extras = _meshCutter.Slice(previousStack, currentStackOr, out nextStack);
            for (int i = 0; i < extras.Length; i++)
            {
                var extra = extras[i];
                
                if(!extra) continue;
                
                extra.AddComponent<Rigidbody>();
                _objectDestroyer.Trash(extra);
            }

            nextStack.AddComponent<BoxCollider>();

            _platformMover.SetPrevious(nextStack);
        }

        public void Dispose()
        {
            _inputManager.OnTap -= OnTap;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
