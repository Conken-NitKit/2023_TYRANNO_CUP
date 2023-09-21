using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;

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
    [SerializeField] private UnityEngine.UI.Image transitionImageR;
    [SerializeField] private UnityEngine.UI.Image transitionImageG;
    [SerializeField] private UnityEngine.UI.Image transitionImageB;

    private static int transitionCount = 0;


    private SEManager seManager;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionCount != 0)
        {
            TransitionBack();
        }
        transitionCount++;
        
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
        seManager.PlayEnter2();
        Transition(() => Debug.Log("ƒ`ƒƒƒŒƒ“ƒWƒ‚[ƒh‚É“]ˆÚ"));
    }

    public void OnRankingClick()
    {
        seManager.PlayEnter1();
        Transition(() => Debug.Log("ƒ‰ƒ“ƒLƒ“ƒO‚É“]ˆÚ"));
    }
    

    public void OnDocterClick()
    {
        doctor.interactable = false;

        doctor.gameObject.transform.DORotate(new Vector3(0, 0, 359), 0.1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);

        Camera.main.GetComponent<AudioSource>().pitch = 1.2f;

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
        Transition(() => Debug.Log("Easy‚É“]ˆÚ"));
    }

    public void OnNormalClick()
    {
        seManager.PlayEnter2();
        Transition(() => Debug.Log("Normal‚É“]ˆÚ"));
    }

    public void OnHardClick()
    {
        seManager.PlayEnter2();
        Transition(() => Debug.Log("Hard‚É“]ˆÚ"));
    }

    public void OnHardcoreClick()
    {
        Camera.main.GetComponent<AudioSource>().pitch = 0.8f;
        //seManager.PlayBell();
        Transition(()=>Debug.Log("Hardcore‚É“]ˆÚ"));
    }

    private void Transition(Action action)
    {
        rayCastBlocker.SetActive(true);

        transitionImage.fillAmount = 0f;
        transitionImageR.fillAmount = 0f;
        transitionImageG.fillAmount = 0f;
        transitionImageB.fillAmount = 0f;
        Camera.main.GetComponent<AudioSource>().volume = 1f;

        var sequence = DOTween.Sequence();
        sequence.Append(transitionImageR.DOFillAmount(1, 0.45f).SetEase(Ease.OutCubic));
        sequence.Join(transitionImageG.DOFillAmount(1, 0.5f).SetEase(Ease.OutSine));
        sequence.Join(transitionImageB.DOFillAmount(1, 0.55f).SetEase(Ease.InSine));
        sequence.Join(transitionImage.DOFillAmount(1, 0.6f).SetEase(Ease.InCubic));
        sequence.Join(Camera.main.GetComponent<AudioSource>().DOFade(0f, 3f));
        sequence.Play().OnComplete(() => action());
    }

    private void TransitionBack()
    {
        rayCastBlocker.SetActive(false);
        
        transitionImage.fillAmount = 1f;
        transitionImageR.fillAmount = 1f;
        transitionImageG.fillAmount = 1f;
        transitionImageB.fillAmount = 1f;
        Camera.main.GetComponent<AudioSource>().volume = 0f;

        var sequence = DOTween.Sequence();
        sequence.Append(transitionImage.DOFillAmount(0, 0.45f).SetEase(Ease.OutCubic));
        sequence.Join(transitionImageB.DOFillAmount(0, 0.5f).SetEase(Ease.OutSine));
        sequence.Join(transitionImageG.DOFillAmount(0, 0.55f).SetEase(Ease.InSine));
        sequence.Join(transitionImageR.DOFillAmount(0, 0.6f).SetEase(Ease.InCubic));
        sequence.Join(Camera.main.GetComponent<AudioSource>().DOFade(1f, 2f));

        sequence.Play();
    }
}
