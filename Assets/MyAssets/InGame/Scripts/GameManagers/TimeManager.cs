using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Tyranno.GameManager
{
    /// <summary>
    /// 時間を管理するクラス
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private IntReactiveProperty _readySecond = new IntReactiveProperty(3);

        [SerializeField]
        private IntReactiveProperty _gameSecond = new IntReactiveProperty(0);
        
        private Coroutine _gameCountCoroutine = null;

        /// <summary>
        /// ゲーム開始前のカウントダウン
        /// </summary>
        public IReadOnlyReactiveProperty<int> ReadySecond => _readySecond;

        /// <summary>
        /// ゲームの経過時間
        /// </summary>
        public IReadOnlyReactiveProperty<int> GameSecond => _gameSecond;

        [SerializeField]
        private bool _playTimeAttack;

        void Awake()
        {
            _playTimeAttack = false;
        }

        /// <summary>
        /// レディカウントダウンを開始する
        /// </summary>
        public void StartGameReadyCountDown()
        {
            StartCoroutine(ReadyCountCoroutine());
        }

        /// <summary>
        /// レディカウントダウンするコルーチン
        /// </summary>
        /// <returns></returns>
        IEnumerator ReadyCountCoroutine()
        {
            yield return new WaitForSeconds(0.5f);

            _readySecond.SetValueAndForceNotify(_readySecond.Value);

            yield return new WaitForSeconds(1);
            while (_readySecond.Value > 0)
            {
                _readySecond.Value -= 1;
                yield return new WaitForSeconds(1);
            }
        }

        /// <summary>
        /// タイムカウントを開始する
        /// </summary>
        public void StartGameCountUp()
        {
            Debug.Log("hoge");
            _playTimeAttack = true;
            _gameCountCoroutine = StartCoroutine(GameCountUpCoroutine());
        }

        /// <summary>
        /// タイムカウントを行うコルーチン
        /// </summary>
        IEnumerator GameCountUpCoroutine()
        {
            while (_playTimeAttack)
            {
                yield return new WaitForSeconds(1);
                _gameSecond.Value++;
            }
        }

        /// <summary>
        /// 呼び出すことでタイムカウントを止める
        /// </summary>
        public void StopTimeCountUp()
        {
            if(_gameCountCoroutine != null && _playTimeAttack) _playTimeAttack = false;
        }
    }
}
