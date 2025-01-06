using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HealthBar : MonoBehaviour
{
    public Image healthFillImage;
    public TextMeshProUGUI bossName;
    public InitBoss initBoss;
    public BossController bossController;
    public GameObject healthBar;
    void Start()
    {
        bossName.text = transform.parent.name;
        healthBar.SetActive(false);
        initBoss = GetComponentInParent<InitBoss>();
        FillBarAmount(initBoss.boss.currentHealth, initBoss.boss.healthPoint);
    }

    void Update()
    {
        if (bossController.hitPlayer.collider != null && !initBoss.boss.Death)
            healthBar.SetActive(true);
        else
            healthBar.SetActive(false);
        FillBarAmount(initBoss.boss.currentHealth, initBoss.boss.healthPoint);
    }
    void FillBarAmount(float min, float max)
    {
        healthFillImage.fillAmount = min / max;
    }
}
