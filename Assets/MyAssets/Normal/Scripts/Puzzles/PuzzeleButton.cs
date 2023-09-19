using System;
using Tyranno.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

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

        async Task Start()
        {
            Thread.Sleep(3000);

            Initialize();
        }

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
