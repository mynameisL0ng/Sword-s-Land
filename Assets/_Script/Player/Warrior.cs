using UnityEngine;

public class Warrior : Character
{
    public static float holdTime;
    public static float requireHoldTime = 2f;
    /*private float cooldownTimeHeavy = 10f;*/
    public Warrior(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 100;
        currentHealth = healthPoint;

        staminaPoint = 30;
        currentStamina = staminaPoint;

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
            if (holdTime >= requireHoldTime)
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
        animator.SetTrigger("HeavyAttack");
    }
    private void HeavyAttackForce()
    {
        switch (direction)
        {
            case Direction.LEFT:
                body.velocity = new Vector2(-3f, body.velocity.y);
                break;
            case Direction.RIGHT:
                body.velocity = new Vector2(+3f, body.velocity.y);
                break;
        }
    }

    void LoadWarrior()
    {
        if (PlayerPrefs.HasKey("W_Health") && PlayerPrefs.HasKey("W_Stamina") && PlayerPrefs.HasKey("W_AttackDamage") && PlayerPrefs.HasKey("W_Speed"))
        {
            healthPoint = PlayerPrefs.GetFloat("W_Health");
            currentHealth = healthPoint;

            staminaPoint = PlayerPrefs.GetFloat("W_Stamina");
            currentStamina = staminaPoint;

            attackDamage = PlayerPrefs.GetFloat("W_AttackDamage");
            speed = PlayerPrefs.GetFloat("W_Speed");
        }
        else
        {
            healthPoint = 100;
            currentHealth = healthPoint;

            staminaPoint = 30;
            currentStamina = staminaPoint;

            attackDamage = 25;
            speed = 6;
        }
    }
}