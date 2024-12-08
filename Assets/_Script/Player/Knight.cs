using UnityEngine;

public class Knight : Character
{
    public static bool isShield;
    public Knight(GameObject gameObject) : base(gameObject)
    {
        isShield = false;
        healthPoint = 120;
        currentHealth = healthPoint;
        staminaPoint = 30;
        currentStamina = staminaPoint;
        attackDamage = 20;
        speed = 4;
        type = CharacterType.KNIGHT;
        direction = Direction.RIGHT;
    }
    public override void PlayerSkillDefault() // Skill: Parry
    {
        if(Input.GetMouseButton(1) && horizontalInPut == 0 && !isAttacking)
        {
            isHoldRightMouse = Input.GetMouseButton(1);
            isShield = Input.GetMouseButton(1);
            animator.SetBool("Parry", Input.GetMouseButton(1));
        }
        else
        {
            isShield = false;
            isHoldRightMouse = false;
            animator.SetBool("Parry", false);
        }
    }
    public void HitParry()
    {
        animator.SetTrigger("HitParry");
    }
}