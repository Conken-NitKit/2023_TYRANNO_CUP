using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManagers;
using UniRx;
using UnityEngine;

namespace Tyranno.GameManager
{
    /// <summary>
    /// ゲームの進行を管理するクラス
    /// </summary>
    public class MainGameManager : MonoBehaviour
    {
        private GameStateReactiveProperty _currentState = new GameStateReactiveProperty(GameState.Init);

        public IReadOnlyReactiveProperty<GameState> CurrentGameState => _currentState;
        
        private TimeManager _timeManager;
        private PuzzleManager _puzzleManager;
        private GamePauseManager _gamePauseManager;
        
        void Start()
        {
            _timeManager = GetComponent<TimeManager>();
            _puzzleManager = GetComponent<PuzzleManager>();
            _gamePauseManager = GetComponent<GamePauseManager>();
        }
    }
}
