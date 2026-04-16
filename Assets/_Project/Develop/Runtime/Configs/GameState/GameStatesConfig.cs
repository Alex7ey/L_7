using Assets._Project.Develop.Runtime.Gameplay.Features.Ability;
using Assets._Project.Develop.Runtime.Gameplay.GameStates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.GameState
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/State/NewGameStatesConfig", fileName = "GameStatesConfig")]
    public class GameStatesConfig : ScriptableObject
    {
        [SerializeField] private List<StateConfig> _stateConfig;

        public StateConfig GetConfig(GameStateType type) 
            => _stateConfig.Find(state => state.GameStateType == type);    
    }

    [Serializable]
    public class StateConfig
    {
        [field: SerializeField] public AbilityType Ability { get; private set; }

        [field: SerializeField] public GameStateType GameStateType { get; private set; }
    }
}
