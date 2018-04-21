using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ResourceType
{
    PlayerHP,
    PlayerMana,
    BossHP,
    BossMana
}

public class ResourceTracker : MonoBehaviour
{

    public GameObject HartPrefab;
    public ResourceType Type;

    private List<ResourceContainer> _containers = new List<ResourceContainer>();
    

    void Update ()
	{
	    var qty = Type == ResourceType.PlayerHP ? Mathf.FloorToInt(PlayerStats.Instance.HP) :
	        Type == ResourceType.PlayerMana ? Mathf.FloorToInt(PlayerStats.Instance.Mana) :
	        Type == ResourceType.BossHP ? Mathf.FloorToInt(BossStats.Instance.HP) : Mathf.FloorToInt(BossStats.Instance.Mana);

	    var maxqty = Type == ResourceType.PlayerHP ? Mathf.FloorToInt(PlayerStats.Instance.MaxHP) :
	        Type == ResourceType.PlayerMana ? Mathf.FloorToInt(PlayerStats.Instance.MaxMana) :
	        Type == ResourceType.BossHP ? Mathf.FloorToInt(BossStats.Instance.MaxHP) : Mathf.FloorToInt(BossStats.Instance.MaxMana);

        if (_containers.Count != maxqty)
	        UpdateContainers(maxqty);

	    if (qty > maxqty)
	        qty = maxqty;

        for (var i = 0; i < qty; i++)
	    {
            _containers[i].SetContainer(true);
	    }

        for(var i = qty; i < _containers.Count; i++)
            _containers[i].SetContainer(false);

	}

    void UpdateContainers(int target)
    {
        while (_containers.Count != target)
        {
            if (_containers.Count > target)
            {
                var container = _containers.Last();
                Destroy(container.gameObject);
                _containers.Remove(container);

            }else if (_containers.Count < target)
            {
                var newcontainer = Instantiate(HartPrefab);
                newcontainer.transform.SetParent(transform);
                newcontainer.SetActive(true);
                _containers.Add(newcontainer.GetComponent<ResourceContainer>());
            }
        }
    }
}
