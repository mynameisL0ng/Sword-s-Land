using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    private void Start()
    {
        CreateData();
        LoadData();
    }
    private void Update()
    {
        StartCoroutine(AutoSave());
    }
    IEnumerator AutoSave()
    {
        while (true)
        {
            SaveData();
            yield return new WaitForSeconds(3f);
        }
    }
    void CreateData()
    {
        if (InitPlayer.isKnight)
        {
            if (!PlayerPrefs.HasKey("K_Level") && !PlayerPrefs.HasKey("K_Health") && !PlayerPrefs.HasKey("K_Stamina") && !PlayerPrefs.HasKey("K_staminaRegeneration")
                && !PlayerPrefs.HasKey("K_AttackDamage") && !PlayerPrefs.HasKey("K_SkillPoint") && !PlayerPrefs.HasKey("K_CurrentEXP")
                && !PlayerPrefs.HasKey("K_ExpRequire") && !PlayerPrefs.HasKey("K_Speed"))
            {
                LoadData();
                Debug.Log("Create");
            }
        }
        else
        {
            if (!PlayerPrefs.HasKey("W_Level") && !PlayerPrefs.HasKey("W_Health") && !PlayerPrefs.HasKey("W_Stamina") && !PlayerPrefs.HasKey("W_staminaRegeneration")
                && !PlayerPrefs.HasKey("W_AttackDamage") && !PlayerPrefs.HasKey("W_SkillPoint") && !PlayerPrefs.HasKey("W_CurrentEXP")
                && !PlayerPrefs.HasKey("W_ExpRequire") && !PlayerPrefs.HasKey("W_Speed"))
            {
                LoadData();
                Debug.Log("Create");
            }
        }
    }

    public void SaveData()
    {
        if (InitPlayer.isKnight)
        {
            PlayerPrefs.SetInt("K_Level", InitPlayer.player.levelPoint);
            PlayerPrefs.SetFloat("K_Health", InitPlayer.player.healthPoint);
            PlayerPrefs.SetFloat("K_Stamina", InitPlayer.player.staminaPoint);
            PlayerPrefs.SetFloat("K_staminaRegeneration", InitPlayer.player.staminaRegeneration);
            PlayerPrefs.SetFloat("K_AttackDamage", InitPlayer.player.attackDamage);
            PlayerPrefs.SetFloat("K_SkillPoint", InitPlayer.player.skillPoint);
            PlayerPrefs.SetFloat("K_CurrentEXP", InitPlayer.player.currentEXP);
            PlayerPrefs.SetFloat("K_ExpRequire", InitPlayer.player.expRequire);
            PlayerPrefs.SetFloat("K_Speed", InitPlayer.player.speed);
        }
        else if (InitPlayer.isWarrior)
        {
            PlayerPrefs.SetInt("W_Level", InitPlayer.player.levelPoint);
            PlayerPrefs.SetFloat("W_Health", InitPlayer.player.healthPoint);
            PlayerPrefs.SetFloat("W_Stamina", InitPlayer.player.staminaPoint);
            PlayerPrefs.SetFloat("W_staminaRegeneration", InitPlayer.player.staminaRegeneration);
            PlayerPrefs.SetFloat("W_AttackDamage", InitPlayer.player.attackDamage);
            PlayerPrefs.SetFloat("W_SkillPoint", InitPlayer.player.skillPoint);
            PlayerPrefs.SetFloat("W_CurrentEXP", InitPlayer.player.currentEXP);
            PlayerPrefs.SetFloat("W_ExpRequire", InitPlayer.player.expRequire);
            PlayerPrefs.SetFloat("W_Speed", InitPlayer.player.speed);
        }
    }

    public void LoadData()
    {
        if (InitPlayer.player != null)
        {
            if (InitPlayer.isKnight)
            {
                if (PlayerPrefs.HasKey("K_Level") && PlayerPrefs.HasKey("K_Health") && PlayerPrefs.HasKey("K_Stamina") && PlayerPrefs.HasKey("K_staminaRegeneration")
                && PlayerPrefs.HasKey("K_AttackDamage") && PlayerPrefs.HasKey("K_SkillPoint") && PlayerPrefs.HasKey("K_CurrentEXP")
                && PlayerPrefs.HasKey("K_ExpRequire") && PlayerPrefs.HasKey("K_Speed"))
                {
                    InitPlayer.player.levelPoint = PlayerPrefs.GetInt("K_Level");
                    InitPlayer.player.staminaRegeneration = PlayerPrefs.GetFloat("K_staminaRegeneration");
                    InitPlayer.player.skillPoint = PlayerPrefs.GetFloat("K_SkillPoint");
                    InitPlayer.player.currentEXP = PlayerPrefs.GetFloat("K_CurrentEXP");
                    InitPlayer.player.expRequire = PlayerPrefs.GetInt("K_Level") * 250;
                    LoadKnight();
                }
            }
            else if (InitPlayer.isWarrior)
            {
                if (PlayerPrefs.HasKey("W_Level") && PlayerPrefs.HasKey("W_Health") && PlayerPrefs.HasKey("W_Stamina") && PlayerPrefs.HasKey("W_staminaRegeneration")
                && PlayerPrefs.HasKey("W_AttackDamage") && PlayerPrefs.HasKey("W_SkillPoint") && PlayerPrefs.HasKey("W_CurrentEXP")
                && PlayerPrefs.HasKey("W_ExpRequire") && PlayerPrefs.HasKey("W_Speed"))
                {
                    InitPlayer.player.levelPoint = PlayerPrefs.GetInt("W_Level");
                    InitPlayer.player.staminaRegeneration = PlayerPrefs.GetFloat("W_staminaRegeneration");
                    InitPlayer.player.skillPoint = PlayerPrefs.GetFloat("W_SkillPoint");
                    InitPlayer.player.currentEXP = PlayerPrefs.GetFloat("W_CurrentEXP");
                    InitPlayer.player.expRequire = PlayerPrefs.GetInt("W_Level") * 250;
                    LoadWarrior();
                }
            }
            else
            {
                InitPlayer.player.levelPoint = 1;
                InitPlayer.player.staminaRegeneration = 0.02f;
                InitPlayer.player.skillPoint = 0;
                InitPlayer.player.currentEXP = 0;
                InitPlayer.player.expRequire = 250;
                if (InitPlayer.isWarrior)
                {
                    LoadWarrior();
                }
                else
                {
                    LoadKnight();
                }
            }
        }
    }

    void LoadKnight()
    {
        if (InitPlayer.isKnight)
        {
            if (PlayerPrefs.HasKey("K_Health") && PlayerPrefs.HasKey("K_Stamina")
                && PlayerPrefs.HasKey("K_AttackDamage") && PlayerPrefs.HasKey("K_Speed"))
            {
                InitPlayer.player.healthPoint = PlayerPrefs.GetFloat("K_Health");
                InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;

                InitPlayer.player.staminaPoint = PlayerPrefs.GetFloat("K_Stamina");
                InitPlayer.player.currentStamina = InitPlayer.player.staminaPoint;

                InitPlayer.player.attackDamage = PlayerPrefs.GetFloat("K_AttackDamage");
                InitPlayer.player.speed = PlayerPrefs.GetFloat("K_Speed");
            }
            else
            {
                InitPlayer.player.healthPoint = 120;
                InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;

                InitPlayer.player.staminaPoint = 50;
                InitPlayer.player.currentStamina = InitPlayer.player.staminaPoint;

                InitPlayer.player.attackDamage = 20;
                InitPlayer.player.speed = 4;
            }
        }
    }
    void LoadWarrior()
    {
        if (InitPlayer.isWarrior)
        {
            if (PlayerPrefs.HasKey("W_Health") && PlayerPrefs.HasKey("W_Stamina")
                && PlayerPrefs.HasKey("W_AttackDamage") && PlayerPrefs.HasKey("W_Speed"))
            {
                InitPlayer.player.healthPoint = PlayerPrefs.GetFloat("W_Health");
                InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;

                InitPlayer.player.staminaPoint = PlayerPrefs.GetFloat("W_Stamina");
                InitPlayer.player.currentStamina = InitPlayer.player.staminaPoint;

                InitPlayer.player.attackDamage = PlayerPrefs.GetFloat("W_AttackDamage");
                InitPlayer.player.speed = PlayerPrefs.GetFloat("W_Speed");
            }
            else
            {
                InitPlayer.player.healthPoint = 100;
                InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;

                InitPlayer.player.staminaPoint = 30;
                InitPlayer.player.currentStamina = InitPlayer.player.staminaPoint;

                InitPlayer.player.attackDamage = 25;
                InitPlayer.player.speed = 6;
            }
        }
    }
    public void SaveStatistics(string nameStatistics, float statistics, float skillPoint)
    {
        if(InitPlayer.isKnight)
            PlayerPrefs.SetFloat("K_" + nameStatistics, statistics);
        else
            PlayerPrefs.SetFloat("W_" + nameStatistics, statistics);

        if (InitPlayer.isKnight)
            PlayerPrefs.SetFloat("K_SkillPoint", skillPoint);
        else
            PlayerPrefs.SetFloat("W_SkillPoint", skillPoint);
        SaveData();
    }

    public void Open(GameObject gameObject)
    {
        gameObject.SetActive(true);
        UI_Manager.modeUI = true;
    }
    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
        UI_Manager.modeUI = false;
    }
    public void BackToCharacterScene()
    {
        UI_Manager.modeUI = false;
        SaveData();
        ApplicationVariables.LoadingSceneName = "CharacterSelection";
        SceneManager.LoadScene("LoadingScene");
    }
}
