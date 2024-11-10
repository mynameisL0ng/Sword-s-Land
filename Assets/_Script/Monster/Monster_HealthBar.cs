using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_HealthBar : MonoBehaviour
{
    public Image healthFillImage;
    public InitMonster initMonster;
    public MonsterController monsterController;
    public GameObject healthBar;
    void Start()
    {
        healthBar.SetActive(false);
        initMonster = GetComponentInParent<InitMonster>();
        FillBarAmount(initMonster.monster.currentHealth, initMonster.monster.healthPoint);
    }

    void Update()
    {
        if(monsterController.hitPlayer.collider != null) 
            healthBar.SetActive(true);
        else 
            healthBar.SetActive(false);
        FillBarAmount(initMonster.monster.currentHealth, initMonster.monster.healthPoint);

    }
    void FillBarAmount(float min, float max)
    {
        healthFillImage.fillAmount = min / max;
    }
}
