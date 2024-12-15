using UnityEngine;

public class Mushroom : Monster
{
    public Mushroom(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 75;
        currentHealth = healthPoint;
        attackDamage = 5;
        speed = .6f;
        direction = Direction.LEFT;
        monsterType = MonsterType.MUSHROOM;
    }
}