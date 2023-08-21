using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Result;
using UnityEngine;
using VContainer;

namespace Camera
{
    public class CameraManager
    {
        private readonly ResultManager _resultManager;
        
        private readonly CinemachineVirtualCamera _followCamera;
        private readonly CinemachineFreeLook _successCamera;

        private CancellationTokenSource _tokenSource;
        
        [Inject]
        public CameraManager(SceneResources sceneResources, ResultManager resultManager)
        {
            _resultManager = resultManager;

            var cameraResources = sceneResources.cameraResources;
            _followCamera = cameraResources.followCamera;
            _successCamera = cameraResources.successCamera;

            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _resultManager.SubscribeToSuccess(SetSuccessCamera);
        }

        public void Reset()
        {
            _resultManager.SubscribeToSuccess(SetSuccessCamera);
            _tokenSource.Cancel();
            SetFollowCamera();
        }

        private void SetSuccessCamera()
        {
            _successCamera.Priority = 20;
            _followCamera.Priority = 10;
            
            RotateSuccessCamera();
        }

        private void SetFollowCamera()
        {
            _followCamera.Priority = 20;
            _successCamera.Priority = 10;
        }

        private async void RotateSuccessCamera()
        {
            while (!_tokenSource.IsCancellationRequested)
            {
                _successCamera.m_XAxis.Value += Time.deltaTime * 40f;
                
                await UniTask.Yield();
            }
        }
    }
}
