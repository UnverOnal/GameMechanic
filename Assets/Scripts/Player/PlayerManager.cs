using System;
using GameResource;
using Platform;
using Result;
using ScriptableObject;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerManager : IDisposable
    {
        private readonly GameObject _player;

        private bool _canMove = true;
        
        private readonly Animator _animator;

        private readonly PlatformManager _platformManager;
        private readonly ResultManager _resultManager;

        private readonly PlayerData _playerData;
        private readonly int _dance = Animator.StringToHash("dance");

        [Inject]
        public PlayerManager(SceneResources sceneResources, PlatformManager platformManager, PlayerData playerData, ResultManager resultManager)
        {
            _player = sceneResources.player;
            _platformManager = platformManager;
            _playerData = playerData;
            _animator = _player.GetComponentInChildren<Animator>();

            _resultManager = resultManager;
            _resultManager.SubscribeToFail(OnFail);
            _resultManager.SubscribeToSuccess(OnSuccess);
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
            _animator.SetTrigger(_dance);
        }

        private void Move()
        {
            var currentStackPosition = _platformManager.CurrentPlatformCenter;
            _player.transform.MoveForward(currentStackPosition,_playerData.playerSpeed);
            _player.transform.Look(currentStackPosition, _playerData.lookRotationFactor);
        }

        public void Dispose()
        {
            _resultManager.UnSubscribeFromFail(OnFail);
            _resultManager.UnSubscribeFromSuccess(OnSuccess);
        }
    }
}