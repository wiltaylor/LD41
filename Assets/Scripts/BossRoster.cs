using UnityEngine;

[CreateAssetMenu(fileName = "Roster", menuName = "ScriptObject/Boss Roster", order = 1)]

public class BossRoster : ScriptableObject
{
    public BossData[] Bosses;
}

