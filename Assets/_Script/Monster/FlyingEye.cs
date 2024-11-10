using UnityEngine;

public class FlyingEye : Monster
{
    public FlyingEye(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 40;
        currentHealth = healthPoint;
        attackDamage = 7;
        speed = .7f;
        direction = Direction.LEFT;
        Debug.Log(currentHealth);
    }
}