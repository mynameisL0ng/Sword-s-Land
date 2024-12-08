using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsUpgrade : MonoBehaviour
{
    StatisticsUIController statisticsUIController;
    public void HealthUpgrade()
    {
        InitPlayer.player.healthPoint += 15;
        InitPlayer.player.skillPoint -= 1;
    }
    public void StaminaUpgrade()
    {
        InitPlayer.player.staminaPoint += 10;
        InitPlayer.player.skillPoint -= 1;
    }
    public void DamageUpgrade()
    {
        InitPlayer.player.attackDamage += 5;
        InitPlayer.player.skillPoint -= 1;
    }
    public void SpeedUpgrade()
    {
        InitPlayer.player.speed += 2;
        InitPlayer.player.skillPoint -= 1;
    }
    public void StaminaRegenUpgrade()
    {
        InitPlayer.player.staminaRegeneration += 0.002f;
        InitPlayer.player.skillPoint -= 1;
    }
}
