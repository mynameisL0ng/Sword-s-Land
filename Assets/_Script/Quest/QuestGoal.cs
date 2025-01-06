using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGoal
{
    public int requireAmount;
    public int currentAmount;
    public bool IsReached()
    {
        return currentAmount >= requireAmount;
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.KILL)
        {
            currentAmount++;
        }
    }
    public void ItemCollected()
    {
        if (goalType == GoalType.GATHER)
        {
            currentAmount++;
        }
    }

    public enum GoalType { KILL, GATHER }
    public GoalType goalType;

    public enum KillType {GOBLIN, FLYINGEYE, MUSHROOM, SKELETON, MEDIEVAL_KING}
    public KillType killType;
}
