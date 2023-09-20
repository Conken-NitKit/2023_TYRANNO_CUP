using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tyranno.GameManager;
using Tyranno.GameManagers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ResultAnimation : MonoBehaviour
{
    [SerializeField]
    private MainGameManager _mainGameManager;

    [SerializeField]
    private GameObject _resultUIs;

    [SerializeField] 
    private Image[] _finishImages;

    [SerializeField]
    private Image _resultBackGround;

    [SerializeField]
    private Image _clipboard;
    
    [SerializeField]
    private Image[] _doctorCommentImages;
    
    [SerializeField]
    private Text[] _doctorComments;
    
    
    void Start()
    {
        _mainGameManager.CurrentGameState.Where(x => x == GameState.Result).Subscribe(x =>
        {
            _resultUIs.SetActive(true);
            AnimateResult();
        });
    }
    
    void AnimateResult()
    {
        var sequence = DOTween.Sequence();
        foreach (var finishImage in _finishImages)
        {
            sequence.Join(finishImage.rectTransform.DOShakePosition(2f, 5f, 100, 1, false, true)).SetEase(Ease.Linear);
        }

        sequence.Append(_resultBackGround.rectTransform.DOMove(new Vector2(0f, 0f), 1.5f).SetEase(Ease.OutBounce));

        sequence.Append(_clipboard.rectTransform.DOLocalMove(new Vector2(125f, 0f), 1f).SetEase(Ease.OutBack));
        sequence.Join(_clipboard.rectTransform.DOLocalRotate(new Vector3(0f, 0f, 705f),1f, RotateMode.FastBeyond360).SetEase(Ease.OutBack));
        
        foreach (var doctorCommentImage in _doctorCommentImages)
        {
            sequence.Join(doctorCommentImage.DOFade(1f, 1f).SetEase(Ease.Linear));
        }
        
        foreach (var doctorComment in _doctorComments)
        {
            sequence.Join(doctorComment.DOFade(1f, 1f).SetEase(Ease.Linear));
        }
        
        sequence.Play();
    }
}
