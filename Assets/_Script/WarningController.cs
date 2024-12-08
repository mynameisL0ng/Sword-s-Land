using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    public GameObject warning;
    public enum WarningType {Stat}
    public WarningType warningType;
    void Start()
    {
        checkWarningType();
    }

    void Update()
    {
        checkWarningType();
    }
    void WarningStat()
    {
        if(InitPlayer.player.skillPoint > 0)
        {
            warning.SetActive(true);
        }
        else
            warning.SetActive(false);
    }
    void checkWarningType()
    {
        switch (warningType)
        {
            case WarningType.Stat:
                WarningStat(); 
                break;
        }
    }
}
