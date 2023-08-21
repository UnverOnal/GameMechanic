using Camera;
using Platform;
using Player;
using Result;
using VContainer;
using VContainer.Unity;

public class GameManager : ITickable
{
    private readonly InputManager _inputManager;
    private readonly PlayerManager _playerManager;
    private readonly ResultManager _resultManager;
    private readonly CameraManager _cameraManager;
    
    [Inject]
    public GameManager(InputManager inputManager, PlayerManager playerManager, ResultManager resultManager, CameraManager cameraManager)
    {
        _inputManager = inputManager;
        _playerManager = playerManager;
        _resultManager = resultManager;
        _cameraManager = cameraManager;

        NextScene();
    }

    public void Tick()
    {
        _inputManager.Update();
        _playerManager.Update();
    }

    private void NextScene()
    {
        _resultManager.Initialize(_playerManager.Tag);
        _playerManager.Initialize();
        _cameraManager.Initialize();
    }
}