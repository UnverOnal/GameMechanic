using Platform;
using Player;
using Result;
using VContainer;
using VContainer.Unity;

public class GameManager : ITickable
{
    private readonly InputManager _inputManager;
    private readonly PlayerManager _playerManager;
    private PlatformManager _platformManager;
    private ResultManager _resultManager;
    
    [Inject]
    public GameManager(PlatformManager platformManager, InputManager inputManager, PlayerManager playerManager, ResultManager resultManager)
    {
        _platformManager = platformManager;
        _inputManager = inputManager;
        _playerManager = playerManager;
        
        _resultManager = resultManager;

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
    }
}