using System.Collections;
using System.Collections.Generic;
using UniRx;
using Tyranno.GameManager;
using UnityEngine;

public class hoge5 : MonoBehaviour
{
    [SerializeField]
    private TimeManager _timeManager;

    void Start()
    {
        _timeManager.ReadySecond.Subscribe(x =>
        {
            if (x <= 0)
            {
                _timeManager.StartGameCountUp();
            }
            Debug.Log($"Ready : {x}");
        });

        _timeManager.GameSecond.Subscribe(x =>
        {
            //Debug.Log($"NowTime : {x}");
        });
    }
}
