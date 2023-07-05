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


    public float upgrade1Value = 1;
    public float upgrade2Value = 2;
    public float upgrade3Value = 3;

    [Header("Korg Upgrade")]
    public GameObject korgSkillUpgrade;
    public GameObject korgUpgradeButton;
    public GameObject korgUpgrade1;
    public GameObject korgUpgrade2;
    public GameObject korgUpgrade3;
    public GameObject korgSkillMaxed;

    [Header("Axel Upgrade")]
    public GameObject axelSkillUpgrade;
    public GameObject axelUpgradeButton;
    public GameObject axelUpgrade1;
    public GameObject axelUpgrade2;
    public GameObject axelUpgrade3;
    public GameObject axelSkillMaxed;

    [Header("Xavier Upgrade")]
    public GameObject xavierSkillUpgrade;
    public GameObject xavierUpgradeButton;
    public GameObject xavierUpgrade1;
    public GameObject xavierUpgrade2;
    public GameObject xavierUpgrade3;
    public GameObject xavierSkillMaxed;

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
        // KORG
        if (selectedIndex == 0) {
            korgSkillUpgrade.SetActive(true);
            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                korgUpgradeButton.SetActive(true);
                korgUpgrade1.SetActive(true);
                korgUpgrade2.SetActive(false);
                korgUpgrade3.SetActive(false);
                korgSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                korgUpgrade1.SetActive(false);
                korgUpgrade2.SetActive(true);
                korgUpgrade3.SetActive(false);
                korgSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                korgUpgrade1.SetActive(false);
                korgUpgrade2.SetActive(false);
                korgUpgrade3.SetActive(true);
                korgSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {
                korgUpgrade1.SetActive(false);
                korgUpgrade2.SetActive(false);
                korgUpgrade3.SetActive(false);
                korgSkillMaxed.SetActive(true);
            }
        }

        // AXEL
        else if (selectedIndex == 1) {
            axelSkillUpgrade.SetActive(true);
            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                axelUpgradeButton.SetActive(true);
                axelUpgrade1.SetActive(true);
                axelUpgrade2.SetActive(false);
                axelUpgrade3.SetActive(false);
                axelSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                axelUpgrade1.SetActive(false);
                axelUpgrade2.SetActive(true);
                axelUpgrade3.SetActive(false);
                axelSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                axelUpgrade1.SetActive(false);
                axelUpgrade2.SetActive(false);
                axelUpgrade3.SetActive(true);
                axelSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {
                axelUpgrade1.SetActive(false);
                axelUpgrade2.SetActive(false);
                axelUpgrade3.SetActive(false);
                axelSkillMaxed.SetActive(true);
            }
        }

        // XAVIER
        if (selectedIndex == 2) {
            xavierSkillUpgrade.SetActive(true);
            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                xavierUpgradeButton.SetActive(true);
                xavierUpgrade1.SetActive(true);
                xavierUpgrade2.SetActive(false);
                xavierUpgrade3.SetActive(false);
                xavierSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                xavierUpgrade1.SetActive(false);
                xavierUpgrade2.SetActive(true);
                xavierUpgrade3.SetActive(false);
                xavierSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                xavierUpgrade1.SetActive(false);
                xavierUpgrade2.SetActive(false);
                xavierUpgrade3.SetActive(true);
                xavierSkillMaxed.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {
                xavierUpgrade1.SetActive(false);
                xavierUpgrade2.SetActive(false);
                xavierUpgrade3.SetActive(false);
                xavierSkillMaxed.SetActive(true);
            }
        }
    }

        //UPGRADES
        public void UpgradeSkillForKorg()
    {
        if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
            PlayerPrefs.SetInt(selectedChar + "Skill", 1);
            PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
            korgSkillMaxed.SetActive(false); // For resets
            korgUpgradeButton.SetActive(true); // For resets
            korgUpgrade2.SetActive(true);
            korgUpgrade1.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0)
        {
            PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);

            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                korgSkillMaxed.SetActive(false); // For resets
                korgUpgradeButton.SetActive(true); // For resets
                korgUpgrade3.SetActive(true);
                korgUpgrade2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                korgUpgrade3.SetActive(false);
                axelUpgradeButton.SetActive(false);
                korgSkillMaxed.SetActive(true);
            }
        }

    }

    public void UpgradeSkillForAxel() {
        if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
            PlayerPrefs.SetInt(selectedChar + "Skill", 1);
            PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
            axelSkillMaxed.SetActive(false); // For resets
            axelUpgradeButton.SetActive(true); // For resets
            axelUpgrade2.SetActive(true);
            axelUpgrade1.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0) {

            PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);

            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                axelSkillMaxed.SetActive(false); // For resets
                axelUpgradeButton.SetActive(true); // For resets
                axelUpgrade3.SetActive(true);
                axelUpgrade2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                axelUpgrade3.SetActive(false);
                axelUpgradeButton.SetActive(false);
                axelSkillMaxed.SetActive(true);
            }
        }

    }

    public void UpgradeSkillForXavier() {
        if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
            PlayerPrefs.SetInt(selectedChar + "Skill", 1);
            PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
            xavierSkillMaxed.SetActive(false); // For resets
            xavierUpgradeButton.SetActive(true); // For resets
            xavierUpgrade2.SetActive(true);
            xavierUpgrade1.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0) {

            PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);

            if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                xavierSkillMaxed.SetActive(false); // For resets
                xavierUpgradeButton.SetActive(true); // For resets
                xavierUpgrade3.SetActive(true);
                xavierUpgrade2.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                xavierUpgrade3.SetActive(false);
                xavierUpgradeButton.SetActive(false);
                xavierSkillMaxed.SetActive(true);
            }
        }

    }

    public void CloseKorgUpgrade() {
        korgSkillUpgrade.SetActive(false);
    }
    public void CloseAxelUpgrade() {
        axelSkillUpgrade.SetActive(false);
    }

    public void CloseXavierUpgrade() {
        xavierSkillUpgrade.SetActive(false);
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
