using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatFountain : MonoBehaviour
{
    public float TimeOut;
    void OnEnable()
    {
        Invoke("SleepMe", TimeOut);
    }

    public void SleepMe()
    {
        gameObject.SetActive(false);
    }
}
