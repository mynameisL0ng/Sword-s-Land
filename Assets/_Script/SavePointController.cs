using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePointController : MonoBehaviour
{
    public GameObject uI_SavePoint;
    public Image fillImage;
    bool isZone;
    float timeRequireToSave = 3f;
    float currentTime = 0;
    private void Start()
    {
        isZone = false;
        uI_SavePoint.SetActive(false);
    }
    private void Update()
    {
        HoldToFill();
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
    void HoldToFill()
    {
        if (isZone && Input.GetKey(KeyCode.E))
        {
            FillingImage(Time.deltaTime);
        }
        else if(currentTime < timeRequireToSave)
        {
            currentTime -= Time.deltaTime;
            fillImage.fillAmount = currentTime / timeRequireToSave;
            if(currentTime <= 0)
                currentTime = 0;
        }
    }
    void FillingImage(float time)
    {
        currentTime += time;
        if (currentTime < timeRequireToSave)
        {
            fillImage.fillAmount = currentTime / timeRequireToSave;
        }
    }

}
