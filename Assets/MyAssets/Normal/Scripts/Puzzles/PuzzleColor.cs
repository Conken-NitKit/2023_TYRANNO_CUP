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
    public class PuzzleColor : MonoBehaviour
    {
        /*[SerializeField]
        private PuzzleState _puzzleState;

        [SerializeField]
        private ChildImageArray[] _imageArray;

        [SerializeField]
        private Color _paintColor = Color.black;

        async Task Start()
        {
            Thread.Sleep(3000);
            
            Debug.Log("Initialized");

            Initialized();
        }

        private void Initialized()
        {
            
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    var a = i;
                    var b = j;
                    
                    Debug.Log(_puzzleState.MatrixColorFilledStates);

                    _puzzleState.MatrixColorFilledStates[a,b].Subscribe(x =>
                    {
                        if (x)
                        {
                            _imageArray[a].ImageArray[b].color = _paintColor;
                        }
                        else
                        {
                            _imageArray[a].ImageArray[b].color = _paintColor;
                        }
                    }).AddTo(gameObject);
                }
            } 
            Debug.Log("色変え終わり");
        }
        
        public void SetPaintColor(Color paintColor)
        {
            _paintColor = paintColor;
        }*/
    }
    
    /*[System.Serializable]
    public class ChildImageArray
    {
        public Image[] ImageArray;
    }*/
}
