using UnityEngine;
using VContainer;

namespace Platform
{
    public class PlatformManager
    {
        public GameObject CurrentStack { get; private set; }
        private GameObject _previousStack;
        
        private readonly GameObject[] _stacks;

        private readonly PlatformMover _platformMover;

        [Inject]
        public PlatformManager(SceneResources sceneResources)
        {
            _stacks = sceneResources.stacks;

            _previousStack = _stacks[0];
            CurrentStack = _stacks[1];

            _platformMover = new PlatformMover(_stacks, 1, 10f);
        }
    }
}
