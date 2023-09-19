using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Tyranno.Common;

namespace Tyranno.Puzzle
{
    public class PuzzleState : MonoBehaviour
    {
        public BoolReactiveProperty[,] MatrixColorFilledStates =
            new BoolReactiveProperty[GlobalConst.SQUARE_SIZE, GlobalConst.SQUARE_SIZE];

        public bool[,] SquareArray = new bool[GlobalConst.SQUARE_SIZE, GlobalConst.SQUARE_SIZE];

        public void Initialize()
        {
            SetAllElementsReactiveProperty(false);

            BoolReactivePropertiesToArray();
        }

        private void BoolReactivePropertiesToArray()
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    var a = i;
                    var b = j;

                    SquareArray[a, b] = MatrixColorFilledStates[a, b].Value;
                }
            } 
        }
        
        private void SetAllElementsReactiveProperty(bool initialValue)
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    MatrixColorFilledStates[i,j] = new BoolReactiveProperty(initialValue);
                }
            } 
        }

        private void SetAllElements(bool setValue)
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    MatrixColorFilledStates[i, j].Value = setValue;
                }
            } 
        }
    }
}
