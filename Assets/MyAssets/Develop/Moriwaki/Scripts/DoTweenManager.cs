using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoTweenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject timeAttackImage;

    [SerializeField]
    private GameObject expandBackGround;

    [SerializeField]
    private Text nouText;

    [SerializeField]
    private GameObject TOP10Text;

    [SerializeField]
    private Text timeAttackText;

    [SerializeField]
    private GameObject FrameImage;

    void Start()
    {
        timeAttackImage.SetActive(false);
        TOP10Text.SetActive(false);

        //アニメーション
        nouText.DOFade(0.0f, 0.1f).SetDelay(2f).OnComplete(() =>
        {
            expandBackGround.transform.DOScaleX(3, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                expandBackGround.GetComponent<Image>().DOFade(0.0f, 0.05f).OnComplete(() =>
                {
                    timeAttackImage.SetActive(true);
                    timeAttackText.DOText("TimeAttack", 1).OnComplete(() =>
                    {
                        TOP10Text.SetActive(true);
                        TOP10Text.transform.DOLocalMoveY(40f, 1f).SetEase(Ease.OutExpo).OnComplete(() =>
                        {
                            TOP10Text.transform.DOScale(new Vector2(2f, 3.5f), 0.7f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
                            {
                                FrameImage.transform.DOScale(new Vector2(1.03f * 1.03f, 1.08f * 1.03f), 0.4f).SetLoops(-1, LoopType.Yoyo);
                            });
                        });
                    });
                });
            });
        });
    }
}
