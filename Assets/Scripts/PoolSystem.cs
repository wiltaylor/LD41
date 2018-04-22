using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    private static PoolSystem _instance;

    public static PoolSystem Instance
    {
        get { return _instance; }
    }


    public PoolItem[] Enamies;
    public PoolItem[] Projectiles;

    private Dictionary<GameObject, List<GameObject>> _enemyPools = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, List<GameObject>> _projectilePools = new Dictionary<GameObject, List<GameObject>>();

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        foreach (var entry in Enamies)
        {
            var list = new List<GameObject>();

            for (int i = 0; i < entry.Qty; i++)
            {
                var obj = Instantiate(entry.Item);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                list.Add(obj);
            }

            _enemyPools.Add(entry.Item, list);
        }

        foreach (var entry in Projectiles)
        {
            var list = new List<GameObject>();

            for (int i = 0; i < entry.Qty; i++)
            {
                var obj = Instantiate(entry.Item);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                list.Add(obj);
            }

            _projectilePools.Add(entry.Item, list);
        }
    }

    public GameObject GetProjectile(GameObject prefab)
    {
        if (!_projectilePools.ContainsKey(prefab))
            return null;

        var pool = _projectilePools[prefab];

        return pool.FirstOrDefault(p => !p.activeInHierarchy);
    }

    public GameObject GetEnemy(GameObject prefab)
    {
        if (!_enemyPools.ContainsKey(prefab))
            return null;

        var pool = _enemyPools[prefab];

        return pool.FirstOrDefault(p => !p.activeInHierarchy);
    }

}
