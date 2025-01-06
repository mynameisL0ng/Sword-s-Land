using UnityEngine;

public class Knight : Character
{
    public static bool isShield;
    public Knight(GameObject gameObject) : base(gameObject)
    {
        healthPoint = 120;
        currentHealth = healthPoint;

        staminaPoint = 50;
        currentStamina = staminaPoint;

        attackDamage = 20;
        speed = 4;
        isShield = false;
        type = CharacterType.KNIGHT;
        direction = Direction.RIGHT;
    }
    public override void PlayerSkillDefault() // Skill: Parry
    {
        if (Input.GetMouseButton(1) && horizontalInPut == 0 && !isAttacking)
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

    void LoadKnight()
    {
        if (PlayerPrefs.HasKey("K_Health") && PlayerPrefs.HasKey("K_Stamina") && PlayerPrefs.HasKey("K_AttackDamage") && PlayerPrefs.HasKey("K_Speed"))
        {
            healthPoint = PlayerPrefs.GetFloat("K_Health");
            currentHealth = healthPoint;

            staminaPoint = PlayerPrefs.GetFloat("K_Stamina");
            currentStamina = staminaPoint;

            attackDamage = PlayerPrefs.GetFloat("K_AttackDamage");
            speed = PlayerPrefs.GetFloat("K_Speed");
        }
        else
        {
            healthPoint = 120;
            currentHealth = healthPoint;

            staminaPoint = 50;
            currentStamina = staminaPoint;

            attackDamage = 20;
            speed = 4;
        }
    }
}