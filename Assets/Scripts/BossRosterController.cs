using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRosterController : MonoBehaviour
{
    public BossRoster Roster;
    public Color DialogueColour;

    private int _bossIndex = -1;
	void Update ()
	{
	    if (BossController.Instance.CurrentBoss != null)
	        return;

	    if (!TextMessageSystem.Instance.isEmpty)
	        return;

	    var nextBoss = GetNextBoss();

	    if (nextBoss == null)
	    {
            SceneManager.LoadScene("Credits");
	        return;
	    }

        BossController.Instance.SetNewBoss(nextBoss);

        TextMessageSystem.Instance.Clear();

        foreach(var item in nextBoss.IntroText)
            TextMessageSystem.Instance.AddMessage(item.Text, item.Time, DialogueColour);
	}

    BossData GetNextBoss()
    {
        _bossIndex++;

        if (_bossIndex >= Roster.Bosses.Length)
            return null;

        return Roster.Bosses[_bossIndex];
    }
}
