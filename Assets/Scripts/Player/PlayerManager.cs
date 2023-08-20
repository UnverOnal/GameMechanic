using Platform;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerManager
    {
        private readonly GameObject _player;

        private bool _canMove = true;

        private readonly PlatformManager _platformManager;

        [Inject]
        public PlayerManager(SceneResources sceneResources, PlatformManager platformManager)
        {
            _player = sceneResources.player;

            _platformManager = platformManager;
        }

        public void Update()
        {
            if (!_canMove) return;

            Move();
        }

        private void Move()
        {
            var currentStackPosition = _platformManager.CurrentStack.transform.position;
            _player.transform.MoveForward(currentStackPosition,1f);
            _player.transform.Look(currentStackPosition, 3f);
        }
    }
}