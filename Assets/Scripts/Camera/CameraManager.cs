using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using GameResource;
using Result;
using UnityEngine;
using VContainer;

namespace Camera
{
    public class CameraManager
    {
        private readonly CinemachineVirtualCamera _followCamera;
        private readonly CinemachineFreeLook _successCamera;

        private readonly CancellationTokenSource _tokenSource;
        
        [Inject]
        public CameraManager(SceneResources sceneResources, ResultManager resultManager)
        {
            var cameraResources = sceneResources.cameraResources;
            _followCamera = cameraResources.followCamera;
            _successCamera = cameraResources.successCamera;

            _tokenSource = new CancellationTokenSource();
            
            resultManager.SubscribeToSuccess(SetSuccessCamera);
        }

        private void SetSuccessCamera()
        {
            _successCamera.Priority = 20;
            _followCamera.Priority = 10;
            
            RotateSuccessCamera();
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
