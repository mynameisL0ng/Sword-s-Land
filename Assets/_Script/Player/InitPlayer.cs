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
    PlayerController[] playerController;
    void Awake()
    {
        LoadPlayerPos();
        playerController = FindObjectsOfType<PlayerController>();
        if (playerController.Length == 0)
        {
            selectedCharacter = CharacterSelect.selectedCharacter;
            playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);
            playerObject.name = "Player";
        }
        if (FindObjectsOfType<InitPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if (playerController.Length > 1)
        {
            Destroy(playerController[0].gameObject);
            return;
        }
        DontDestroyOnLoad(playerObject);
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
    void LoadPlayerPos()
    {
        if(PlayerPrefs.HasKey("X") && PlayerPrefs.HasKey("Y") && PlayerPrefs.HasKey("Z"))
        {
            float x = PlayerPrefs.GetFloat("X");
            float y = PlayerPrefs.GetFloat("Y");
            float z = PlayerPrefs.GetFloat("Z");
            gameObject.transform.position = new Vector3(x, y, z);
            Debug.Log("Load success");
        }
    }
}
