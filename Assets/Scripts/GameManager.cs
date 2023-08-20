using Platform;
using VContainer;
using VContainer.Unity;

public class GameManager : ITickable
{
    private readonly InputManager _inputManager;
    private PlatformManager _platformManager;
    
    [Inject]
    public GameManager(PlatformManager platformManager, InputManager inputManager)
    {
        _platformManager = platformManager;
        _inputManager = inputManager;
    }

    public void Tick()
    {
        _inputManager.Update();
    }
}