using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Monster monster;

    public InitMonster initMonster;
    public RaycastHit2D hitPlayer;
    public GameObject expPopUp;

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
                Exp_PopUp();
                this.enabled = false;
                Destroy(gameObject, 3);
            }
        }
    }
    private void DectectedPlayer()
    {
        hitPlayer = Physics2D.BoxCast(transform.position, dectectedSize, 0, -transform.up, dectectedDistance, playerLayer);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * dectectedDistance, dectectedSize);
    }*/

    void Exp_PopUp()
    {
        Vector2 popupPosition = transform.position;
        popupPosition.y = 3f;
        expPopUp.GetComponent<TextMeshPro>().text = monster.expDrop.ToString();
        Instantiate(expPopUp, popupPosition, Quaternion.identity);
    }

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
                monster.EnemyDealDamage(monster.attackDamage);
        }
    }
    private void EnemyPushAttackForce()
    {
        monster.EnemyAttackPush();
    }
}
