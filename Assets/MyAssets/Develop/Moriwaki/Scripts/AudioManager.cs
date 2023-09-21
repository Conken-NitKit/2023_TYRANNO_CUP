using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip sound1;
    AudioSource audioSource;

    public float delayTime;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("StartBGM", delayTime);
    }

    void StartBGM() 
    {
        //音(sound1)を鳴らす
        audioSource.PlayOneShot(sound1);
    }
}
