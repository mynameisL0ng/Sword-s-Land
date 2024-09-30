using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFeature : MonoBehaviour
{
    [SerializeField] GameObject characterSelectScreen;
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject settingScreen;
    [SerializeField] GameObject escScreen;
    bool escScreenActive = false;
    private void Start()
    {
        characterSelectScreen.SetActive(false);
        mainScreen.SetActive(true);
        settingScreen.SetActive(false);
        escScreen.SetActive(escScreenActive);

    }
    private void Update()
    {
        EscScreenActive();
    }
    private void EscScreenActive()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(escScreenActive == false)
            {
                escScreenActive = true;
                escScreen.SetActive(escScreenActive);
            }
            else
            {
                escScreenActive = false;
                escScreen.SetActive(escScreenActive);
            }
        }
    }
    public void OnPlayBtn()
    {
        characterSelectScreen.SetActive(true);
        mainScreen.SetActive(false);
    }
    public void OnSettingBtn()
    {
        mainScreen.SetActive(false);
        settingScreen.SetActive(true);
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
