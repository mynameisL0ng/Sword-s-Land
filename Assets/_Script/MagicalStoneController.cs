using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicalStoneController : MonoBehaviour
{
    public GameObject eKey;
    public GameObject magicalStoneUI;
    private void Start()
    {
        eKey.SetActive(false);
        magicalStoneUI.SetActive(false);
    }
    private void Update()
    {
        if (eKey.activeSelf)
        {
            if (Input.GetAxis("Horizontal") <= .5)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    magicalStoneUI.SetActive(true);
                }
            }
        }
        if(magicalStoneUI.activeSelf)
        {
            UI_Manager.modeUI = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            eKey.SetActive(true);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            eKey.SetActive(false);
    }

}
