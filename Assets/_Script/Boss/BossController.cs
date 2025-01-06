using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Boss boss;
    public InitBoss initBoss;

    public Vector2 dectectedSize;
    public float dectectedDistance;
    public LayerMask playerLayer;
    public RaycastHit2D hitPlayer;

    void Start()
    {
        if (initBoss == null)
        {
            initBoss = GetComponentInParent<InitBoss>();
        }

        if (initBoss != null)
        {
            boss = initBoss.boss;
        }
    }

    void Update()
    {
        if (boss != null)
        {
            boss.Update();
            DetectedPlayer();
        }
    }
    private void DetectedPlayer()
    {
        hitPlayer = Physics2D.BoxCast(transform.position, dectectedSize, 0, -transform.up, dectectedDistance, playerLayer);
    }

    /*    private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * dectectedDistance, dectectedSize);
        }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (InitPlayer.isKnight && Knight.isShield)
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("HitParry");
                collision.gameObject.GetComponent<PlayerController>().AudioSkillDefault();
            }
            else
                boss.DealDamage(boss.attackDamage);
        }
    }

    private void PushAttackForce() // anim event
    {
        boss.AttackPush();
    }
}
