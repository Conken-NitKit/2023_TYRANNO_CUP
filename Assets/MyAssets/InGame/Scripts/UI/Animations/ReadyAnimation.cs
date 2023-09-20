using System.Collections;
using System.Collections.Generic;
using Tyranno.GameManager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

namespace Tyranno.Ui.Animations
{
    public class ReadyAnimation : MonoBehaviour
    {
        [SerializeField] 
        private Image _readyCountGage;

        [SerializeField]
        private TimeManager _timeManager;

        void Start()
        {
            _timeManager.ReadySecond.Skip(1).Where(x => x > 0).Subscribe(_ =>
            {
                _readyCountGage.fillAmount = 1;
                _readyCountGage.DOFillAmount(0f, 1).SetEase(Ease.Linear);
            });
        }
    }
}
