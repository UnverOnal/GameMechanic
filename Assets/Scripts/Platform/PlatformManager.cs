using System;
using UnityEngine;
using VContainer;

namespace Platform
{
    public class PlatformManager : IDisposable
    {
        public GameObject CurrentStack => _platformMover.CurrentStack;
        
        private readonly GameObject[] _stacks;

        private readonly PlatformMover _platformMover;
        private readonly InputManager _inputManager;
        private readonly MeshCutter _meshCutter;

        [Inject]
        public PlatformManager(SceneResources sceneResources, InputManager inputManager)
        {
            _stacks = sceneResources.stacks;
            
            _inputManager = inputManager;
            _platformMover = new PlatformMover(_stacks, 1, 10f);
            _meshCutter = new MeshCutter();

            _inputManager.OnTap += OnTap;
        }

        private void OnTap()
        {
            _platformMover.StopMoving();
            SlicePlatform();
            _platformMover.ActivateNext();
        }

        private void SlicePlatform()    
        {
            var currentStack = _platformMover.CurrentStack;
            var previousStack = _platformMover.PreviousStack;
            var extras = _meshCutter.Slice(previousStack, currentStack, out var platformLeft);
            for (int i = 0; i < extras.Length; i++)
                extras[i]?.AddComponent<Rigidbody>();

            _platformMover.SetPrevious(platformLeft);
        }

        public void Dispose()
        {
            _inputManager.OnTap -= OnTap;
        }
    }
}
