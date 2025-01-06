using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public void MoveToLoadScene_Event()
    {
        ApplicationVariables.LoadingSceneName = "GamePlay";
        SceneManager.LoadScene("LoadingScene");
    }
}
