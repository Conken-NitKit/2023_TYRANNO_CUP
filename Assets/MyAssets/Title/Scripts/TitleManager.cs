using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject hukidashi = transform.Find("劇的！/title (16)").gameObject;
        GameObject katayaburi = transform.Find("カタヤブーリ先生の").gameObject;
        RectTransform rectTransform = hukidashi.GetComponent<RectTransform>();
        katayaburi.transform.DOLocalMove(new Vector3(5f, 0f, 0f), 3f);
        hukidashi.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutExpo);
        //hukidashi.transform.DOMoveY(6f, 3f).SetEase(Ease.Linear);
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 6f,0.5f).SetEase(Ease.Linear);
        
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
}
