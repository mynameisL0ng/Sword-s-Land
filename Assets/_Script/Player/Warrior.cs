using UnityEngine;

public class Warrior : Character
{
    private float holdTime;
    private float requireHoldTime = 2.5f;
    private int heavyAttack;
    public Warrior(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 100;
        magicPoint = 20;
        attack = 25;
        speed = 6;
        type = CharacterType.WARRIOR;
        direction = Direction.RIGHT;
    }
    public override void PlayerSkillDefault()
    {
        if (Input.GetMouseButton(1) && horizontalInPut == 0 && !isAttacking)
        {
            isHoldRightMouse = Input.GetMouseButton(1);
            holdTime += Time.deltaTime;
            animator.SetBool("HoldHeavy", Input.GetMouseButton(1));
            HeavyAttack();
        }
        else
        {
            isHoldRightMouse = false;
            animator.SetBool("HoldHeavy", false);
            holdTime = 0;
        }
    }
    private void HeavyAttack()
    {
        Debug.Log("Heavy Attack is ready");
        if (holdTime >= requireHoldTime)
        {
            animator.SetFloat("HoldHeavyTime", holdTime);
            heavyAttack = attack * 4;
            holdTime = 0;
        }
    }
}