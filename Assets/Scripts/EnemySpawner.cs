using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private static EnemySpawner _instance;

    public static EnemySpawner Instance
    {
        get { return _instance; }
    }

    public float MinDistance;
    public float MaxDistance;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
    
    public void Spawn(GameObject prefab, int qty)
    {
        for (int i = 0; i < qty; i++)
        {
            var newEnemy = PoolSystem.Instance.GetEnemy(prefab);

            if (newEnemy == null) //hit unit cap drop out.
                return;

            var range = Random.Range(MinDistance, MaxDistance);
            var angle = Random.Range(0f, 360f);
            var rot = Quaternion.AngleAxis(angle, Vector3.up);
            var direction = rot * Vector3.forward;

            var newpos = transform.position + direction * range;

            newEnemy.transform.position = newpos;

            newEnemy.SetActive(true);

        }
    }
}
