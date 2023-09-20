using System;
using Tyranno.Common;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Tyranno.Puzzle
{
    /// <summary>
    /// パズルのマス（ボタン）を管理するクラス
    /// </summary>
    public class PuzzleButton : MonoBehaviour
    {
        [SerializeField]
        private PuzzleState _puzzleState;
        
        [SerializeField]
        private ChildButtonArray[] _buttonArray;

        [SerializeField] 
        private ChildSquareInfoArray[] _squareInfoArray;

        IEnumerator  Start()
        {
            yield return new WaitForSeconds(0.3f);

            Initialize();
        }
        
        /// <summary>
        /// 初期化処理
        /// マスが押されたイベントを購読してる
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < GlobalConst.SQUARE_SIZE; i++)
            {
                for (int j = 0; j < GlobalConst.SQUARE_SIZE; j++)
                {
                    var a = i;
                    var b = j;
                    
                    _buttonArray[i].ButtonArray[j].onClick.AsObservable().Subscribe(_ =>
                    {
                        _puzzleState.SetReverseValue(_squareInfoArray[a].SquareInfoArray[b].Row, _squareInfoArray[a].SquareInfoArray[b].Column);
                    }).AddTo(gameObject);
                }
            }
        }
    }

    /// <summary>
    /// UI.Buttonを多次元配列にしてインスペクターに表示するためのクラス
    /// </summary>
    [System.Serializable]
    class ChildButtonArray
    {
        public Button[] ButtonArray;
    }
    
    /// <summary>
    /// SquareInfoを多次元配列にしてインスペクターに表示するためのクラス
    /// </summary>
    [System.Serializable]
    class ChildSquareInfoArray
    {
        public SquareInfo[] SquareInfoArray;
    }
}
