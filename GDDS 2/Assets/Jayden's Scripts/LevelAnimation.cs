using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnimation : MonoBehaviour
{
    [SerializeField] Animator UIAnim;
    [SerializeField] GameObject uiElement;
    [SerializeField] GameObject BG, startButton, backButton, backBack;
    public void StartNext()
    {
        
        uiElement.SetActive(false);
    }

    public void EndNext()
    {
        uiElement.SetActive(!uiElement.activeInHierarchy);
        backButton.SetActive(!backButton.activeInHierarchy);
        backBack.SetActive(!backBack.activeInHierarchy);
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

    public void EnableBG()
    {
        BG.SetActive(!BG.activeInHierarchy);
    }
}
