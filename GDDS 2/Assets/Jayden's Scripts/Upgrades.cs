using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour { 

    public List<string> allChar;
    public List<Sprite> charSprites;
    public GameObject charSprite;
    int selectedIndex;
    string selectedChar;
    public Text coinsCollectedText;
    public float coinsCollectedAll;

    public GameObject korgSkillUpgrade;
    public GameObject axelSkillUpgrade;
    public GameObject xavierSkillUpgrade;

    void Update() {

        UpdatePlayerCoins();
    }

    public void UpdatePlayerCoins() {
        coinsCollectedAll = PlayerPrefs.GetFloat("Coins");
        coinsCollectedText.text = "Coins Collected:" + coinsCollectedAll;
    }
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

    public void SelectingWhoToUpgradeSkills() {
        if(selectedIndex == 0) {
            korgSkillUpgrade.SetActive(true);
        }
        if (selectedIndex == 1) {
            axelSkillUpgrade.SetActive(true);
        }
        if (selectedIndex == 2) {
            xavierSkillUpgrade.SetActive(true);
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
        if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 2) {
            korgSkillUpgrade.SetActive(false);
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
        charSprite.GetComponent<Image>().sprite = charSprites[selectedIndex];
    }

    public void SwitchCharRight()
    {
        selectedIndex++;
        if (selectedIndex > allChar.Count-1)
        {
            selectedIndex = allChar.Count-1;
        }
        selectedChar = allChar[selectedIndex];
        charSprite.GetComponent<Image>().sprite = charSprites[selectedIndex];
    }

    public void Back()
    {
        SceneManager.LoadScene("Level Select");
    }

}
