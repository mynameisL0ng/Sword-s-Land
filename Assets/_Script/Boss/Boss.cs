using UnityEngine;
using static InitMonster;
public class Boss
{
    public float healthPoint { get; set; }
    public float currentHealth { get; set; }
    public float attackDamage { get; set; }
    public float expDrop { get; set; }
    protected float speed { get; set; }
    public bool Death { get; set; }

    private bool canAttack { get; set; }
    private float timeCooldown { get; set; }

    protected enum Direction { LEFT, RIGHT }
    protected Direction direction { get; set; }
    protected enum StateAnimator { IDLE, RUN, JUMP, FALL }
    protected StateAnimator stateAnimator { get; set; }
    protected enum BossType { MEDIEVAL_KING }
    protected BossType bossType { get; set; }

    protected GameObject bossObject { get; set; }
    protected Rigidbody2D body { get; set; }
    protected Animator animator { get; set; }
    protected BoxCollider2D collider2D { get; set; }
    protected Transform transform { get; set; }

    protected Vector2 oldPosition { get; set; } // spawn pos
    protected Vector2 pushBack { get; set; }
    public Boss(GameObject gameObject)
    {
        expDrop = 2500;
        bossObject = gameObject;
        canAttack = true;
        timeCooldown = 1.5f;
        pushBack = new Vector2(1.5f, 1.5f);
        body = gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = 1.496f;
        animator = gameObject.GetComponent<Animator>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        transform = gameObject.transform;
        oldPosition = transform.position;
        Death = false;
    }

    internal void Update()
    {
        BossAction();
        FlipSprite();
        if(body.velocity.y > .1f)
        {
            stateAnimator = StateAnimator.JUMP;
            SetStateAnimator((int)stateAnimator);
        }
        else if(body.velocity.y < -.1f)
        {
            stateAnimator = StateAnimator.FALL;
            SetStateAnimator((int)stateAnimator);
        }
    }

    private void BossAction() // Actions: Move to target, Return spawn point, attack
    {
        if (!Death)
        {
            if(InitPlayer.isKnight)
            {
                if (bossObject.GetComponent<BossController>().hitPlayer.collider != null) // detect player
                {
                    if (Vector2.Distance(transform.position, bossObject.GetComponent<BossController>().hitPlayer.collider.transform.position) > 3.5f && canAttack)
                    {
                        FlipToPlayer();
                        MoveToTarget();
                        if (!canAttack)
                        {
                            CooldownAttack();
                        }
                    }
                    else if (Vector2.Distance(transform.position, bossObject.GetComponent<BossController>().hitPlayer.collider.transform.position) <= 3.5f && canAttack)
                    {
                        Debug.Log(Vector2.Distance(transform.position, bossObject.GetComponent<BossController>().hitPlayer.collider.transform.position));
                        AttackAnimation();
                        Jump();
                        CooldownAttack();
                    }
                    else
                    {
                        stateAnimator = StateAnimator.IDLE;
                        CooldownAttack();
                        SetStateAnimator((int)stateAnimator);
                    }
                }
                else // return spawn point
                {
                    canAttack = true;
                    FlipToOldPos();
                    transform.position = Vector2.MoveTowards(transform.position, oldPosition, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, oldPosition) < 3f)
                    {
                        stateAnimator = StateAnimator.IDLE;
                        SetStateAnimator((int)stateAnimator);
                        Regeneration();
                    }
                }
            }
            else
            {
                if (bossObject.GetComponent<BossController>().hitPlayer.collider != null) // detect player
                {
                    if (Vector2.Distance(transform.position, bossObject.GetComponent<BossController>().hitPlayer.collider.transform.position) > 2.5f && canAttack)
                    {
                        FlipToPlayer();
                        MoveToTarget();
                        if (!canAttack)
                        {
                            CooldownAttack();
                        }
                    }
                    else if (Vector2.Distance(transform.position, bossObject.GetComponent<BossController>().hitPlayer.collider.transform.position) <= 2.5f && canAttack)
                    {
                        Debug.Log(Vector2.Distance(transform.position, bossObject.GetComponent<BossController>().hitPlayer.collider.transform.position));
                        AttackAnimation();
                        Jump();
                        CooldownAttack();
                    }
                    else
                    {
                        stateAnimator = StateAnimator.IDLE;
                        CooldownAttack();
                        SetStateAnimator((int)stateAnimator);
                    }
                }
                else // return spawn point
                {
                    canAttack = true;
                    FlipToOldPos();
                    transform.position = Vector2.MoveTowards(transform.position, oldPosition, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, oldPosition) < 3f)
                    {
                        stateAnimator = StateAnimator.IDLE;
                        SetStateAnimator((int)stateAnimator);
                        Regeneration();
                    }
                }
            }
        }
    }

    public void MoveToTarget()
    {
        stateAnimator = StateAnimator.RUN;
        SetStateAnimator((int)stateAnimator);
        transform.position = Vector2.MoveTowards(transform.position, bossObject.GetComponent<BossController>().hitPlayer.transform.position, speed * Time.deltaTime);
        //transform.position = Vector2.Lerp(transform.position, bossObject.GetComponent<BossController>().hitPlayer.transform.position, speed * Time.deltaTime);
    }

    void Jump()
    {
        stateAnimator = StateAnimator.JUMP;
        SetStateAnimator((int)stateAnimator);
        switch(direction)
        {
            case Direction.LEFT:
                    body.velocity = new Vector2(body.velocity.x + 3.5f, body.velocity.y + 2.5f);
                break;
            case Direction.RIGHT:
                body.velocity = new Vector2(body.velocity.x - 3.5f, body.velocity.y + 2.5f);
                break;
        }
    }

    private void AttackAnimation()
    {
        animator.SetTrigger("Attack");
        canAttack = false;
    }

    public void DealDamage(float attackDamage)
    {
        Debug.Log(attackDamage);
        InitPlayer.player.TakeHit(attackDamage);
    }

    private void CooldownAttack()
    {
        if (timeCooldown > 0)
        {
            timeCooldown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
            timeCooldown = 3f;
        }
    }

    void Regeneration()
    {
        if (currentHealth < healthPoint)
        {
            currentHealth += 0.02f;
        }
        else
        {
            currentHealth = healthPoint;
        }
    }

    public void TakeHit(float attackDamage)
    {
        if (!Death)
        {
            currentHealth -= attackDamage;
            PushBack();
            animator.SetTrigger("TakeHit");
            if (currentHealth <= 0)
                Die();
        }
    }
    public void Die()
    {
        InitPlayer.player.currentEXP += expDrop;
        collider2D.enabled = false;
        body.bodyType = RigidbodyType2D.Static;
        Death = true;
        animator.SetTrigger("Die");
        PlayerPrefs.SetFloat("CurrentEXP", InitPlayer.player.currentEXP);
        if (InitPlayer.player.playerObject != null)
        {
            if (InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.isActive)
            {
                if (bossType.ToString() == InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.goal.killType.ToString())
                {
                    InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.goal.EnemyKilled();
                    Debug.Log(InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.goal.currentAmount);
                }
            }
        }
    }

    public void PushBack()
    {
        switch (direction)
        {
            case Direction.LEFT:
                body.velocity = new Vector2(body.velocity.x + pushBack.x, body.velocity.y + pushBack.y);
                break;
            case Direction.RIGHT:
                body.velocity = new Vector2(body.velocity.x - pushBack.x, body.velocity.y + pushBack.y);
                break;
        }
    }

    public void AttackPush()
    {
        switch (direction)
        {
            case Direction.LEFT:
                body.velocity = new Vector2(body.velocity.x - 0.01f, body.velocity.y);
                break;
            case Direction.RIGHT:
                body.velocity = new Vector2(body.velocity.x + 0.01f, body.velocity.y);
                break;
        }
    }

    protected void SetStateAnimator(int stateAnimator)
    {
        animator.SetInteger("State", stateAnimator);
    }

    private void FlipToPlayer()
    {
        if (transform.position.x > bossObject.GetComponent<BossController>().hitPlayer.transform.position.x)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }
    }

    private void FlipToOldPos()
    {
        stateAnimator = StateAnimator.RUN;
        animator.SetInteger("State", (int)stateAnimator);
        if (transform.position.x >= oldPosition.x)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }
    }

    private void FlipSprite()
    {
        if (!Death)
        {
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
}
