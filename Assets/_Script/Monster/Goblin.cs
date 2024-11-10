using UnityEngine;

public class Goblin : Monster
{
    public Goblin(GameObject gameObject) :base(gameObject)
    {
        healthPoint = 60;
        currentHealth = healthPoint;
        attackDamage = 5;
        speed = .5f;
        direction = Direction.RIGHT;
        Debug.Log(currentHealth);
    }

}