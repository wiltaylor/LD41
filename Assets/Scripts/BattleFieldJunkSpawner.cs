﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BattleFieldJunkSpawner : MonoBehaviour
{
    public BattleFieldJunkItem[] Junk;
    public float MinX;
    public float MaxX;
    public float MinZ;
    public float MaxZ;
    public float Y;
    public LayerMask CheckMask;

    void Start()
    {
        var infloopcheck = 0;

        foreach (var item in Junk)
        {
            var qty = Random.Range(item.Min, item.Max);

            for (int i = 0; i < qty; i++)
            {
                float x;
                float z;

                infloopcheck = 0;

                while (true)
                {
                    infloopcheck++;
                    x = Random.Range(MinX, MaxX);
                    z = Random.Range(MinZ, MaxZ);

                    if (!Physics.CheckSphere(new Vector3(x, Y, z), item.CheckRadius, CheckMask))
                        break;

                    if(infloopcheck > 1000000)
                        throw new ApplicationException("Infinite loop protection triggered");
                }

                var angle = Random.Range(0, 360f);



                var junk = Instantiate(item.Prefab);
                junk.transform.position = new Vector3(x, Y, z);
                junk.transform.Rotate(Vector3.up, angle);
                junk.SetActive(true);
                junk.transform.SetParent(transform);
            }
        }
    }
}
