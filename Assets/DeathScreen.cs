using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
	void Update ()
	{
	    if (PlayerStats.Instance.HP <= 0)
	    {
	        for (int i = 0; i < transform.childCount; i++)
	            transform.GetChild(i).gameObject.SetActive(true);

	    }

	}

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
