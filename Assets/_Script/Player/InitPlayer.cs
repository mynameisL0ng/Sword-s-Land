using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayer : MonoBehaviour
{
    public static Character player;
    public static bool isWarrior;
    public static bool isKnight;
    GameObject playerObject;
    GameObject selectedCharacter;
    float x, y; // get posistion of SavePoint
    void Awake()
    {
        selectedCharacter = CharacterSelect.selectedCharacter;
        LoadPlayerPos();
        playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);
        playerObject.name = "Player";
        switch (selectedCharacter.name)
        {
            case "Knight":
                player = new Knight(playerObject);
                isKnight = true;
                break;
            case "Warrior":
                player = new Warrior(playerObject);
                isWarrior = true;
                break;
        }
    }
    public void LoadPlayerPos()
    {
        if(selectedCharacter.name == "Knight" && PlayerPrefs.HasKey("K_SavePoint1_X") && PlayerPrefs.HasKey("K_SavePoint1_Y"))
        {
            Debug.Log("Knight");
            x = PlayerPrefs.GetFloat("K_SavePoint1_X");
            y = PlayerPrefs.GetFloat("K_SavePoint1_Y");
            Debug.Log(x + "," +  y);
            transform.position = new Vector2(x, y);
        }
        else if(selectedCharacter.name == "Warrior" && PlayerPrefs.HasKey("W_SavePoint1_X") && PlayerPrefs.HasKey("W_SavePoint1_Y"))
        {
            Debug.Log("Warrior");
            x = PlayerPrefs.GetFloat("W_SavePoint1_X");
            y = PlayerPrefs.GetFloat("W_SavePoint1_Y");
            Debug.Log(x + "," + y);
            transform.position = new Vector2(x, y);
        }
        
    }
}
