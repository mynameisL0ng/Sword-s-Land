using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsUpgrade : MonoBehaviour
{
    public DataController dataController;
    private void Awake()
    {
        dataController = FindFirstObjectByType<DataController>();
    }
    public void HealthUpgrade()
    {
        InitPlayer.player.healthPoint += 15;
        InitPlayer.player.skillPoint -= 1;
        InitPlayer.player.currentHealth = InitPlayer.player.healthPoint;
        dataController.SaveStatistics("Health", InitPlayer.player.healthPoint, InitPlayer.player.skillPoint);
    }
    public void StaminaUpgrade()
    {
        InitPlayer.player.staminaPoint += 10;
        InitPlayer.player.skillPoint -= 1;
        InitPlayer.player.currentStamina = InitPlayer.player.staminaPoint;
        dataController.SaveStatistics("Stamina", InitPlayer.player.staminaPoint, InitPlayer.player.skillPoint);
    }
    public void DamageUpgrade()
    {
        InitPlayer.player.attackDamage += 5;
        InitPlayer.player.skillPoint -= 1;
        dataController.SaveStatistics("AttackDamage", InitPlayer.player.attackDamage, InitPlayer.player.skillPoint);
    }
    public void SpeedUpgrade()
    {
        InitPlayer.player.speed += 2;
        InitPlayer.player.skillPoint -= 1;
        dataController.SaveStatistics("Speed", InitPlayer.player.speed, InitPlayer.player.skillPoint);
    }
    public void StaminaRegenUpgrade()
    {
        InitPlayer.player.staminaRegeneration += 0.002f;
        InitPlayer.player.skillPoint -= 1;
        dataController.SaveStatistics("staminaRegeneration", InitPlayer.player.healthPoint, InitPlayer.player.skillPoint);
    }
}
