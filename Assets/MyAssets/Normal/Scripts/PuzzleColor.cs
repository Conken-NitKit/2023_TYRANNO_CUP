using System.Collections;
using System.Collections.Generic;
using Tyranno.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.Puzzle
{
    public class PuzzleColor : MonoBehaviour
    {
        [SerializeField]
        private PuzzleState _puzzleState;

        private ChildImageArray[] _imageArray;

        [SerializeField]
        private Color _paintColor = Color.black;

        private void Initialize()
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    var a = i;
                    var b = j;

                    _puzzleState.MatrixColorFilledStates[i,j].Subscribe(x =>
                    {
                        _puzzleState.SquareArray[a, b] = x;
                        if (x)
                        {
                            _imageArray[a].childImageArray[b].color = Color.black;
                        }
                        else
                        {
                            _imageArray[a].childImageArray[b].color = Color.white;
                        }
                    }).AddTo(gameObject);
                }
            } 
        }
        
        public void SetPaintColor(Color paintColor)
        {
            _paintColor = paintColor;
        }
    }
    
    [System.Serializable]
    public class ChildImageArray
    {
        public Image[] childImageArray;
    }
}
