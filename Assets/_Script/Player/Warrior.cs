using UnityEngine;

public class Warrior : Character
{
    public static float holdTime;
    public static float requireHoldTime = 2.5f;
    public static float heavyAttack;
    /*private float cooldownTimeHeavy = 10f;*/
    public Warrior(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 100;
        currentHealth = healthPoint;
        magicPoint = 20;
        attackDamage = 25;
        speed = 6;
        type = CharacterType.WARRIOR;
        direction = Direction.RIGHT;
    }
    public override void PlayerSkillDefault() // Skill: Heavy Attack
    {
        if (Input.GetButton("HeavyAttack") && horizontalInPut == 0 && !isAttacking)
        {
            isHoldRightMouse = Input.GetMouseButton(1);
            /*holdTime += Time.deltaTime;*/
            animator.SetBool("HoldHeavy", Input.GetButton("HeavyAttack"));
            if(holdTime >= requireHoldTime)
                HeavyAttackAnimation();
        }
        else
        {
            isHoldRightMouse = false;
            animator.SetBool("HoldHeavy", false);
            holdTime = 0;
        }
    }
    private void HeavyAttackAnimation()
    {
        /*body.velocity = new Vector2(body.velocity.x + 5, body.velocity.y);*/
        /*animator.SetFloat("HoldHeavyTime", holdTime);*/
        animator.SetTrigger("HeavyAttack");
    }
    public float HeavyAttackDamage()
    {
        holdTime = 0;
        return attackDamage * 3.5f;
    }
}