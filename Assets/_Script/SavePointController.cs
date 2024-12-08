using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePointController : MonoBehaviour
{
    public GameObject uI_SavePoint;
    public Image fillImage;
    bool isZone;
    private void Start()
    {
        isZone = false;
        uI_SavePoint.SetActive(false);
    }
    private void Update()
    {
        if(isZone)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(name);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            uI_SavePoint.SetActive(true);
            isZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            uI_SavePoint.SetActive(false);
            isZone = false;
        }
    }

}
