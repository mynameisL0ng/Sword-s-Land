using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    private int index;
    [SerializeField] GameObject[] character;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] GameObject[] infoCharacters;
    public static GameObject selectedCharacter;

    void Start()
    {
        index = 0;
        SelectCharacter();
    }
    public void OnPrevBtnClick()
    {
        if (index > 0)
        {
            index--;
        }
        SelectCharacter();
    }
    public void OnNextBtnClick()
    {
        if (index < character.Length - 1)
        {
            index++;
        }
        SelectCharacter();
    }
    public void OnCreateBtnClick()
    {
        SceneManager.LoadScene("Tutorial");
    }
    private void SelectCharacter()
    {
        for (int i = 0; i < character.Length; i++)
        {
            if (i == index)
            {
                character[i].GetComponent<SpriteRenderer>().color = Color.white;
                character[i].GetComponent<Animator>().enabled = true;
                characterName.text = characterPrefabs[i].name;
                infoCharacters[i].SetActive(true);
                if (characterPrefabs[i].name == "Knight")
                    characterName.color = Color.red;
                else
                    characterName.color = Color.cyan;
                selectedCharacter = characterPrefabs[i];
            }
            else
            {
                character[i].GetComponent<SpriteRenderer>().color = Color.black;
                character[i].GetComponent<Animator>().enabled = false;
                infoCharacters[i].SetActive(false);
            }
        }
    }
}
