using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Tyranno.GameManager;

namespace Tyranno.UI
{
    public class ConditionsMatchUI : MonoBehaviour
    {
        [SerializeField]
        private Image _speechBubble;

        [SerializeField]
        private Text _speech;

        [SerializeField]
        private PuzzleManager _puzzleManager;

        private void Start()
        {
            _puzzleManager.IsConditionMet.SkipLatestValueOnSubscribe().Subscribe(x =>
            {
                _speechBubble.rectTransform.anchoredPosition = new Vector2(-325f, -50f);
                _speechBubble.rectTransform.localScale = new Vector3(0,0,0);
                _speechBubble.rectTransform.DOAnchorPos(new Vector2(-230f, 10f), 0.3f).SetEase(Ease.OutBack);
                _speechBubble.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
                
                if (x)
                {
                    _speech.text = "正解じゃ！";
                }
                else
                {
                    _speech.text = "不正解じゃ...";
                }
            });
        }
    }
}
