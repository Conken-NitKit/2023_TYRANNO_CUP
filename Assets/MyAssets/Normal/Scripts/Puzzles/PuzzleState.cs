using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Tyranno.Common;
using UnityEngine.UI;

namespace Tyranno.Puzzle
{
    public class PuzzleState : MonoBehaviour
    {
        public BoolReactiveProperty[,] MatrixColorFilledStates =
            new BoolReactiveProperty[GlobalConst.SQUARE_SIZE, GlobalConst.SQUARE_SIZE];

        public bool[,] SquareArray = new bool[GlobalConst.SQUARE_SIZE, GlobalConst.SQUARE_SIZE];
        
        [SerializeField]
        private ChildImageArray[] _imageArray;

        [SerializeField]
        private Color _paintColor = Color.black;

        void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            SetAllElementsReactiveProperty(false);

            BoolReactivePropertiesToArray();
            
            
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    var a = i;
                    var b = j;
                    
                    Debug.Log(MatrixColorFilledStates[a,b]);

                    MatrixColorFilledStates[i, j].Subscribe(x =>
                    {
                        SquareArray[a, b] = x;
                        
                        if (x)
                        {
                            _imageArray[a].ImageArray[b].color = _paintColor;
                        }
                        else
                        {
                            _imageArray[a].ImageArray[b].color = Color.white;
                        }
                        
                    }).AddTo(gameObject);
                }
            } 
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
        
        public void SetValue(int i, int j)
        {
            MatrixColorFilledStates[i,j].Value = !MatrixColorFilledStates[i,j].Value;
        }
        
        public void SetPaintColor(Color paintColor)
        {
            _paintColor = paintColor;
        }
    }
    
    [System.Serializable]
    public class ChildImageArray
    {
        public Image[] ImageArray;
    }
}
