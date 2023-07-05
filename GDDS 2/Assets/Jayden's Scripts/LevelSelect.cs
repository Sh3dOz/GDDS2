using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string levelSelected;
    [SerializeField] string difficultySelected;
    [SerializeField] Animator UIAnim;
    [SerializeField] Dropdown difficultySelect;
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject nextButton, startButton;
    [SerializeField] Image korg, axel, x;

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
    }
    public void StageSelect(string level)
    {
        levelSelected = level;
    }

    public void DifficultySelect(string difficulty)
    {
        difficultySelected = difficulty;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levelSelected + " " + difficultySelected);
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CharacterSelect(string name)
    {
        PlayerPrefs.SetString("Character", name);
    }
}