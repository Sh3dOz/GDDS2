using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour {

    public List<string> allChar;
    public List<Sprite> charSprites;
    public List<Sprite> bgSprites;
    public List<GameObject> characterSprites;

    public GameObject charSprite;
    public GameObject bgSprite;

    int selectedIndex;
    string selectedChar;

    public Text coinsCollectedText;
    public float coinsCollectedAll;

    public AudioSource audioS;

    [Header("Passives")]
    public float passiveValue = 100;

    [Header("Korg Passive")]
    public GameObject korgPassiveUpgrade;
    public GameObject korgPassiveButton;
    public GameObject korgPassive;
    public GameObject korgPassiveMaxed;

    [Header("Axel Passive")]
    public GameObject axelPassiveUpgrade;
    public GameObject axelPassiveButton;
    public GameObject axelPassive;
    public GameObject axelPassiveMaxed;

    [Header("Xavier Passive")]
    public GameObject xavierPassiveUpgrade;
    public GameObject xavierPassiveButton;
    public GameObject xavierPassive;
    public GameObject xavierPassiveMaxed;


    [Header("Upgrades")]
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


    [Header("Powerup")]
    public float powerup1Value = 4;
    public float powerup2Value = 5;
    public float powerup3Value = 6;

    [Header("Korg Upgrade")]
    public GameObject korgPowerupUpgrade;
    public GameObject korgPowerupButton;
    public GameObject korgPowerup1;
    public GameObject korgPowerup2;
    public GameObject korgPowerup3;
    public GameObject korgPowerupMaxed;

    [Header("Axel Upgrade")]
    public GameObject axelPowerupUpgrade;
    public GameObject axelPowerupButton;
    public GameObject axelPowerup1;
    public GameObject axelPowerup2;
    public GameObject axelPowerup3;
    public GameObject axelPowerupMaxed;

    [Header("Xavier Upgrade")]
    public GameObject xavierPowerupUpgrade;
    public GameObject xavierPowerupButton;
    public GameObject xavierPowerup1;
    public GameObject xavierPowerup2;
    public GameObject xavierPowerup3;
    public GameObject xavierPowerupMaxed;

    void Update() {

        UpdatePlayerCoins();
        UpdatePassiveLevelStatus();
        UpdateSkillLevelStatus();
        UpdatePowerupLevelStatus();
        UpdateCharacterSprites();

    }

    public void UpdatePlayerCoins() {
        coinsCollectedAll = PlayerPrefs.GetFloat("Coins");
        coinsCollectedText.text = "=" + coinsCollectedAll;
    }

    public void UpdateCharacterSprites() {
        if (selectedIndex == 0) {
            characterSprites[0].SetActive(true);
            characterSprites[1].SetActive(false);
            characterSprites[2].SetActive(false);

        }
        else if (selectedIndex == 1) {
            characterSprites[0].SetActive(false);
            characterSprites[1].SetActive(true);
            characterSprites[2].SetActive(false);
        }

        else if (selectedIndex == 2) {
            characterSprites[0].SetActive(false);
            characterSprites[1].SetActive(false);
            characterSprites[2].SetActive(true);
        }
    }

    public void UpdatePassiveLevelStatus() {
        if (selectedIndex == 0) {
            if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                PlayerPrefs.SetInt("PassiveForKorg", 0);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Passive") == 1) {
                PlayerPrefs.SetInt("PassiveForKorg", 1);

            }
        }

        if (selectedIndex == 1) {
            if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                PlayerPrefs.SetInt("PassiveForAxel", 0);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Passive") == 1) {
                PlayerPrefs.SetInt("PassiveForAxel", 1);

            }
        }

        if (selectedIndex == 2) {
            if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                PlayerPrefs.SetInt("PassiveForXavier", 0);
            }
            else if (PlayerPrefs.GetInt(selectedChar + "Passive") == 1) {
                PlayerPrefs.SetInt("PassiveForXavier", 1);
            }
        }
    }
                
            

            public void SelectingWhoToUpgradePassive() {
                // KORG
                if (selectedIndex == 0) {
                    korgPassiveUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                        korgPassiveButton.SetActive(true);
                        korgPassive.SetActive(true);
                        korgPassiveMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Passive") >= 1) {
                        korgPassiveButton.SetActive(false);
                        korgPassive.SetActive(false);
                        korgPassiveMaxed.SetActive(true);
                    }
                }

                // Axel
                else if (selectedIndex == 1) {
                    axelPassiveUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                        axelPassiveButton.SetActive(true);
                        axelPassive.SetActive(true);
                        axelPassiveMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Passive") >= 1) {
                        axelPassiveButton.SetActive(false);
                        axelPassive.SetActive(false);
                        axelPassiveMaxed.SetActive(true);
                    }
                }
                // Xavier
                else if (selectedIndex == 2) {
                    xavierPassiveUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                        xavierPassiveButton.SetActive(true);
                        xavierPassive.SetActive(true);
                        xavierPassiveMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Passive") >= 1) {
                        xavierPassiveButton.SetActive(false);
                        xavierPassive.SetActive(false);
                        xavierPassiveMaxed.SetActive(true);
                    }
                }
            }


            public void UpgradePassiveForKorg() {
                if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Passive", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - passiveValue);
                    korgPassiveMaxed.SetActive(true); // For resets
                    korgPassiveButton.SetActive(false); // For resets
                    korgPassive.SetActive(false);
                }
            }

            public void UpgradePassiveForAxel() {
                if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Passive", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - passiveValue);
                    axelPassiveMaxed.SetActive(true); // For resets
                    axelPassiveButton.SetActive(false); // For resets
                    axelPassive.SetActive(false);
                }
            }

            public void UpgradePassiveForXavier() {
                if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Passive", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - passiveValue);
                    xavierPassiveMaxed.SetActive(true); // For resets
                    xavierPassiveButton.SetActive(false); // For resets
                    xavierPassive.SetActive(false);
                }
            }

            public void CloseKorgPassive() {
                korgPassiveUpgrade.SetActive(false);
            }
            public void CloseAxelPassive() {
                axelPassiveUpgrade.SetActive(false);
            }

            public void CloseXavierPassive() {
                xavierPassiveUpgrade.SetActive(false);
            }

            public void ResetPassiveLevel() {
                PlayerPrefs.SetInt(selectedChar + "Passive", 0);
            }




            public void UpdateSkillLevelStatus() {
                if (selectedIndex == 0) {
                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                        PlayerPrefs.SetInt("SkillForKorg", 0);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                        PlayerPrefs.SetInt("SkillForKorg", 1);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        PlayerPrefs.SetInt("SkillForKorg", 2);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                        PlayerPrefs.SetInt("SkillForKorg", 3);
                    }
                }

                if (selectedIndex == 1) {
                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                        PlayerPrefs.SetInt("SkillForAxel", 0);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                        PlayerPrefs.SetInt("SkillForAxel", 1);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        PlayerPrefs.SetInt("SkillForAxel", 2);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                        PlayerPrefs.SetInt("SkillForAxel", 3);
                    }
                }

                if (selectedIndex == 2) {
                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                        PlayerPrefs.SetInt("SkillForXavier", 0);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                        PlayerPrefs.SetInt("SkillForXavier", 1);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        PlayerPrefs.SetInt("SkillForXavier", 2);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                        PlayerPrefs.SetInt("SkillForXavier", 3);
                    }
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
                        axelUpgradeButton.SetActive(true);
                        axelUpgrade1.SetActive(false);
                        axelUpgrade2.SetActive(true);
                        axelUpgrade3.SetActive(false);
                        axelSkillMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        axelUpgradeButton.SetActive(true);
                        axelUpgrade1.SetActive(false);
                        axelUpgrade2.SetActive(false);
                        axelUpgrade3.SetActive(true);
                        axelSkillMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {;
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
                        xavierUpgradeButton.SetActive(true);
                        xavierUpgrade1.SetActive(false);
                        xavierUpgrade2.SetActive(true);
                        xavierUpgrade3.SetActive(false);
                        xavierSkillMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        xavierUpgradeButton.SetActive(true);
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
            public void UpgradeSkillForKorg() {
                if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Skill", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
                    korgSkillMaxed.SetActive(false); // For resets
                    korgUpgradeButton.SetActive(true); // For resets
                    korgUpgrade2.SetActive(true);
                    korgUpgrade1.SetActive(false);
                }
                else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0) {
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





            public void UpdatePowerupLevelStatus() {
                if (selectedIndex == 0) {
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        PlayerPrefs.SetInt("PowerupForKorg", 0);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        PlayerPrefs.SetInt("PowerupForKorg", 1);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetInt("PowerupForKorg", 2);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetInt("PowerupForKorg", 3);
                    }
                }

                if (selectedIndex == 1) {
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        PlayerPrefs.SetInt("PowerupForAxel", 0);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        PlayerPrefs.SetInt("PowerupForAxel", 1);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetInt("PowerupForAxel", 2);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetInt("PowerupForAxel", 3);
                    }
                }

                if (selectedIndex == 2) {
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        PlayerPrefs.SetInt("PowerupForXavier", 0);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        PlayerPrefs.SetInt("PowerupForXavier", 1);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetInt("PowerupForXavier", 2);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetInt("PowerupForXavier", 3);
                    }
                }
            }

            public void SelectingWhoToUpgradePowerup() {
                // KORG
                if (selectedIndex == 0) {
                    korgPowerupUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        korgPowerupButton.SetActive(true);
                        korgPowerup1.SetActive(true);
                        korgPowerup2.SetActive(false);
                        korgPowerup3.SetActive(false);
                        korgPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        korgPowerup1.SetActive(false);
                        korgPowerup2.SetActive(true);
                        korgPowerup3.SetActive(false);
                        korgPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        korgPowerup1.SetActive(false);
                        korgPowerup2.SetActive(false);
                        korgPowerup3.SetActive(true);
                        korgPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") >= 3) {
                        korgPowerup1.SetActive(false);
                        korgPowerup2.SetActive(false);
                        korgPowerup3.SetActive(false);
                        korgPowerupMaxed.SetActive(true);
                    }
                }

                // AXEL
                else if (selectedIndex == 1) {
                    axelPowerupUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        axelPowerupButton.SetActive(true);
                        axelPowerup1.SetActive(true);
                        axelPowerup2.SetActive(false);
                        axelPowerup3.SetActive(false);
                        axelPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        axelPowerupButton.SetActive(true);
                        axelPowerup1.SetActive(false);
                        axelPowerup2.SetActive(true);
                        axelPowerup3.SetActive(false);
                        axelPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        axelPowerupButton.SetActive(true);
                        axelPowerup1.SetActive(false);
                        axelPowerup2.SetActive(false);
                        axelPowerup3.SetActive(true);
                        axelPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") >= 3) {
                        ;
                        axelPowerup1.SetActive(false);
                        axelPowerup2.SetActive(false);
                        axelPowerup3.SetActive(false);
                        axelPowerupMaxed.SetActive(true);
                    }
                }

                // XAVIER
                if (selectedIndex == 2) {
                    xavierPowerupUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        xavierPowerupButton.SetActive(true);
                        xavierPowerup1.SetActive(true);
                        xavierPowerup2.SetActive(false);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        xavierPowerupButton.SetActive(true);
                        xavierPowerup1.SetActive(false);
                        xavierPowerup2.SetActive(true);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        xavierPowerupButton.SetActive(true);
                        xavierPowerup1.SetActive(false);
                        xavierPowerup2.SetActive(false);
                        xavierPowerup3.SetActive(true);
                        xavierPowerupMaxed.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") >= 3) {
                        xavierPowerup1.SetActive(false);
                        xavierPowerup2.SetActive(false);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupMaxed.SetActive(true);
                    }
                }
            }

            //POWERUPS
            public void UpgradePowerupForKorg() {
                if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup1Value);
                    korgPowerupMaxed.SetActive(false); // For resets
                    korgPowerupButton.SetActive(true); // For resets
                    korgPowerup2.SetActive(true);
                    korgPowerup1.SetActive(false);
                }
                else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup2Value);
                        korgPowerupMaxed.SetActive(false); // For resets
                        korgPowerupButton.SetActive(true); // For resets
                        korgPowerup3.SetActive(true);
                        korgPowerup2.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup3Value);
                        korgPowerup3.SetActive(false);
                        axelPowerupButton.SetActive(false);
                        korgPowerupMaxed.SetActive(true);
                    }
                }

            }

            public void UpgradePowerupForAxel() {
                if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup1Value);
                    axelPowerupMaxed.SetActive(false); // For resets
                    axelPowerupButton.SetActive(true); // For resets
                    axelPowerup2.SetActive(true);
                    axelPowerup1.SetActive(false);
                }
                else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0) {

                    PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup2Value);
                        axelPowerupMaxed.SetActive(false); // For resets
                        axelPowerupButton.SetActive(true); // For resets
                        axelPowerup3.SetActive(true);
                        axelPowerup2.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup3Value);
                        axelPowerup3.SetActive(false);
                        axelPowerupButton.SetActive(false);
                        axelPowerupMaxed.SetActive(true);
                    }
                }

            }

            public void UpgradePowerupForXavier() {
                if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup1Value);
                    xavierPowerupMaxed.SetActive(false); // For resets
                    xavierPowerupButton.SetActive(true); // For resets
                    xavierPowerup2.SetActive(true);
                    xavierPowerup1.SetActive(false);
                }
                else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0) {

                    PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup2Value);
                        xavierPowerupMaxed.SetActive(false); // For resets
                        xavierPowerupButton.SetActive(true); // For resets
                        xavierPowerup3.SetActive(true);
                        xavierPowerup2.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup3Value);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupButton.SetActive(false);
                        xavierPowerupMaxed.SetActive(true);
                    }
                }

            }

            public void CloseKorgPowerup() {
                korgPowerupUpgrade.SetActive(false);
            }
            public void CloseAxelPowerup() {
                axelPowerupUpgrade.SetActive(false);
            }

            public void CloseXavierPowerup() {
                xavierPowerupUpgrade.SetActive(false);
            }

            public void ResetPowerupLevel() {
                PlayerPrefs.SetInt(selectedChar + "Powerup", 0);
            }





            public void SwitchCharLeft() {
                selectedIndex--;
                if (selectedIndex < 0) {
                    selectedIndex = 0;
                }
                selectedChar = allChar[selectedIndex];
                bgSprite.GetComponent<Image>().sprite = bgSprites[selectedIndex];
            }

            public void SwitchCharRight() {
                selectedIndex++;
                if (selectedIndex > allChar.Count - 1) {
                    selectedIndex = allChar.Count - 1;
                }
                selectedChar = allChar[selectedIndex];
                bgSprite.GetComponent<Image>().sprite = bgSprites[selectedIndex];
            }

            public void Back() {
                SceneManager.LoadScene("Level Select");
            }

        }
