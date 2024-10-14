using UnityEngine;

public class Skeleton : Monster
{
    public Skeleton(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 130;
        currentHealth = healthPoint;
        attackDamage = 12;
        speed = .6f;
        direction = Direction.LEFT;
    }
}