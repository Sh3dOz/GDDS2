using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnimation : MonoBehaviour
{
    [SerializeField] Animator UIAnim;
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject nextButton, startButton, backButton, backBack;
    public void StartNext()
    {
        
        uiElement.SetActive(false);
    }

    public void EndNext()
    {
        uiElement.SetActive(!uiElement.activeInHierarchy);
        backButton.SetActive(!backButton.activeInHierarchy);
        backBack.SetActive(!backBack.activeInHierarchy);
        nextButton.SetActive(!nextButton.activeInHierarchy);
        startButton.SetActive(!startButton.activeInHierarchy);
        
    }
    public void Next()
    {
        //Go to character select
        UIAnim.SetTrigger("Character");

    }

    public void Back()
    {
        UIAnim.SetTrigger("Level");
    }
}
