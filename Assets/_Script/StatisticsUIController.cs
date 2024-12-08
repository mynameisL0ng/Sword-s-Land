using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatisticsUIController : MonoBehaviour
{
    public TextMeshProUGUI[] statisticsText;
    public Sprite[] characterSourceImage;
    public GameObject[] upgradeButton;
    public Image characterSprite;
    private Animator animator;
    void Start()
    {
        Check_SkillPoint();
        ButtonUpgradeActive(Check_SkillPoint());
        gameObject.SetActive(false);
        Show_Statistics();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Show_Statistics();
    }

    public void Show_Statistics()
    {
        Show_SourceImage();
        statisticsText[0].text = "Class: " + InitPlayer.player.type.ToString();
        statisticsText[1].text = "HP: " + InitPlayer.player.healthPoint.ToString();
        statisticsText[2].text = "STA: " + InitPlayer.player.staminaPoint.ToString();
        statisticsText[3].text = "DMG: " + InitPlayer.player.attackDamage.ToString();
        statisticsText[4].text = "SPE: " + InitPlayer.player.speed.ToString();
        statisticsText[5].text = "STA Regen: " + (InitPlayer.player.staminaRegeneration * 100).ToString() + "%";
        statisticsText[6].text = "Skill Point: " + InitPlayer.player.skillPoint.ToString();
        statisticsText[7].text = "Level: " + InitPlayer.player.levelPoint.ToString();
        if (!Check_SkillPoint()) // skill point = 0
        {
            statisticsText[6].color = new Color32(108, 108, 108, 255);
        }
        else// skill point != 0 ( > 0 )
        {
            statisticsText[6].color = Color.white;
        }
        ButtonUpgradeActive(Check_SkillPoint());
    } 

    public void ButtonUpgradeActive(bool isSkillPoint)
    {
        if (!isSkillPoint)
        {
            for (int i = 0; i < upgradeButton.Length; i++)
            {
                upgradeButton[i].SetActive(false);
            }
        }
        else
        {
            for(int i = 0; i < upgradeButton.Length; i++)
            {
                upgradeButton[i].SetActive(true);
            }
        }
    }

    public bool Check_SkillPoint()
    {
        if (InitPlayer.player.skillPoint == 0)
        {
            return false;
        }

        return true;
    }

    void Show_SourceImage()
    {
        if(InitPlayer.player.type.ToString() == "KNIGHT")
        {
            characterSprite.sprite = characterSourceImage[0];
        }
        else
        {
            characterSprite.sprite = characterSourceImage[1];
        }
    }
    public void Show_StatWindow()
    {
        this.gameObject.SetActive(true);
        UI_Manager.modeUI = true;
        Debug.Log(UI_Manager.modeUI);
    }
    public void Hide_StatWindow()
    {
        gameObject.SetActive(false);
        UI_Manager.modeUI = false;
        Debug.Log(UI_Manager.modeUI);
    }
}
