using System.Collections;
using System.Collections.Generic;
using Tyranno.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.Puzzle
{
    public class PuzzleButton : MonoBehaviour
    {
        [SerializeField]
        private PuzzleState _puzzleState;
        
        [SerializeField]
        private ChildButtonArray[] _buttonArray;

        [SerializeField] 
        private ChildSquareInfoArray[] _squareInfoArray;

        private void Initialize()
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    var a = i;
                    var b = j;

                    _buttonArray[i].ButtonArray[j].onClick.AsObservable().Subscribe(_ =>
                    {
                        _puzzleState.SetValue(_squareInfoArray[a].SquareInfoArray[b].Row, _squareInfoArray[a].SquareInfoArray[b].Column);
                    }).AddTo(gameObject);
                }
            }
        }
    }

    [System.Serializable]
    class ChildButtonArray
    {
        public Button[] ButtonArray;
    }
    
    [System.Serializable]
    class ChildSquareInfoArray
    {
        public SquareInfo[] SquareInfoArray;
    }
}
