using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Monster monster;
    public InitMonster initMonster;
    public RaycastHit2D hitPlayer;
    [SerializeField] private Vector2 dectectedSize;
    [SerializeField] private float dectectedDistance;
    [SerializeField] private LayerMask playerLayer;
    void Start()
    {
        if (initMonster == null)
        {
            initMonster = GetComponentInParent<InitMonster>();
        }
        
        if(initMonster != null)
        {
            monster = initMonster.monster;
            Debug.Log(monster);
        }
    }
    private void Update()
    {
        if (monster != null)
        {
            DectectedPlayer();
            monster.Update();
            if (monster.Death)
            {
                Debug.Log("Death");
                this.enabled = false;
            }
        }
    }
    private void DectectedPlayer()
    {
        hitPlayer = Physics2D.BoxCast(transform.position, dectectedSize, 0, -transform.up, dectectedDistance, playerLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * dectectedDistance, dectectedSize);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (InitPlayer.isKnight && Knight.isShield)
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("HitParry");
            }
            else
                monster.EnemyDealDamage(monster.attackDamage);
        }
    }
    private void EnemyPushAttackForce()
    {
        monster.EnemyAttackPush();
    }
}
