using Platform;
using Player;
using ScriptableObject;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private SceneResources sceneResources;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlatformData platformData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>();

            builder.RegisterComponent(sceneResources);
            builder.RegisterInstance(playerData);
            builder.RegisterInstance(platformData);

            builder.Register<PlatformManager>(Lifetime.Singleton);
            builder.Register<InputManager>(Lifetime.Singleton);
            builder.Register<PlayerManager>(Lifetime.Singleton);
        }
    }
}
