using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMonster : MonoBehaviour
{
    public static Monster monster;
    public static RaycastHit2D hitPlayer;
    [SerializeField] private Vector2 dectectedSize;
    [SerializeField] private float dectectedDistance;
    [SerializeField] private LayerMask playerLayer;
    void Start()
    {
        GameObject monsterObject = gameObject;
        switch (name)
        {
            case "Goblin":
                monster = new Goblin(monsterObject);
                break;
            case "Skeleton":
                monster = new Skeleton(monsterObject);
                break;
            case "Mushroom":
                monster = new Mushroom(monsterObject);
                break;
            case "Flying Eye":
                monster = new FlyingEye(monsterObject);
                break;
        }
    }
    private void Update()
    {
        DectectedPlayer();
        monster.Update();
    }
    private void DectectedPlayer()
    {
        hitPlayer = Physics2D.BoxCast(transform.position, dectectedSize, 0, -transform.up, dectectedDistance, playerLayer);
    }
    /*private void OnDrawGizmos() draw detect zone of enemy
    {
        Gizmos.DrawWireCube(transform.position - transform.up * dectectedDistance, dectectedSize);
    }*/

}
