using Platform;
using VContainer;
using VContainer.Unity;

public class GameManager : IInitializable, ITickable
{
    private PlatformManager _platformManager;
    
    [Inject]
    public GameManager(PlatformManager platformManager)
    {
        _platformManager = platformManager;
    }
    
    public void Initialize()
    {
    }

    public void Tick()
    {
    }
}