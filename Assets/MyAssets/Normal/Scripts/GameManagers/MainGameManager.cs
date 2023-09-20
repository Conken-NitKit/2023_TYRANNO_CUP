using System;
using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManagers;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Tyranno.GameManager
{
    /// <summary>
    /// ゲームの進行を管理するクラス
    /// </summary>
    public class MainGameManager : MonoBehaviour, IGameStateProvider
    {
        private GameStateReactiveProperty _currentState = new GameStateReactiveProperty(GameState.Init);

        public IReadOnlyReactiveProperty<GameState> CurrentGameState => _currentState;
        
        private TimeManager _timeManager;
        private PuzzleManager _puzzleManager;
        private GamePauseManager _gamePauseManager;

        [SerializeField]
        private UnityEvent[] _initializeMethods;
        
        [SerializeField]
        private GameSetting _gameSetting;
        
        void Start()
        {
            _timeManager = GetComponent<TimeManager>();
            _puzzleManager = GetComponent<PuzzleManager>();
            _gamePauseManager = GetComponent<GamePauseManager>();
            
            _currentState.Subscribe(state =>
            {
                OnStateChanged(state);
            });
        }

        void OnStateChanged(GameState nextState)
        {
            Debug.Log(nextState);
            switch (nextState)
            {
                case GameState.Init:
                    StartCoroutine(InitCoroutine());
                    break;
                case GameState.Ready:
                    Ready();
                    break;
                case GameState.Game:
                    MainGame();
                    break;
                case GameState.Result:
                    Result();
                    break;
                default:
                    break;
            }
        }

        IEnumerator InitCoroutine()
        {
            foreach (var _initializeMethod in _initializeMethods)
            {
                _initializeMethod.Invoke();
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return null;
            
            _currentState.Value = GameState.Ready;
        }

        void Ready()
        {
            _timeManager.ReadySecond
                .FirstOrDefault(x => x >= 0)
                .Delay(TimeSpan.FromSeconds(1))
                .Subscribe(_ => _currentState.Value = GameState.Game)
                .AddTo(gameObject);
            
            _timeManager.StartGameReadyCountDown();
        }

        void MainGame()
        {
            if (_gameSetting.isTimeAttack)
            {
                _timeManager.StartGameCountUp();
            }
        }

        void Result()
        {
            
        }
    }
}
