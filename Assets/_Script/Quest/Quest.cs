using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;
    public float experienceReward;
    public int masteryReward;
    
    public void QuestComplete()
    {
        AlertController.instance.CreateAlert("Quest Completed");
        isActive = false;
    }

    public QuestGoal goal;
}
