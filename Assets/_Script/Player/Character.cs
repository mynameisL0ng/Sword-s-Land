using Unity.VisualScripting;
using UnityEngine;

public abstract class Character
{
    public float healthPoint { get; set; }
    public  float currentHealth { get; set; }
    protected int magicPoint { get; set; }
    public float attackDamage { get; set; }
    protected float attackPush { get; set; }
    public float speed { get; set; }
    protected float jumpPower { get; set; }
    public enum Direction { LEFT, RIGHT}
    protected enum CharacterType { KNIGHT, WARRIOR}
    public enum AnimatorState { IDLE, RUN, JUMP, FALL}
    public Direction direction { get; set; }
    protected CharacterType type { get; set; }
    
    public float horizontalInPut;
    public bool isAttacking;
    public bool isHoldRightMouse = false;
    public bool isDie = false;
    protected Rigidbody2D body { get; set; }
    protected Animator animator { get; set; }
    protected BoxCollider2D collider2D { get; set; }
    protected Transform transform { get; set; }
    protected GameObject playerObject { get; set; }
    public Character(GameObject gameObject)
    {
        playerObject = gameObject;
        attackPush = 2f;
        jumpPower = 6f;
        body = gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = 1.496f;
        animator = gameObject.GetComponent<Animator>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        transform = gameObject.transform;
        isAttacking = false;
    }

    internal void Update()
    {
        PlayerJump();
        PlayerAttack();
        InputHandle();
        FlipSprite();
        UpdateStateAnimator();
        PlayerSkillDefault();
    }

    private void InputHandle()
    {
        if (!isAttacking)
        {
            horizontalInPut = Input.GetAxis("Horizontal");
            if (horizontalInPut > .015f)
            {
                body.velocity = new Vector2(speed * horizontalInPut, body.velocity.y);
                direction = Direction.RIGHT;
            }
            else if (horizontalInPut < -.015f)
            {
                body.velocity = new Vector2(speed * horizontalInPut, body.velocity.y);
                direction = Direction.LEFT;
            }
        }
    }
    
    private void PlayerAttack()
    {
        if(Input.GetButtonDown("Attack") && PlayerController.grounded)
        {
            horizontalInPut = 0f;
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }
    public abstract void PlayerSkillDefault();
    
    public void PlayerAttackPush()
    {
        switch (direction)
        {
            case Direction.LEFT:
                body.velocity = new Vector2(body.velocity.x - attackPush, body.velocity.y);
                break;
            case Direction.RIGHT:
                body.velocity = new Vector2(body.velocity.x + attackPush, body.velocity.y);
                break;
        }
    }
    public void TakeHit(float attackDamage)
    {
        currentHealth -= attackDamage;
        if(!isHoldRightMouse && !isAttacking)
            animator.SetTrigger("TakeHit");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        isDie = true;
        animator.SetTrigger("Dead");
        playerObject.GetComponent<PlayerController>().enabled = false;
        collider2D.enabled = false;
        body.bodyType = RigidbodyType2D.Static;
    }
    public void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && PlayerController.grounded && !isAttacking && !Input.GetButton("HeavyAttack"))
        {
            body.velocity = new Vector2(body.velocity.x,jumpPower);
        }
    }
    private void UpdateStateAnimator()
    {
        AnimatorState animatorState;
        if((horizontalInPut < -.15f || horizontalInPut > .15f) && !isAttacking)
        {
            animatorState = AnimatorState.RUN;
            if (horizontalInPut > .15f)
            {
                animator.SetInteger("State", (int)animatorState);
            }
            else if (horizontalInPut < -.15f)
            {
                animator.SetInteger("State", (int)animatorState);
            }
        }
        else
        {
            animatorState = AnimatorState.IDLE;
            animator.SetInteger("State", (int)animatorState);
        }
        if(body.velocity.y > .01f)
        {
            animatorState = AnimatorState.JUMP;
            animator.SetInteger("State", (int)animatorState);
        }
        else if(body.velocity.y < -.01f)
        {
            animatorState = AnimatorState.FALL;
            animator.SetInteger("State", (int)animatorState);
        }
    }

    private void FlipSprite() {
        switch (direction)
        {
            case Direction.LEFT:
                transform.localScale = new Vector2(-1, transform.localScale.y);
                break;
            case Direction.RIGHT:
                transform.localScale = new Vector2(1, transform.localScale.y);
                break;
        }
    }
}