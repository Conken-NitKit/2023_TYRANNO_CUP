using System;
using System.Collections;
using System.Collections.Generic;
using Tyranno.Puzzle;
using Tyranno.Puzzle.Algorithms;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class hoge6 : MonoBehaviour
{
    [SerializeField]
    private PuzzleState _puzzleState;

    [SerializeField]
    private GameSetting _gameSetting;

    private int _conditionNum = 1;

    private int[] oi = new int[10];

    [SerializeField]
    private string[] _setumeis = new string[10];

    private string _setumeidayo;

    [SerializeField]
    private Text _text;

    [SerializeField] private Text _seigo;
    
    private Func<bool[,],bool>[] funcs =
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
        _conditionNum = _gameSetting.StartConditionsNum;
        
        for (int i = 0; i < 10; i++)
        {
            oi[i] = i;
        }
        
        for (int i = 0; i < 10; i++)
        {
            var temp = oi[i]; 
            int randomIndex = Random.Range(0, 10); 
            oi[i] = oi[randomIndex]; 
            oi[randomIndex] = temp; 
        }

        for (int i = 0; i < _conditionNum; i++)
        {
            _setumeidayo += $"{_setumeis[oi[i]]}\n\n";
        }
        _text.text = _setumeidayo;
    }
    
    public void OnClicked()
    {
        for (int i = 0; i < _conditionNum; i++)
        {
            if (!funcs[oi[i]](_puzzleState.SquareArray))
            {
                _seigo.text = "不正解じゃ！";
                return;
            }
        }
        
        _seigo.text = "正解じゃ！";
        _setumeidayo += $"{_setumeis[oi[_conditionNum]]}\n\n";
        _text.text = _setumeidayo;
        _conditionNum++;
    }
}
