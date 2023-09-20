using System.Collections;
using System.Collections.Generic;
using Tyranno.Puzzle;
using UniRx;
using UnityEngine;

public class hoge4 : MonoBehaviour
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
        Debug.Log($"{Tyranno.Puzzle.Algorithms.ConditionProfiles.LeftToRightMaze(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.UpperToBottomMaze(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.SingleColoredWall(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.Symmetry(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.PointSymmetry(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.VerticalSymmetry(_puzzleState.SquareArray)},{Tyranno.Puzzle.Algorithms.ConditionProfiles.QuantityLimit(_puzzleState.SquareArray)}");
    }
}
