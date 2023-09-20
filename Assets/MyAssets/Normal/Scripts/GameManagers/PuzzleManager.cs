using System;
using System.Collections;
using System.Collections.Generic;
using Tyranno.Puzzle;
using Tyranno.Puzzle.Algorithms;
using UniRx;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Tyranno.GameManager
{
    /// <summary>
    /// パズルの状況を管理するクラスw
    /// </summary>
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField]
        private PuzzleState _puzzleState;
        
        [SerializeField]
        private GameSetting _gameSetting;

        private IntReactiveProperty _currentWaveNum = new IntReactiveProperty(0);
        public IReadOnlyReactiveProperty<int> CurrentWaveNum => _currentWaveNum;

        [NonSerialized]
        public int ConditionNum = 1;

        [NonSerialized]
        public int[] ConditionsOrder = new int[100];
        
        [NonSerialized]
        public bool[] JudgmentConditions = new bool[100];

        private Func<bool[,],bool>[] ConditionsMethods =
        {
            ConditionProfiles.IsLeftToRightMaze,
            ConditionProfiles.IsUpperToBottomMaze,
            ConditionProfiles.IsSingleColoredWall,
            ConditionProfiles.IsSymmetry, 
            ConditionProfiles.IsPointSymmetry,
            ConditionProfiles.IsVerticalSymmetry,
            ConditionProfiles.IsQuantityLimit,
            ConditionProfiles.IsFalseConnectionSizeVaild,
            ConditionProfiles.IsTrueCountInRowOrColumnValid,
            ConditionProfiles.IsTrueConnectionSizeVaild
        };
        
        void Start()
        {
            ConditionNum = _gameSetting.StartConditionsNum;

            for (int i = 0; i < _gameSetting.MaxConditionsNum; i++)
            {
                JudgmentConditions[i] = false;
            }
        
            for (int i = 0; i < ConditionsMethods.Length; i++)
            {
                ConditionsOrder[i] = i;
            }
        
            for (int i = 0; i < ConditionsMethods.Length; i++)
            {
                var temp = ConditionsOrder[i]; 
                int randomIndex = Random.Range(0, ConditionsMethods.Length); 
                ConditionsOrder[i] = ConditionsOrder[randomIndex]; 
                ConditionsOrder[randomIndex] = temp; 
            }
            
        }
    
        public void OnClicked()
        {
            for (int i = 0; i < ConditionNum; i++)
            {
                if (!ConditionsMethods[ConditionsOrder[i]](_puzzleState.SquareArray))
                {
                    JudgmentConditions[i] = false;
                    continue;
                }
                JudgmentConditions[i] = true;
            }
            
            for (int i = 0; i < ConditionNum; i++)
            {
                if (!JudgmentConditions[i])
                {
                    return;
                }
            }
            
            _currentWaveNum.Value++;
            ConditionNum++;
        }
    }
}