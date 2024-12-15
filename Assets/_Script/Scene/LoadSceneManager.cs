using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }
    IEnumerator LoadAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ApplicationVariables.LoadingSceneName);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            loadingText.text = "Loading... " + Mathf.RoundToInt(asyncLoad.progress * 100);
            yield return null;
        }
        loadingText.text = "Loading... 100%";
        yield return new WaitForSeconds(2);
        asyncLoad.allowSceneActivation = true;
    }
}
