using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject hukidashi;
    [SerializeField] private GameObject katayaburi;
    [SerializeField] private GameObject noutore;
    [SerializeField] private GameObject difficultyDialog;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject bluePostit;
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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void OnBackClick()
    {
        difficultyDialog.GetComponent<DifficultyDialogManager>().Disable();
        backButton.interactable = false;
        back.GetComponent<DifficultyDialogManager>().Disable();
        normalModeButton.interactable = true;
    }

    public void OnChallengeModeClick()
    {
        //�����Ƀ`�������W���[�h�V�[���]�ڏ���������
    }

    public void OnRankingClick()
    {
        //�����Ƀ����L���O�V�[���]�ڏ���������
    }

    DG.Tweening.Sequence crazySequence = DOTween.Sequence();

    public void OnDocterClick()
    {
        this.crazySequence.Kill();
        crazySequence = DOTween.Sequence();
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
        //������easy�̏���������
    }

    public void OnNormalClick()
    {
        //������normal�̏���������
    }

    public void OnHardClick()
    {
        //������hard�̏���������
    }

    public void OnHardcoreClick()
    {
        //������hardcore�̏���������
    }
}
