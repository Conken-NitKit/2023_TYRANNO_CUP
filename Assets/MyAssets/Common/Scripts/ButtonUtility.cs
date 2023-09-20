using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.Common
{
    public class ButtonUtility : MonoBehaviour
    {
        [SerializeField] private Button button;

        [SerializeField]
        private bool _canHitRepeatedly;
        
 
        void Start()
        {
            if (_canHitRepeatedly)
            {
                button.onClick.AsObservable().Subscribe(_ =>
                {
                    button.interactable = false;
                }).AddTo(gameObject);
                
                button.onClick.AsObservable().Delay(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
                {
                    button.interactable = true;
                }).AddTo(gameObject);
            }
        }
    }
}
