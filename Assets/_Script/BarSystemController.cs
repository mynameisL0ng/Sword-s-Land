using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarSystemController : MonoBehaviour
{
    [SerializeField] private Image healthFillImg;
    [SerializeField] private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthFillImg.fillAmount = InitPlayer.player.currentHealth / InitPlayer.player.healthPoint;
        healthText.text = InitPlayer.player.currentHealth.ToString() + "/" + InitPlayer.player.healthPoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthFillAmmount(InitPlayer.player.currentHealth, InitPlayer.player.healthPoint);
    }
    private void UpdateHealthFillAmmount(float healthCurrentPoint, float heathMaxPoint)
    {
        healthFillImg.fillAmount = healthCurrentPoint / heathMaxPoint;
        healthText.text = InitPlayer.player.currentHealth.ToString() + "/" + InitPlayer.player.healthPoint.ToString();
    }
}
