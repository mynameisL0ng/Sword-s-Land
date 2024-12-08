using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float destroyDelayTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelayTime);
    }
}
