using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMonster : MonoBehaviour
{
    public enum MonsterType { Goblin, Skeleton, Mushroom, FlyingEye}
    public MonsterType monsterType;
    public Monster monster;
    [SerializeField] private GameObject monsterPrefabs;
    void Awake()
    {
        GameObject monsterObject;
        switch (monsterType)
        {
            case MonsterType.Goblin:
                monsterObject = Instantiate(monsterPrefabs, transform.position, Quaternion.identity);
                monsterObject.transform.parent = this.transform;
                monsterObject.name = MonsterType.Goblin.ToString();
                monster = new Goblin(monsterObject);
                break;
            case MonsterType.Skeleton:
                monsterObject = Instantiate(monsterPrefabs, transform.position, Quaternion.identity);
                monsterObject.transform.parent = this.transform;
                monsterObject.name = MonsterType.Skeleton.ToString();
                monster = new Skeleton(monsterObject);
                break;
            case MonsterType.Mushroom:
                monsterObject = Instantiate(monsterPrefabs, transform.position, Quaternion.identity);
                monsterObject.transform.parent = this.transform;
                monsterObject.name = MonsterType.Mushroom.ToString();
                monster = new Mushroom(monsterObject);
                break;
            case MonsterType.FlyingEye:
                monsterObject = Instantiate(monsterPrefabs, transform.position, Quaternion.identity);
                monsterObject.transform.parent = this.transform;
                monsterObject.name = MonsterType.FlyingEye.ToString();
                monster = new FlyingEye(monsterObject);
                break;
        }
    }

}
