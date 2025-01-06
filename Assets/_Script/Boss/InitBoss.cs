using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBoss : MonoBehaviour
{
    public Boss boss;
    public GameObject bossPrefabs;
    public string nameBoss;
    void Start()
    {
        GameObject bossObject;
        bossObject = Instantiate(bossPrefabs, transform.position, Quaternion.identity);
        bossObject.transform.parent = transform;
        bossObject.name = nameBoss;
        boss = new MedievalKing(bossObject);
    }

}
