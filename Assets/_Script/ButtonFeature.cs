using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ButtonFeature : MonoBehaviour
{
    public GameObject characterSelectScreen;
    public GameObject mainScreen;
    public GameObject settingScreen;

    public AudioSource audioSourceSetting;
    bool escSettingScreen = false;
    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMusic_Volume"))
        {
            audioSourceSetting.volume = PlayerPrefs.GetFloat("BGMusic_Volume");
        }
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
        mainScreen.SetActive(false);
        characterSelectScreen.SetActive(true);
    }
    public void OnSettingBtn()
    {
        escSettingScreen = true;
        mainScreen.SetActive(!escSettingScreen);
        settingScreen.SetActive(escSettingScreen);
    }
    public void EscSettingKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escSettingScreen = !escSettingScreen;
            mainScreen.SetActive(!escSettingScreen);
            settingScreen.SetActive(escSettingScreen);
            if(!escSettingScreen)
                PlayerPrefs.SetFloat("BGMusic_Volume", audioSourceSetting.volume);
        }
    }
    public void OnExitSettingScreenBtn()
    {
        escSettingScreen = false;
        mainScreen.SetActive(!escSettingScreen);
        settingScreen.SetActive(escSettingScreen);
        PlayerPrefs.SetFloat("BGMusic_Volume", audioSourceSetting.volume);
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
