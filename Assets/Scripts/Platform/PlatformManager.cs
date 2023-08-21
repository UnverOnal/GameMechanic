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

        [Inject]
        public PlatformManager(SceneResources sceneResources, InputManager inputManager, PlayerData playerData)
        {
            _stacks = sceneResources.stacks;
            
            _inputManager = inputManager;
            _platformMover = new PlatformMover(_stacks, 1, playerData.startingPlatformDistance, playerData.platformMovementDuration);
            _meshCutter = new MeshCutter();

            CurrentPlatformCenter = _platformMover.CurrentStack.GetCenter();

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
                extras[i]?.AddComponent<Rigidbody>();

            currentPlatform.AddComponent<BoxCollider>();

            _platformMover.SetPrevious(currentPlatform);
        }

        public void Dispose()
        {
            _inputManager.OnTap -= OnTap;
        }
    }
}
