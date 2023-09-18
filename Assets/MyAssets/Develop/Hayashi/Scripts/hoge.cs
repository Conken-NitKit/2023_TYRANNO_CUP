using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class hoge : MonoBehaviour
{
    private const int SquareSize = 3;

    private BoolReactiveProperty[,] hoges = new BoolReactiveProperty[SquareSize,SquareSize];

    [SerializeField]
    private ChildArray[] Arrays;

    void Start()
    {
        SetAllElementsReactiveProperty(false);
        
        
        for (int i = 0; i < SquareSize; i++)
        {
            for (int j = 0; j < SquareSize; j++)
            {
                var a = i;
                var b = j;
                hoges[i,j].Subscribe(x =>
                {
                    if (x)
                    {
                        Arrays[a].childArray[b].color = Color.black;
                    }
                    else
                    {
                        Arrays[a].childArray[b].color = Color.white;
                    }
                }).AddTo(gameObject);
            }
        } 
    }

    public void hage()
    {
        SetAllElements(true);
    }
    
    private void SetAllElementsReactiveProperty(bool x)
    {
        for (int i = 0; i < SquareSize; i++)
        {
            for (int j = 0; j < SquareSize; j++)
            {
                hoges[i,j] = new BoolReactiveProperty(x);
            }
        } 
    }

    private void SetAllElements(bool x)
    {
        for (int i = 0; i < SquareSize; i++)
        {
            for (int j = 0; j < SquareSize; j++)
            {
                hoges[i, j].Value = x;
            }
        } 
    }
    
    public void SetValue(int i, int j)
    {
        hoges[i,j].Value = !hoges[i,j].Value;
    }
}


[System.Serializable]
public class ChildArray
{
    public Image[] childArray;
}