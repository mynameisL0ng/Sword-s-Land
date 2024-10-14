using UnityEngine;

public class Mushroom : Monster
{
    public Mushroom(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 110;
        currentHealth = healthPoint;
        attackDamage = 13;
        speed = .6f;
        direction = Direction.LEFT;
    }
}