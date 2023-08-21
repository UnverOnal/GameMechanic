using Platform;
using ScriptableObject;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerManager
    {
        private readonly GameObject _player;

        private bool _canMove = true;

        private readonly PlatformManager _platformManager;
        
        private readonly PlayerData _playerData;

        [Inject]
        public PlayerManager(SceneResources sceneResources, PlatformManager platformManager, PlayerData playerData)
        {
            _player = sceneResources.player;

            _platformManager = platformManager;
            
            _playerData = playerData;
        }

        public void Update()
        {
            if (!_canMove) return;

            Move();
        }

        private void Move()
        {
            var currentStackPosition = _platformManager.CurrentPlatformCenter;
            _player.transform.MoveForward(currentStackPosition,_playerData.playerSpeed);
            _player.transform.Look(currentStackPosition, _playerData.lookRotationFactor);
        }
    }
}