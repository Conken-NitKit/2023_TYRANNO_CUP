using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tyranno.GameManager;
using Tyranno.GameManagers;
using UniRx;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _seSource;
    
    [SerializeField]
    private AudioSource _bgmSource;
    
    [SerializeField]
    private AudioClip _countDownSE;

    [SerializeField] 
    private AudioClip _mainBGM;
    
    [SerializeField]
    private AudioClip _finishSE;
    
    [SerializeField]
    private AudioClip _stampSE;

    [SerializeField] 
    private AudioClip _correctSE;
    
    [SerializeField] 
    private AudioClip _incorrectSE;
    
    [SerializeField] 
    private AudioClip _resultBGM;
    
    [SerializeField]
    private MainGameManager _mainGameManager;

    [SerializeField] 
    private PuzzleManager _puzzleManager;

    void Start()
    {
        _mainGameManager.CurrentGameState.Subscribe(x =>
        {
            if (x == GameState.Ready)
            {
                _seSource.clip = _countDownSE;
                _seSource.Play();

                _bgmSource.clip = _mainBGM;
                _bgmSource.Play();
            }
            else if (x == GameState.Result)
            {
                _bgmSource.Stop();
                _seSource.clip = _finishSE;
                _seSource.Play();

                _bgmSource.volume = 0f;
                
                _bgmSource.clip = _resultBGM;
                _bgmSource.Play();
                
                DOTween.To 
                (
                    () => _bgmSource.volume, 
                    (x) => _bgmSource.volume = x,	//何を
                    0.1f,		//どこまで(最終的な値)
                    10f		//どれくらいの時間
                );
            }
        });

        _puzzleManager.IsConditionMet.Skip(1).Subscribe(x =>
        {
            if (x)
            {
                _seSource.clip = _correctSE;
                _seSource.Play();
            }
            else
            {
                _seSource.clip = _incorrectSE;
                _seSource.Play();
            }
        });
    }

    public void RingStampSE()
    {
        _bgmSource.Stop();
        _seSource.clip = _stampSE;
        _seSource.Play();
    }
}
