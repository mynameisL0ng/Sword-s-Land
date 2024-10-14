using UnityEngine;

public class FlyingEye : Monster
{
    public FlyingEye(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 70;
        currentHealth = healthPoint;
        attackDamage = 20;
        speed = .7f;
        direction = Direction.LEFT;
        Debug.Log(currentHealth);
    }
}