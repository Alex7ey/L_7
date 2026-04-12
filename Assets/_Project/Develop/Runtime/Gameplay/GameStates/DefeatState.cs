using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.GameStates
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly PlayerStatisticsService _playerStatisticsService;

        public DefeatState(
            IInputService inputService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            PlayerDataProvider playerDataProvider,
            PlayerStatisticsService playerStatisticsService) : base(inputService)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;
            _playerStatisticsService = playerStatisticsService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("ПОРАЖЕНИЕ!");

            _playerStatisticsService.Add(StatisticsItemTypes.Loss);

            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
