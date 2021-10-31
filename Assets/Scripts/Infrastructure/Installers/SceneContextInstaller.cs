using LevelLogic.UI;
using LevelLogic.Zones;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private LevelFinisherView _levelFinisherView;
        [SerializeField] private FinishZone _finishZone;
        [SerializeField] private LevelProgressView _levelProgressView;
        [SerializeField] private StartGameView startGameView;

        public override void InstallBindings()
        {
            Container.BindInstance(_player).AsSingle();
            _player.Initialize();
            
            Container.BindInstance(_levelFinisherView).AsSingle();
            Container.BindInstance(_finishZone).AsSingle();
            Container.BindInstance(_levelProgressView).AsSingle();
            Container.BindInstance(startGameView).AsSingle();
        }
    }
}