using System.Collections;
using System.Collections.Generic;
using Tyranno.Puzzle;
using UniRx;
using UnityEngine;

public class hoge4copy : MonoBehaviour
{
    [SerializeField]
    private PuzzleState _puzzleState;

    private char[,] array = new char[3, 3];

    private string hoge = "";
    
    public void Clicked()
    {
        for (int i = 0; i < 9; i++)
        {
            hoge += "{";
            for (int j = 0; j < 9; j++)
            {
                if (_puzzleState.SquareArray[i, j])
                {
                    hoge += "◎";
                }
                else
                {
                    hoge += "×";
                }

                if (j != 8)
                {
                    hoge += ',';
                }
            }
            hoge += "}\n";
        }
        Debug.Log(hoge);
        Debug.Log($"{Tyranno.Puzzle.Algorithms.ConditionProfiles.IsSurroundedByFalse(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.IsHorizontallyOrVerticallyConnected(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.IsNot4FoldSymmetry(_puzzleState.SquareArray)}");
    }
}
