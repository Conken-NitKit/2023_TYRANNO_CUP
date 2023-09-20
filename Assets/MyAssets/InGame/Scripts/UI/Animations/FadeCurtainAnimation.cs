using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tyranno.Ui.Animations
{
    public class FadeCurtainAnimation : MonoBehaviour
    {
        [SerializeField]
        private Image[] _curtains;

        
        void Start()
        {
            OpenAnimation();
        }

        void OpenAnimation()
        {
            foreach (var curtain in _curtains)
            {
                curtain.DOFillAmount(0f, 1.5f).SetEase(Ease.OutSine);
                curtain.DOFade(0f, 3f).SetEase(Ease.InCubic);
            }
        }
        
        void CloseAnimation()
        {
            foreach (var curtain in _curtains)
            {
                curtain.DOFillAmount(1f, 1.5f).SetEase(Ease.OutSine);
                curtain.DOFade(1f, 3f).SetEase(Ease.InCubic);
            }
        }
    }
}
