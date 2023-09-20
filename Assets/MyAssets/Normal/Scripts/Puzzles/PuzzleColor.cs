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
    /// <summary>
    /// パズルのマスの色を変えるクラス
    /// </summary>
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
        
        /// <summary>
        /// 初期化処理
        /// 現在のパズルの状況を購読してパズルの色を変更する
        /// </summary>
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
        
        /// <summary>
        /// パズルのマスに塗る色を変更するメソッド
        /// </summary>
        public void SetPaintColor(Color paintColor)
        {
            _paintColor = paintColor;
        }*/
    }
    
    /// <summary>
    /// c
    /// </summary>
    /*[System.Serializable]
    public class ChildImageArray
    {
        public Image[] ImageArray;
    }*/
}
