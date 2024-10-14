using UnityEngine;

public class Goblin : Monster
{
    public Goblin(GameObject gameObject) :base(gameObject)
    {
        healthPoint = 100;
        currentHealth = healthPoint;
        attackDamage = 15;
        speed = .5f;
        direction = Direction.RIGHT;
        Debug.Log(currentHealth);
    }

}