using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string levelSelected;
    string difficultySelected;

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
}