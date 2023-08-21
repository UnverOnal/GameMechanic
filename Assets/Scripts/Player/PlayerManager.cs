using GameResource;
using Platform;
using Result;
using ScriptableObject;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerManager : IReloadable
    {
        public Vector3 Position => _player.transform.position;
        
        public string Tag => _player.gameObject.tag;
        
        private readonly GameObject _player;

        private bool _canMove = true;
        
        private readonly Animator _animator;

        private readonly PlatformManager _platformManager;
        private readonly ResultManager _resultManager;
        
        private readonly PlayerData _playerData;

        [Inject]
        public PlayerManager(SceneResources sceneResources, PlatformManager platformManager, PlayerData playerData, ResultManager resultManager)
        {
            _player = sceneResources.player;

            _platformManager = platformManager;
            
            _playerData = playerData;

            _resultManager = resultManager;

            _animator = _player.GetComponentInChildren<Animator>();
        }

        public void Initialize()
        {
            _resultManager.SubscribeToFail(OnFail);
            _resultManager.SubscribeToSuccess(OnSuccess);
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            if (!_canMove) return;

            Move();
        }

        private void OnFail()
        {
            _canMove = false;
        }
        private void OnSuccess()
        {
            _canMove = false;
            _animator.SetTrigger("dance");
        }

        private void Move()
        {
            var currentStackPosition = _platformManager.CurrentPlatformCenter;
            _player.transform.MoveForward(currentStackPosition,_playerData.playerSpeed);
            _player.transform.Look(currentStackPosition, _playerData.lookRotationFactor);
        }
    }
}