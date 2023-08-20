using Platform;
using Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private SceneResources sceneResources;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>();

            builder.RegisterComponent(sceneResources);

            builder.Register<PlatformManager>(Lifetime.Singleton);
            builder.Register<InputManager>(Lifetime.Singleton);
            builder.Register<PlayerManager>(Lifetime.Singleton);
        }
    }
}
