using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public enum TypeNPC { Quest, Talk}
    public TypeNPC Type;
    bool inZone;
    public GameObject eKey;

    public GameObject npcDialog; // UI Dialog
    public string[] npcLine;
    public TextMeshProUGUI showLines;
    public string[] choiceLines;
    public TextMeshProUGUI[] showChoiceText;
    public GameObject buttonGroup;

    SpriteRenderer spriteRenderer; // source npcIcon
    public Image npcIcon;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inZone = false;
        npcDialog.SetActive(false);
        eKey.SetActive(inZone);
    }
    private void Update()
    {
        if(inZone)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SetupDialog();
                UI_Manager.modeUI = true;
            }
        }
        QuestLine();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!inZone)
        {
            inZone = true;
            eKey.SetActive(inZone);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inZone = false;
        eKey.SetActive(inZone);
    }
    void SetupDialog()
    {
        npcDialog.SetActive(inZone);
        npcIcon.sprite = spriteRenderer.sprite;
        
        if (Type == TypeNPC.Quest)
        {
            QuestNPC();
        }
        else
        {
            TalkNPC();
        }
    }
    void QuestLine()
    {
        if (!InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.isActive)
        {
            showLines.text = npcLine[0];
        }
        else
        {
            buttonGroup.SetActive(false);
            showLines.text = npcLine[1];
        }
    }
    void QuestNPC()
    {
        buttonGroup.SetActive(true);
        for (int i = 0; i < choiceLines.Length; i++)
        {
            showChoiceText[i].text = choiceLines[i];
        }
    }
    void TalkNPC()
    {
        buttonGroup.SetActive(false);
    }
}
