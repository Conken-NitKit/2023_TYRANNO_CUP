using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageShake : MonoBehaviour
{
    void OnEnable()
    {
        this.GetComponent<Image>().rectTransform.DOShakePosition(2f, 5f, 100, 1, false, true);
    }
}
