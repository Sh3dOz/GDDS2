using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnimation : MonoBehaviour
{
    [SerializeField] Animator UIAnim;
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject nextButton, startButton;
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
