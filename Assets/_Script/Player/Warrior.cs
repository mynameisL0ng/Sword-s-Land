using UnityEngine;

public class Warrior : Character
{
    public Warrior(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 100;
        magicPoint = 20;
        attack = 25;
        speed = 6;
        type = CharacterType.WARRIOR;
        direction = Direction.RIGHT;
        
    }
}