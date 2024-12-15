using UnityEngine;

public class Skeleton : Monster
{
    public Skeleton(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 100;
        currentHealth = healthPoint;
        attackDamage = 10;
        speed = .6f;
        direction = Direction.LEFT;
        monsterType = MonsterType.SKELETON;
    }
}