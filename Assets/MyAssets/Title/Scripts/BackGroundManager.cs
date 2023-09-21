using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image.material.mainTextureOffset = new Vector2(0, 0);
        image.material.DOOffset(new Vector2(x, y), 10f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        image.material.mainTextureOffset = new Vector2(0, 0);
    }
}
