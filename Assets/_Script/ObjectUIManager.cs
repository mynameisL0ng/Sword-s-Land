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
        InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SavePlayerPos();
    }

    void SavePlayerPos()
    {
        PlayerPrefs.SetFloat("X", InitPlayer.player.transform.position.x);
        PlayerPrefs.SetFloat("Y", InitPlayer.player.transform.position.y);
        PlayerPrefs.SetFloat("Z", InitPlayer.player.transform.position.z);
    }
}
