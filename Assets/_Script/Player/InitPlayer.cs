using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayer : MonoBehaviour
{
    public static Character player;
    public static bool isWarrior;
    public static bool isKnight;
    [SerializeField] private GameObject playerHealthBar;
    void Awake()
    {
        GameObject selectedCharacter = CharacterSelect.selectedCharacter;
        GameObject playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);
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

}
