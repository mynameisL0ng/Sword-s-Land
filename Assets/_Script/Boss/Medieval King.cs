using UnityEngine;

public class MedievalKing : Boss
{
    public MedievalKing(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 350;
        currentHealth = healthPoint;
        attackDamage = 25;
        speed = 3f;
        direction = Direction.LEFT;
    }
}
