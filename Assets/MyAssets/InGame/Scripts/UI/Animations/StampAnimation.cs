using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tyranno.Ui.Animations
{
    public class StampAnimation : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _completeMethod;

        [SerializeField]
        private Image _stamp;

        public void PushStamp()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_stamp.DOFade(1f, 0.5f).SetEase(Ease.InQuint));
            sequence.Join(_stamp.rectTransform.DOScale(new Vector2(1, 1), 0.5f).SetEase(Ease.InQuint));
            if(_completeMethod != null)sequence.AppendCallback(_completeMethod.Invoke);

            sequence.Play();
        }
    }
}
