using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    LevelSelect select;
    public string difficulty;
    private void Start()
    {
        select = FindObjectOfType<LevelSelect>();
    }
    public void DifficultyCheck()
    {
        if (transform.parent.name == select.levelSelected)
        {
            select.DifficultySelect(difficulty);
        }
    }
}
