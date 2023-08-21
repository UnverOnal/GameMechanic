using Audio;
using Camera;
using GameResource;
using Player;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace GameManagement
{
    public class GameManager : ITickable
    {
        private readonly InputManager _inputManager;
        private readonly PlayerManager _playerManager;
        private readonly CameraManager _cameraManager;
        private readonly AudioManager _audioManager;

        [Inject]
        public GameManager(InputManager inputManager, PlayerManager playerManager,
            CameraManager cameraManager, AudioManager audioManager, SceneResources sceneResources)
        {
            _inputManager = inputManager;
            _playerManager = playerManager;
            _cameraManager = cameraManager;
            _audioManager = audioManager;

            var restartButton = sceneResources.resultUiResources.restartButton;
            restartButton.onClick.AddListener(LoadScene);
            var nextButton = sceneResources.resultUiResources.nextButton;
            nextButton.onClick.AddListener(LoadScene);
        }

        public void Tick()
        {
            _inputManager.Update();
            _playerManager.Update();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}