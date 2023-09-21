using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Tyranno.Common;
using UnityEngine.UI;

namespace Tyranno.Puzzle
{
    /// <summary>
    /// パズルの状態を保持するクラス
    /// TODO : 処理の都合でPuzzleColorと統合してるので時間があれば修正
    /// </summary>
    public class PuzzleState : MonoBehaviour
    {
        public BoolReactiveProperty[,] MatrixColorFilledStates =
            new BoolReactiveProperty[GlobalConst.SQUARE_SIZE, GlobalConst.SQUARE_SIZE];

        public bool[,] SquareArray = new bool[GlobalConst.SQUARE_SIZE, GlobalConst.SQUARE_SIZE];
        
        [SerializeField]
        private ChildImageArray[] _imageArray;

        [SerializeField]
        private Color _paintColor = Color.black;

        /// <summary>
        /// 初期化処理
        /// 初期値の代入と配列化を行っています
        /// </summary>
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

        /// <summary>
        /// ReactivePropertyを配列に変換するメソッド
        /// </summary>
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
        
        /// <summary>
        /// ReactivePropertyの配列に初期値を代入するメソッド
        /// </summary>
        /// <param name="initialValue"></param>
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

        /// <summary>
        /// 配列の全ての値に同値をセットするメソッド
        /// </summary>
        /// <param name="setValue"></param>
        public void SetAllElements(bool setValue)
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    MatrixColorFilledStates[i, j].Value = setValue;
                }
            } 
        }
        
        /// <summary>
        /// 指定した位置の値に逆の値を入れるメソッド
        /// </summary>
        /// <param name="i">行</param>
        /// <param name="j">列</param>
        public void SetReverseValue(int i, int j)
        {
            MatrixColorFilledStates[i,j].Value = !MatrixColorFilledStates[i,j].Value;
        }
        
        /// <summary>
        /// パズルのマスに塗る色を変更するメソッド
        /// </summary>
        /// <param name="paintColor"></param>
        public void SetPaintColor(Color paintColor)
        {
            _paintColor = paintColor;
        }
    }
    
    /// <summary>
    /// UI.Imageを多次元配列にしてインスペクターに表示するためのクラス
    /// </summary>
    [System.Serializable]
    public class ChildImageArray
    {
        public Image[] ImageArray;
    }
}
