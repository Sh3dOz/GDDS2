using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string levelSelected;
    [SerializeField] string difficultySelected;
    [SerializeField] Animator UIAnim;
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject nextButton, startButton;

    public void StageSelect(string level)
    {
        levelSelected = level;
    }

    public void DifficultySelect(string difficulty)
    {
        difficultySelected = difficulty;
    }

    public void Next()
    {
        //Go to character select
        UIAnim.SetTrigger("Character");

    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levelSelected + " " + difficultySelected);
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StartNext()
    {
        uiElement.SetActive(false);
    }

    public void EndNext()
    {
        nextButton.SetActive(nextButton.activeInHierarchy);
        startButton.SetActive(startButton.activeInHierarchy);
        uiElement.SetActive(uiElement.activeInHierarchy);
    }
}