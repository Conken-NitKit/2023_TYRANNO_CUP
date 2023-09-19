using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class hoge : MonoBehaviour
{
    private const int SquareSize = 3;

    public BoolReactiveProperty[,] hoges = new BoolReactiveProperty[SquareSize,SquareSize];

    public bool[,] SquareArray = new bool[SquareSize, SquareSize];

    [SerializeField] 
    private ChildButtonArray[] buttons;

    [SerializeField] 
    private ChildHoge2Array[] _hoge2Arrays;

    [SerializeField]
    private ChildImageArray[] Arrays;

    void Start()
    {
        SetAllElementsReactiveProperty(false);

        ToArray();

        for (int i = 0; i < SquareSize; i++)
        {
            for (int j = 0; j < SquareSize; j++)
            {
                var a = i;
                var b = j;

                hoges[i,j].Subscribe(x =>
                {
                    SquareArray[a, b] = x;
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
        
        for (int i = 0; i < SquareSize; i++)
        {
            for (int j = 0; j < SquareSize; j++)
            {
                var a = i;
                var b = j;

                buttons[i].childArray[j].onClick.AsObservable().Subscribe(_ =>
                {
                    SetValue(_hoge2Arrays[a].childArray[b].ind, _hoge2Arrays[a].childArray[b].jnd);
                }).AddTo(gameObject);
                
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

    private void ToArray()
    {
        for (int i = 0; i < SquareSize; i++)
        {
            for (int j = 0; j < SquareSize; j++)
            {
                var a = i;
                var b = j;

                SquareArray[a, b] = hoges[a, b].Value;
            }
        } 
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
public class ChildImageArray
{
    public Image[] childArray;
}

[System.Serializable]
public class ChildHoge2Array
{
    public hoge2[] childArray;
}

[System.Serializable]
public class ChildButtonArray
{
    public Button[] childArray;
}
