using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFeature : MonoBehaviour
{
    [SerializeField] GameObject characterSelectScreen;
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject settingScreen;
    bool escSettingActive = false;
    private void Start()
    {
        characterSelectScreen.SetActive(false);
        mainScreen.SetActive(true);
        settingScreen.SetActive(false);

    }
    private void Update()
    {
        EscSettingKey();
    }

    public void OnPlayBtn()
    {
        characterSelectScreen.SetActive(true);
        mainScreen.SetActive(false);
    }
    public void OnSettingBtn()
    {
        escSettingActive = true;
        mainScreen.SetActive(false);
        settingScreen.SetActive(true);
    }
    public void EscSettingKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                escSettingActive = !escSettingActive;
                mainScreen.SetActive(!escSettingActive);
                settingScreen.SetActive(escSettingActive);
        }
    }
    public void OnExitSettingScreenBtn()
    {
        mainScreen.SetActive(true);
        settingScreen.SetActive(false);
    }
    public void OnBackToMainScreenBtn()
    {
        characterSelectScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    public void OnQuitBtn()
    {
        Application.Quit();
    }
}
