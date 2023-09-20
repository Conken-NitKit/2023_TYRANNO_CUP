using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tyranno.Ui.Animations
{
    public class FadeBallAnimation : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _completeMethod;

        [SerializeField]
        private Image _ball;

        public void CloseAnimation()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_ball.rectTransform.DOLocalMove(new Vector2(0, 0), 1f));
            sequence.Join(_ball.rectTransform.DOScale(new Vector2(0, 0), 1f));
            sequence.Append(_ball.rectTransform.DOScale(new Vector2(50, 50), 1f));
            if(_completeMethod != null)sequence.AppendCallback(_completeMethod.Invoke);

            sequence.Play();
        }
    }
}