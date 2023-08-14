using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string levelSelected;
    public static int index;
    [SerializeField] string difficultySelected;
    [SerializeField] Animator UIAnim;
    [SerializeField] Dropdown difficultySelect;
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject startButton;
    [SerializeField] Image korg, axel, x;

    public AudioSource UI;
    public AudioClip pressingSound;

    public void Start()
    {
        if(PlayerPrefs.GetInt("Korg") != 1)
        {
            korg.color = new Color(0, 0, 0);
        }
        if (PlayerPrefs.GetInt("Axel") != 1)
        {
            axel.color = new Color(0, 0, 0);
        }
        if (PlayerPrefs.GetInt("X") != 1)
        {
            x.color = new Color(0, 0, 0);
        }
        difficultySelected = PlayerPrefs.GetString("Difficulty", "Easy");
        if(difficultySelected == "Easy")
        {
            index = 0;
        }
        else if(difficultySelected == "Normal")
        {
            index = 1;
        }
        else if(difficultySelected == "Hard")
        {
            index = 2;
        }
        
    }
    public void StageSelect(string level)
    {
        levelSelected = level;
    }

    public void DifficultySelect(Dropdown dropdown)
    {
        difficultySelected = dropdown.options[dropdown.value].text;
        PlayerPrefs.SetString("Difficulty", difficultySelected);
    }

    public void StartLevel()
    {
        if (levelSelected != "Level 3")
        {
            SceneManager.LoadScene(levelSelected + " " + difficultySelected);
        }
        else
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CharacterSelect(string name)
    {
        //Check if character is unlocked
        if (PlayerPrefs.GetInt(name) != 1) return;
        //Set playerprefs
        PlayerPrefs.SetString("Character", name);
        //Cannot start without selecting a character
        startButton.SetActive(true);
    }

    public void Settings() {
        StartCoroutine("OpenSettings");
    }
    public IEnumerator OpenSettings() {
        UI.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Settings");
    }

}