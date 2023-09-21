using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioClip enter1;
    public AudioClip enter2;
    public AudioClip bell;
    [SerializeField] private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEnter1()
    {
        audioSource.PlayOneShot(enter1);
    }

    public void PlayEnter2()
    {
        audioSource.PlayOneShot(enter2);
    }

    public void PlayBell()
    {
        audioSource.PlayOneShot(bell);
    }
}
