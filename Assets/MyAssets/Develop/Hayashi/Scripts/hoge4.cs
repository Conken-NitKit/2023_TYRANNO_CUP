using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class hoge4 : MonoBehaviour
{
    [SerializeField]
    private hoge _hoge;
    
    public void Clicked()
    {
        var array = _hoge.SquareArray;
        Debug.Log($"{array[0,0]},{array[0,1]},{array[0,2]}\n{array[1,0]},{array[1,1]},{array[1,2]}\n{array[2,0]},{array[2,1]},{array[2,2]}");
    }
}
