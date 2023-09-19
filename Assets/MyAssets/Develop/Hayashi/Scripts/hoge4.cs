using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class hoge4 : MonoBehaviour
{
    [SerializeField]
    private hoge _hoge;

    private char[,] array = new char[3, 3];
    
    public void Clicked()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_hoge.SquareArray[i, j])
                {
                    array[i,j] = '○';
                }
                else
                {
                    array[i,j] = '×';
                }
            }
        }
        Debug.Log($"{array[0,0]},{array[0,1]},{array[0,2]}\n{array[1,0]},{array[1,1]},{array[1,2]}\n{array[2,0]},{array[2,1]},{array[2,2]}");
    }
}
