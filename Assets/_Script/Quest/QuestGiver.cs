using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerController player;

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI requireText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI masteryText;

    private void Start()
    {
        questWindow.SetActive(false);
        player = FindAnyObjectByType<PlayerController>();
    }

    public void OpenQuestWindow()
    {
        UI_Manager.modeUI = true;
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        requireText.text = quest.goal.currentAmount.ToString() + "/" + quest.goal.requireAmount.ToString() + " Goblin";
        experienceText.text = "Experience\n" + quest.experienceReward.ToString();
        masteryText.text = "Mastery\n" + quest.masteryReward.ToString();
    }
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        UI_Manager.modeUI = false;
        quest.isActive = true;
        player.quest = quest;
    }
    public void CloseQuest()
    {
        questWindow.SetActive(false);
        UI_Manager.modeUI = false;
    }
}
