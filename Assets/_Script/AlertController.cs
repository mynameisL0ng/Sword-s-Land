using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AlertController : MonoBehaviour
{
    public static AlertController instance {  get; private set; }
    public GameObject alertPrefab;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void CreateAlert(string text)
    {
        GameObject alertIntance = Instantiate(alertPrefab);
        Transform alertGroup = alertIntance.transform.Find("Alert Group");
        alertGroup.GetComponentInChildren<TextMeshProUGUI>().text = text;
        Destroy(alertIntance,2.11f);
    }
}
