using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject hukidashi;
    [SerializeField] private GameObject katayaburi;
    [SerializeField] private GameObject noutore;
    [SerializeField] private GameObject difficultyDialog;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject bluePostit;
    [SerializeField] private GameObject rayCastBlocker;
    [SerializeField] private UnityEngine.UI.Button normalModeButton;
    [SerializeField] private UnityEngine.UI.Button challengeModeButton;
    [SerializeField] private UnityEngine.UI.Button rankingButton;
    [SerializeField] private UnityEngine.UI.Button backButton;
    [SerializeField] private UnityEngine.UI.Button doctor;
    [SerializeField] private UnityEngine.UI.Button bluePostitButton;
    [SerializeField] private UnityEngine.UI.Button easyButton;
    [SerializeField] private UnityEngine.UI.Button normalButton;
    [SerializeField] private UnityEngine.UI.Button hardButton;
    [SerializeField] private UnityEngine.UI.Button hardcoreButton;
    [SerializeField] private UnityEngine.UI.Image transitionImage;

    private SEManager seManager;

    // Start is called before the first frame update
    void Start()
    {
        seManager = Camera.main.GetComponent<SEManager>();
        rayCastBlocker.SetActive(false);

        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(katayaburi.transform.DOLocalMove(new Vector3(-13.9f, 0.8f, 0f), 0.8f));
        sequence.Append(hukidashi.transform.DORotate(new Vector3(0, 0, 359), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutBack));
        sequence.Join(hukidashi.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutExpo));
        sequence.Append(noutore.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack));

        

        sequence.Play().OnComplete(() => doctor.interactable = true);
        
        //RectTransform rectTransform = hukidashi.GetComponent<RectTransform>();
        //katayaburi.transform.DOLocalMove(new Vector3(5f, 0f, 0f), 3f);
        //hukidashi.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutExpo);
        //hukidashi.transform.DOMoveY(6f, 3f).SetEase(Ease.Linear);
        //rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 6f,0.5f).SetEase(Ease.Linear);
        
    }

    private void OnEnable()
    {
        rayCastBlocker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Debug.Log("x:" + mousePos.x + "    y:" + mousePos.y);
        }
    }

    public void OnNormalModeClick()
    {
        difficultyDialog.SetActive(true);
        back.SetActive(true);
        normalModeButton.interactable = false;
        backButton.interactable = true;
        seManager.PlayEnter1();
    }

    public void OnBackClick()
    {
        difficultyDialog.GetComponent<DifficultyDialogManager>().Disable();
        backButton.interactable = false;
        back.GetComponent<DifficultyDialogManager>().Disable();
        normalModeButton.interactable = true;
        seManager.PlayEnter1();
    }

    public void OnChallengeModeClick()
    {
        seManager.PlayEnter1();
        //ここにチャレンジモードシーン転移処理を書く
    }

    public void OnRankingClick()
    {
        seManager.PlayEnter1();
        //ここにランキングシーン転移処理を書く
    }

    DG.Tweening.Sequence crazySequence = DOTween.Sequence();

    public void OnDocterClick()
    {
        doctor.interactable = false;

        DG.Tweening.Sequence crazySequence = DOTween.Sequence();

        crazySequence.Append(noutore.transform.DORotate(new Vector3(0, 0, -20), 0.15f, RotateMode.Fast));
        crazySequence.Append(noutore.transform.DORotate(new Vector3(0, 0, 40), 0.3f, RotateMode.Fast).SetEase(Ease.InOutBounce).SetLoops(int.MaxValue, LoopType.Yoyo));
        crazySequence.Join(noutore.transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.2f).SetEase(Ease.InOutBack).SetLoops(int.MaxValue, LoopType.Yoyo));
        crazySequence.Join(noutore.transform.DOLocalMove(new Vector3(0f, 0f, 2f), 0.4f).SetEase(Ease.InOutBack).SetLoops(int.MaxValue, LoopType.Yoyo));

        crazySequence.Play();
    }

    int postitClickCount = 0;

    public void OnBluePostitClick()
    {
        postitClickCount++;
        if (postitClickCount >= 5)
        {
            bluePostitButton.interactable = false;
            bluePostit.transform.DOLocalMove(new Vector3(0f, -400f, 0f), 1f).SetEase(Ease.InCubic).OnComplete(() => Destroy(bluePostit));
        }
    }

    public void OnEasyClick()
    {
        seManager.PlayEnter2();
        //ここにeasyの処理を書く
    }

    public void OnNormalClick()
    {
        seManager.PlayEnter2();
        //ここにnormalの処理を書く
    }

    public void OnHardClick()
    {
        seManager.PlayEnter2();
        //ここにhardの処理を書く
    }

    public void OnHardcoreClick()
    {
        Camera.main.GetComponent<AudioSource>().pitch = 0.8f;
        //seManager.PlayBell();
        //ここにhardcoreの処理を書く
        Transition(()=>Debug.Log("転移"));
    }

    private void Transition(Action action)
    {
        rayCastBlocker.SetActive(true); 
        var sequence = DOTween.Sequence();
        sequence.Append(transitionImage.DOFillAmount(1, 0.5f));
        sequence.AppendInterval(3f);
        sequence.Play().OnComplete(() => action());
    }
}
