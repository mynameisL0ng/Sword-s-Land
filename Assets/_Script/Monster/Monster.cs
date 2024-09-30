using UnityEngine;

public abstract class Monster
{
    public int healthPoint {  get; set; }
    protected int startHealthPoint { get; set; }
    protected int attack { get; set; }
    protected float speed { get; set; }
    protected enum Direction { LEFT, RIGHT }
    protected enum StateAnimator { IDLE , RUN , DEATH , TAKEHIT}
    protected Direction direction { get; set; }
    protected StateAnimator stateAnimator { get; set; }
    protected Vector2 oldPosition { get; set; }
    protected Rigidbody2D body { get; set; }
    protected Animator animator { get; set; }
    protected BoxCollider2D collider2D { get; set; }
    protected Transform transform { get; set; }
    protected Monster(GameObject gameObject)
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = 1.496f;
        animator = gameObject.GetComponent<Animator>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        transform = gameObject.transform;
        oldPosition = transform.position;
    }
    internal void Update()
    {
        MonsterAction();
        FlipSprite();
    }

    public abstract void MonsterMovement();
    private void MonsterAction()
    {
        if (InitMonster.hitPlayer.collider != null)
        {
            if (Vector2.Distance(transform.position, InitMonster.hitPlayer.collider.transform.position) > 2)
            {
                MonsterFlipToPlayer();
                MonsterMovement();
            }
            else
            {
                stateAnimator = StateAnimator.IDLE;
                SetStateAnimator((int)stateAnimator);
            }
        }
        else
        {
            MonsterFlipToOldPos();
            if(Vector2.Distance(transform.position, oldPosition) < .1f)
            {
                stateAnimator = StateAnimator.IDLE;
                SetStateAnimator((int)stateAnimator);
            }
            transform.position = Vector2.MoveTowards(transform.position, oldPosition, 1 * Time.deltaTime);
        }
    }
    private void FlipSprite()
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
    private void MonsterFlipToPlayer()
    {
        if(transform.position.x > InitMonster.hitPlayer.transform.position.x)
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
        if (transform.position.x > oldPosition.x)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }
    }
    protected void SetStateAnimator(int stateAnimator)
    {
        animator.SetInteger("State", stateAnimator);
    }
}