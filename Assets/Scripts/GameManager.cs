using Platform;
using Player;
using VContainer;
using VContainer.Unity;

public class GameManager : ITickable
{
    private readonly InputManager _inputManager;
    private readonly PlayerManager _playerManager;
    private PlatformManager _platformManager;
    
    [Inject]
    public GameManager(PlatformManager platformManager, InputManager inputManager, PlayerManager playerManager)
    {
        _platformManager = platformManager;
        _inputManager = inputManager;
        _playerManager = playerManager;
    }

    public void Tick()
    {
        _inputManager.Update();
        _playerManager.Update();
    }
}