using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatFountain : MonoBehaviour
{
    public float TimeOut;
    public AudioSource SquishSound;
    
    void OnEnable()
    {
        Invoke("SleepMe", TimeOut);
        SquishSound.Play();
    }

    public void SleepMe()
    {
        gameObject.SetActive(false);
    }
}
