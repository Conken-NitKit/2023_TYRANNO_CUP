using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hoge2 : MonoBehaviour
{
    [SerializeField] 
    private hoge Hoge;

    [SerializeField]
    private int i, j;

    public void Clicked()
    {
        Hoge.SetValue(i, j);
    }
}
