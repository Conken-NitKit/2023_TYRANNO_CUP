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

    private int _conditionNum = 1;

    private int[] oi = new int[5];

    [SerializeField]
    private string[] _setumeis = new string[5];

    private string _setumeidayo;

    [SerializeField]
    private Text _text;

    [SerializeField] private Text _seigo;
    
    private Func<bool[,],bool>[] funcs =
    {
        ConditionProfiles.LeftToRightMaze,
        ConditionProfiles.UpperToBottomMaze,
        ConditionProfiles.SingleColoredWall,
        ConditionProfiles.Symmetry, 
        ConditionProfiles.PointSymmetry
    };

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            oi[i] = i;
        }
        
        for (int i = 0; i < 5; i++)
        {
            //（説明１）現在の要素を預けておく
            var temp = oi[i]; 
            //（説明２）入れ替える先をランダムに選ぶ
            int randomIndex = Random.Range(0, 5); 
            //（説明３）現在の要素に上書き
            oi[i] = oi[randomIndex]; 
            //（説明４）入れ替え元に預けておいた要素を与える
            oi[randomIndex] = temp; 
        }
        

        _setumeidayo = _setumeis[oi[0]];
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
        _setumeidayo += $"\n\n{_setumeis[oi[_conditionNum]]}";
        _text.text = _setumeidayo;
        _conditionNum++;
    }
}
