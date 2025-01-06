using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectUIManager : MonoBehaviour
{
    private void Awake()
    {
        UI_Manager.modeUI = false;
    }
    public void ButtonClose(GameObject gameObject)
    {
        gameObject.SetActive(false);
        UI_Manager.modeUI = false;
    }
    public void RestButton()
    {
        ActiveSavePoint();
        InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;
        ApplicationVariables.LoadingSceneName = "GamePlay";
        SceneManager.LoadScene("LoadingScene");
    }

    void ActiveSavePoint()
    {
        if(InitPlayer.isKnight)
        {
            PlayerPrefs.SetFloat("K_SavePoint1_X", InitPlayer.player.transform.position.x);
            PlayerPrefs.SetFloat("K_SavePoint1_Y", InitPlayer.player.transform.position.y);
        }
        else
        {
            PlayerPrefs.SetFloat("W_SavePoint1_X", InitPlayer.player.transform.position.x);
            PlayerPrefs.SetFloat("W_SavePoint1_Y", InitPlayer.player.transform.position.y);
        }
    }
}
