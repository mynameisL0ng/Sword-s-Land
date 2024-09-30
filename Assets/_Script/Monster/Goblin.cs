using UnityEngine;

public class Goblin : Monster
{
    public Goblin(GameObject gameObject) :base(gameObject)
    {
        healthPoint = 100;
        startHealthPoint = healthPoint;
        attack = 15;
        speed = .5f;
        direction = Direction.LEFT;
        Debug.Log(startHealthPoint);
    }

    public override void MonsterMovement()
    {
        stateAnimator = StateAnimator.RUN;
        SetStateAnimator((int)stateAnimator);
        transform.position = Vector2.Lerp(transform.position, 
            InitMonster.hitPlayer.collider.transform.position, 
            speed * Time.deltaTime);
    }
}