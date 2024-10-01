using UnityEngine;

public class Knight : Character
{
    public Knight(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 120;
        magicPoint = 30;
        attack = 20;
        speed = 4;
        type = CharacterType.KNIGHT;
        direction = Direction.RIGHT;
    }
    public override void PlayerSkillDefault()
    {
        
    }
}