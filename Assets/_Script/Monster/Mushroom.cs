using UnityEngine;

public class Mushroom : Monster
{
    public Mushroom(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 110;
        attack = 13;
        speed = 1.8f;
        direction = Direction.LEFT;
    }
    public override void MonsterMovement()
    {

    }
}