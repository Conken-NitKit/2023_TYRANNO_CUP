using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManager;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Tyranno.UI
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField]
        private GameSetting _gameSetting;

        [SerializeField]
        private TimeManager _timeManager;

        [SerializeField]
        private GameObject _inputTimeAttack;

        [SerializeField]
        private Text _teacherComment;

        [SerializeField]
        private Text _scoreType;

        [SerializeField]
        private Text _score;

        void Start()
        {
            
            if (_gameSetting.IsTimeAttack)
            {
                _inputTimeAttack.SetActive(true);
                _timeManager.GameSecond.Subscribe(x =>
                {
                    _score.text = $"{x}";
                });

                _scoreType.text = "あなたのタイムは";

                _teacherComment.text = $"{_gameSetting.Comment}";
            }
            else
            {
                _inputTimeAttack.SetActive(false);
                _score.text = $"{_gameSetting.Score}";
                _scoreType.text = "あなたのIQは";
                _teacherComment.text = $"{_gameSetting.Comment}";
            }
        }
    }
}