using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyDialogManager : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 rectPos;
    [SerializeField] private float rotate;

    // Start is called before the first frame update
    void OnEnable()
    {
        rectTransform.anchoredPosition = new Vector2(0, -450);
        rectTransform.rotation = Quaternion.Euler(0, 0, 30);
        var sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPos(rectPos, 0.8f).SetEase(Ease.OutExpo));
        sequence.Join(rectTransform.DORotate(new Vector3(0, 0, rotate), 0.8f, RotateMode.Fast).SetEase(Ease.OutExpo));
        sequence.Play();

    }

    public void Disable()
    {
        this.gameObject.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPos(new Vector2(0, -450), 0.4f).SetEase(Ease.InBack));
        sequence.Join(rectTransform.DORotate(new Vector3(0, 0, 30), 0.4f, RotateMode.Fast).SetEase(Ease.InBack)) ;
        sequence.Play().OnComplete(() => this.gameObject.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
