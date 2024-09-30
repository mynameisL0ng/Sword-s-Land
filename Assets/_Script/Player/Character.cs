using Unity.VisualScripting;
using UnityEngine;

public class Character
{
    protected int healthPoint { get; set; }
    protected int magicPoint { get; set; }
    protected int attack { get; set; }
    protected float attackPush { get; set; }
    protected float speed { get; set; }
    protected float jumpPower { get; set; }
    protected enum Direction { LEFT, RIGHT}
    protected enum CharacterType { KNIGHT, WARRIOR}
    public enum AnimatorState { IDLE, RUN, JUMP, FALL}
    protected Direction direction { get; set; }
    protected CharacterType type { get; set; }
    
    public static float horizontalInPut;
    protected Rigidbody2D body { get; set; }
    protected Animator animator { get; set; }
    protected BoxCollider2D collider2D { get; set; }
    protected Transform transform { get; set; }
    public Character(GameObject gameObject)
    {
        attackPush = 1.5f;
        jumpPower = 5f;
        body = gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = 1.496f;
        animator = gameObject.GetComponent<Animator>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        transform = gameObject.transform;
    }

    internal void Update()
    {
        PlayerJump();
        InputHandle();
        FlipSprite();
        UpdateStateAnimator();
        PlayerAttack();
    }

    private void InputHandle()
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
    
    private void PlayerAttack()
    {
        if(Input.GetButtonDown("Attack") && PlayerController.grounded)
        {
            animator.SetTrigger("Attack");
        }
    }
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
    public void PlayerDealDamage()
    {
        InitMonster.monster.healthPoint -= attack;
        Debug.Log(InitMonster.monster.healthPoint);
    }
    private void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && PlayerController.grounded)
        {
            body.velocity = new Vector2(body.velocity.x,jumpPower);
        }
    }
    private void UpdateStateAnimator()
    {
        AnimatorState animatorState;
        if(horizontalInPut < -.15f || horizontalInPut > .15f)
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