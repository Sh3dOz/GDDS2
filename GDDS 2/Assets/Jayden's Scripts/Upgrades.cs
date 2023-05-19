using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    public List<string> allChar;
    public List<Sprite> charSprites;
    public GameObject charSprite;
    int selectedIndex;
    string selectedChar;

    public void UpgradePassive()
    {
        if(PlayerPrefs.GetInt(selectedChar + "Passive") == 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Passive", 1);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Passive") > 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Passive", PlayerPrefs.GetInt(selectedChar + "Passive") + 1);
        }
    }

    public void UpgradeSkill()
    {
        if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Skill", 1);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);
        }
    }

    public void UpgradePowerup()
    {
        if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);
        }
    }

    public void SwitchCharLeft()
    {
        selectedIndex--;
        if(selectedIndex < 0)
        {
            selectedIndex = 0;
        }
        selectedChar = allChar[selectedIndex];
        charSprite.GetComponent<SpriteRenderer>().sprite = charSprites[selectedIndex];
    }

    public void SwitchCharRight()
    {
        selectedIndex++;
        if (selectedIndex > allChar.Count-1)
        {
            selectedIndex = allChar.Count-1;
        }
        selectedChar = allChar[selectedIndex];
        charSprite.GetComponent<SpriteRenderer>().sprite = charSprites[selectedIndex];
    }

    public void Back()
    {
        SceneManager.LoadScene("Level Select");
    }
}
