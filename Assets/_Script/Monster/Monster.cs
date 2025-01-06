using Unity.VisualScripting;
using UnityEngine;

public abstract class Monster
{
    public float healthPoint {  get; set; }
    public float currentHealth { get; set; }
    public float attackDamage { get; set; }
    public float expDrop { get; set; }
    protected float speed { get; set; }
    public bool Death { get; set; }

    private bool canAttack { get; set; }
    private float timeCooldown { get; set; }

    protected enum Direction { LEFT, RIGHT }
    protected enum StateAnimator { IDLE , RUN }
    protected Direction direction { get; set; }
    protected StateAnimator stateAnimator { get; set; }
    protected enum MonsterType { GOBLIN, FLYINGEYE, MUSHROOM, SKELETON }
    protected MonsterType monsterType { get; set; }
    protected GameObject monsterObject { get; set; }
    protected Vector2 oldPosition { get; set; }
    protected Vector2 pushBack { get; set; }
    protected Rigidbody2D body { get; set; }
    protected Animator animator { get; set; }
    protected BoxCollider2D collider2D { get; set; }
    protected Transform transform { get; set; }
    protected Monster(GameObject gameObject)
    {
        expDrop = 200;
        monsterObject = gameObject;
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
        MonsterAction();
        FlipSprite();
    }

    private void MonsterAction() // Actions: Move to target, Return spawn point, attack
    {
        if(!Death)
        {
            if (monsterObject.GetComponent<MonsterController>().hitPlayer.collider != null) // detect player
            {
                if (Vector2.Distance(transform.position, monsterObject.GetComponent<MonsterController>().hitPlayer.collider.transform.position) > 2)
                {
                    MonsterFlipToPlayer();
                    MonsterMovement();
                    if(!canAttack)
                    {
                        CooldownAttack();
                    }
                }
                else if (Vector2.Distance(transform.position, monsterObject.GetComponent<MonsterController>().hitPlayer.collider.transform.position) <= 2 && canAttack)
                {
                    MonsterAttackAnimation();
                    CooldownAttack();
                    // attack animation
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
                MonsterFlipToOldPos();
                if (Vector2.Distance(transform.position, oldPosition) < .5f)
                {
                    stateAnimator = StateAnimator.IDLE;
                    SetStateAnimator((int)stateAnimator);
                    Regeneration();
                }
                transform.position = Vector2.Lerp(transform.position, oldPosition, speed * Time.deltaTime);
            }
        }
    }
    public void MonsterMovement()
    {
        stateAnimator = StateAnimator.RUN;
        SetStateAnimator((int)stateAnimator);
        /*transform.position = Vector2.MoveTowards(transform.position, MonsterController.hitPlayer.transform.position, speed * Time.deltaTime);*/
        transform.position = Vector2.Lerp(transform.position, monsterObject.GetComponent<MonsterController>().hitPlayer.transform.position, speed * Time.deltaTime);
    }
    private void MonsterAttackAnimation()
    {
        animator.SetTrigger("Attack");
        canAttack = false;
    }
    private void FlipSprite()
    {
        if(!Death)
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
    private void MonsterFlipToPlayer()
    {
        if(transform.position.x > monsterObject.GetComponent<MonsterController>().hitPlayer.transform.position.x)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }
    }
    private void MonsterFlipToOldPos()
    {
        stateAnimator = StateAnimator.RUN;
        SetStateAnimator((int)stateAnimator);
        if (transform.position.x >= oldPosition.x)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }
    }

    public void EnemyDealDamage(float attackDamage)
    {
        InitPlayer.player.TakeHit(attackDamage);
    }
    private void CooldownAttack()
    {
        if(timeCooldown > 0)
        {
            timeCooldown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
            timeCooldown = 1.5f;
        }
    }
    public void TakeHit(float attackDamage)
    {
        if(!Death)
        {
            currentHealth -= attackDamage;
            PushBack();
            animator.SetTrigger("TakeHit");
            if (currentHealth <= 0)
                Die();
        }
    }
    void Regeneration()
    {
        if(currentHealth < healthPoint)
        {
            currentHealth += 0.02f;
        }
        else
        {
            currentHealth = healthPoint;
        }
    }
    public void Die()
    {
        if (InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.isActive)
        {
            expDrop *= 1.5f;
            InitPlayer.player.currentEXP += expDrop;
        }
        else
        {
            expDrop = 200;
            InitPlayer.player.currentEXP += expDrop;
        }
        collider2D.enabled = false;
        body.bodyType = RigidbodyType2D.Static;
        Death = true;
        animator.SetBool("Death", Death);
        PlayerPrefs.SetFloat("CurrentEXP", InitPlayer.player.currentEXP);

        // count player quest
        if (InitPlayer.player.playerObject != null)
        {
            if(InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.isActive)
            {
                if (monsterType.ToString() == InitPlayer.player.playerObject.GetComponent<PlayerController>().quest.goal.killType.ToString())
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
    public void EnemyAttackPush()
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
}