using System;
using GameManagement;
using GameResource;
using ScriptableObject;
using UnityEngine;
using Util;
using VContainer;

namespace Platform
{
    public class PlatformManager
    {
        public event Action OnPerfectTap;
        public event Action OnNormalTap;
        public Vector3 CurrentPlatformCenter { get; private set; }

        private readonly PlatformMover _platformMover;
        private readonly MeshCutter _meshCutter;
        private readonly ObjectDestroyer _objectDestroyer;

        private Vector3 _startingPosition;

        [Inject]
        public PlatformManager(SceneResources sceneResources, InputManager inputManager, PlatformData platformData)
        {
            var stacks = sceneResources.stacks;

            inputManager.OnTap += OnTap;
            
            _meshCutter = new MeshCutter();
            _objectDestroyer = new ObjectDestroyer(platformData.maximumTrashSize, platformData.delayForDestroyExtraParts);
            _platformMover = new PlatformMover(stacks, platformData.startingPlatformDistance,
                platformData.platformMovementDuration, platformData.perfectTapTolerance);

            CurrentPlatformCenter = _platformMover.CurrentStack.GetCenter();
        }

        private void OnTap()
        {
            _platformMover.StopMoving(out var isCloseEnough);
            var nextStack = _platformMover.NextStack;
            
            if (isCloseEnough)
                OnPerfectTap?.Invoke();
            else
            {
                SlicePlatform(out nextStack);
                OnNormalTap?.Invoke();
            }
            
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
    }
}
