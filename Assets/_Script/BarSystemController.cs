using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarSystemController : MonoBehaviour
{
    public Image fillHealth;
    public TextMeshProUGUI healthTextAmount;

    public Image fillExp;
    public TextMeshProUGUI expTextAmount;

    public Image fillStamina;

    // Start is called before the first frame update
    void Start()
    {
        InitializedBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFillAmount_Text(fillHealth, healthTextAmount, InitPlayer.player.currentHealth, InitPlayer.player.healthPoint); // health
        UpdateFillAmount_Text(fillExp, expTextAmount, InitPlayer.player.currentEXP, InitPlayer.player.expRequire); // exp
        UpdateFillAmount(fillStamina, InitPlayer.player.currentStamina, InitPlayer.player.staminaPoint); // stamina
    }

    void InitializedBar()
    {
        // health bar
        fillHealth.fillAmount = InitPlayer.player.currentHealth / InitPlayer.player.healthPoint;
        healthTextAmount.text = InitPlayer.player.currentHealth.ToString() + "/" + InitPlayer.player.healthPoint.ToString();

        // exp bar
        fillExp.fillAmount = InitPlayer.player.currentEXP / InitPlayer.player.expRequire;
        expTextAmount.text = InitPlayer.player.currentEXP.ToString() + "/" + InitPlayer.player.expRequire.ToString();

        // stamina bar
        fillStamina.fillAmount = InitPlayer.player.currentStamina / InitPlayer.player.staminaPoint;
    }

    private void UpdateFillAmount_Text(Image image, TextMeshProUGUI text , float min, float max) // Fill Amount and text UI
    {
        image.fillAmount = min / max;
        text.text = min.ToString() + "/" + max.ToString();
    }
    private void UpdateFillAmount(Image image, float min, float max) // Full amount only
    {
        image.fillAmount = min / max;
    }
}
