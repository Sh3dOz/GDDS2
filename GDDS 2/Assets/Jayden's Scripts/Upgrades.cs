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
    public GameObject upgradeButton;
    public GameObject korgUpgrade1;
    public float upgrade1Value = 20;
    public GameObject korgUpgrade2;
    public float upgrade2Value = 100;
    public GameObject korgUpgrade3;
    public float upgrade3Value = 1000;
    public GameObject korrgSkillMaxed;


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
        if (selectedIndex == 0) {
            korgSkillUpgrade.SetActive(true);
            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                korgUpgrade1.SetActive(true);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                korgUpgrade2.SetActive(true);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                korgUpgrade3.SetActive(true);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {
                upgradeButton.SetActive(false);
                korrgSkillMaxed.SetActive(true);
            }
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
            PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
            korgUpgrade2.SetActive(true);
            korgUpgrade1.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0)
        {

            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                korgUpgrade3.SetActive(true);
                korgUpgrade2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                korgUpgrade3.SetActive(false);
                upgradeButton.SetActive(false);
                korrgSkillMaxed.SetActive(true);
            }
        }
        PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);
    }

    public void ResetSkillLevel() {
        PlayerPrefs.SetInt(selectedChar + "Skill", 0);
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
