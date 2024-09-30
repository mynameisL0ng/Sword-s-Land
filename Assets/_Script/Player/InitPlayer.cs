using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayer : MonoBehaviour
{
    public static Character player;
    void Start()
    {
        GameObject selectedCharacter = CharacterSelect.selectedCharacter;
        GameObject playerObject = Instantiate(selectedCharacter, transform.position, Quaternion.identity);
        playerObject.name = "Player";

        switch (selectedCharacter.name)
        {
            case "Knight":
                player = new Knight(playerObject);
                break;
            case "Warrior":
                player = new Warrior(playerObject);
                break ;
        }
    }

}
