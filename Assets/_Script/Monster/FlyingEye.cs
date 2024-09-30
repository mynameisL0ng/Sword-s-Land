using UnityEngine;

public class FlyingEye : Monster
{
    public FlyingEye(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 70;
        attack = 20;
        speed = 2.5f;
        direction = Direction.LEFT;
    }
    public override void MonsterMovement()
    {

    }
}