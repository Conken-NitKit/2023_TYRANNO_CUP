using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManager;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Tyranno.UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField]
        private TimeManager _timeManager;

        [SerializeField] 
        private Text _countSeconds;

        [SerializeField] 
        private GameSetting _gameSetting;

        void Start()
        {
            _countSeconds.text = "";
            if (_gameSetting.IsTimeAttack)
            {
                _timeManager.GameSecond.Subscribe(x =>
                {
                    _countSeconds.text = $"{x / 60:00} : {x % 60:00}";
                });
            }
        }
    }
}
