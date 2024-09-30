using UnityEngine;

public class Skeleton : Monster
{
    public Skeleton(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 130;
        attack = 12;
        speed = 1.7f;
        direction = Direction.LEFT;
    }
    public override void MonsterMovement()
    {

    }
}