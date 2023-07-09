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
    public AudioClip pressingSound;
    public AudioClip upgradeSound;
    public GameObject interactables;

    [Header("Passives")]
    public float passiveValue = 100;

    [Header("Korg Passive")]
    public GameObject korgPassiveUpgrade;
    public GameObject korgPassiveButton;
    public GameObject korgPassive;
    public GameObject korgPassiveMaxed;
    public GameObject korgPassiveEmpty;
    public GameObject korgPassiveFull;

    [Header("Axel Passive")]
    public GameObject axelPassiveUpgrade;
    public GameObject axelPassiveButton;
    public GameObject axelPassive;
    public GameObject axelPassiveMaxed;
    public GameObject axelPassiveEmpty;
    public GameObject axelPassiveFull;

    [Header("Xavier Passive")]
    public GameObject xavierPassiveUpgrade;
    public GameObject xavierPassiveButton;
    public GameObject xavierPassive;
    public GameObject xavierPassiveMaxed;
    public GameObject xavierPassiveEmpty;
    public GameObject xavierPassiveFull;


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
    public GameObject korgSkillEmpty;
    public GameObject korgSkillOneThird;
    public GameObject korgSkillTwoThird;
    public GameObject korgSkillFull;

    [Header("Axel Upgrade")]
    public GameObject axelSkillUpgrade;
    public GameObject axelUpgradeButton;
    public GameObject axelUpgrade1;
    public GameObject axelUpgrade2;
    public GameObject axelUpgrade3;
    public GameObject axelSkillMaxed;
    public GameObject axelSkillEmpty;
    public GameObject axelSkillOneThird;
    public GameObject axelSkillTwoThird;
    public GameObject axelSkillFull;

    [Header("Xavier Upgrade")]
    public GameObject xavierSkillUpgrade;
    public GameObject xavierUpgradeButton;
    public GameObject xavierUpgrade1;
    public GameObject xavierUpgrade2;
    public GameObject xavierUpgrade3;
    public GameObject xavierSkillMaxed;
    public GameObject xavierSkillEmpty;
    public GameObject xavierSkillOneThird;
    public GameObject xavierSkillTwoThird;
    public GameObject xavierSkillFull;



    [Header("Powerup")]
    public float powerup1Value = 4;
    public float powerup2Value = 5;
    public float powerup3Value = 6;

    [Header("Korg Powerup")]
    public GameObject korgPowerupUpgrade;
    public GameObject korgPowerupButton;
    public GameObject korgPowerup1;
    public GameObject korgPowerup2;
    public GameObject korgPowerup3;
    public GameObject korgPowerupMaxed;
    public GameObject korgPowerupEmpty;
    public GameObject korgPowerupOneThird;
    public GameObject korgPowerupTwoThird;
    public GameObject korgPowerupFull;

    [Header("Axel Powerup")]
    public GameObject axelPowerupUpgrade;
    public GameObject axelPowerupButton;
    public GameObject axelPowerup1;
    public GameObject axelPowerup2;
    public GameObject axelPowerup3;
    public GameObject axelPowerupMaxed;
    public GameObject axelPowerupEmpty;
    public GameObject axelPowerupOneThird;
    public GameObject axelPowerupTwoThird;
    public GameObject axelPowerupFull;

    [Header("Xavier Powerup")]
    public GameObject xavierPowerupUpgrade;
    public GameObject xavierPowerupButton;
    public GameObject xavierPowerup1;
    public GameObject xavierPowerup2;
    public GameObject xavierPowerup3;
    public GameObject xavierPowerupMaxed;
    public GameObject xavierPowerupEmpty;
    public GameObject xavierPowerupOneThird;
    public GameObject xavierPowerupTwoThird;
    public GameObject xavierPowerupFull;

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
                audioS.PlayOneShot(pressingSound);
                interactables.SetActive(false);
                // KORG
                if (selectedIndex == 0) {
                    korgPassiveUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                        korgPassiveButton.SetActive(true);
                        korgPassive.SetActive(true);
                        korgPassiveMaxed.SetActive(false);
                korgPassiveEmpty.SetActive(true);
                korgPassiveFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Passive") >= 1) {
                        korgPassiveButton.SetActive(false);
                        korgPassive.SetActive(false);
                        korgPassiveMaxed.SetActive(true);
                korgPassiveEmpty.SetActive(false);
                korgPassiveFull.SetActive(true);
                    }
                }

                // Axel
                else if (selectedIndex == 1) {
                    axelPassiveUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                        axelPassiveButton.SetActive(true);
                        axelPassive.SetActive(true);
                        axelPassiveMaxed.SetActive(false);
                axelPassiveEmpty.SetActive(true);
                axelPassiveFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Passive") >= 1) {
                        axelPassiveButton.SetActive(false);
                        axelPassive.SetActive(false);
                        axelPassiveMaxed.SetActive(true);
                axelPassiveEmpty.SetActive(false);
                axelPassiveFull.SetActive(true);
                    }
                }
                // Xavier
                else if (selectedIndex == 2) {
                    xavierPassiveUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                        xavierPassiveButton.SetActive(true);
                        xavierPassive.SetActive(true);
                        xavierPassiveMaxed.SetActive(false);
                xavierPassiveEmpty.SetActive(true);
                xavierPassiveFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Passive") >= 1) {
                        xavierPassiveButton.SetActive(false);
                        xavierPassive.SetActive(false);
                        xavierPassiveMaxed.SetActive(true);
                xavierPassiveEmpty.SetActive(false);
                xavierPassiveFull.SetActive(true);
            }
                }
            }


            public void UpgradePassiveForKorg() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Passive", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - passiveValue);
                    korgPassiveMaxed.SetActive(true); // For resets
                    korgPassiveButton.SetActive(false); // For resets
                    korgPassive.SetActive(false);
            korgPassiveEmpty.SetActive(false);
            korgPassiveFull.SetActive(true);
        }
            }

            public void UpgradePassiveForAxel() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Passive", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - passiveValue);
                    axelPassiveMaxed.SetActive(true); // For resets
                    axelPassiveButton.SetActive(false); // For resets
                    axelPassive.SetActive(false);
            axelPassiveEmpty.SetActive(false);
            axelPassiveFull.SetActive(true);
        }
            }

            public void UpgradePassiveForXavier() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Passive") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Passive", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - passiveValue);
                    xavierPassiveMaxed.SetActive(true); // For resets
                    xavierPassiveButton.SetActive(false); // For resets
                    xavierPassive.SetActive(false);
            xavierPassiveEmpty.SetActive(false);
            xavierPassiveFull.SetActive(true);
        }
            }

            public void CloseKorgPassive() {
                audioS.PlayOneShot(pressingSound);
                korgPassiveUpgrade.SetActive(false);
                interactables.SetActive(true);

            }
            public void CloseAxelPassive() {
                audioS.PlayOneShot(pressingSound);
                axelPassiveUpgrade.SetActive(false);
                interactables.SetActive(true);
            }

            public void CloseXavierPassive() {
                audioS.PlayOneShot(pressingSound);
                xavierPassiveUpgrade.SetActive(false);
                interactables.SetActive(true);
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
                audioS.PlayOneShot(pressingSound);
                interactables.SetActive(false);
                // KORG
                if (selectedIndex == 0) {
                    korgSkillUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                        korgUpgradeButton.SetActive(true);
                        korgUpgrade1.SetActive(true);
                        korgUpgrade2.SetActive(false);
                        korgUpgrade3.SetActive(false);
                        korgSkillMaxed.SetActive(false);
                korgSkillEmpty.SetActive(true);
                korgSkillOneThird.SetActive(false);
                korgSkillTwoThird.SetActive(false);
                korgSkillFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                        korgUpgrade1.SetActive(false);
                        korgUpgrade2.SetActive(true);
                        korgUpgrade3.SetActive(false);
                        korgSkillMaxed.SetActive(false);
                korgSkillEmpty.SetActive(false);
                korgSkillOneThird.SetActive(true);
                korgSkillTwoThird.SetActive(false);
                korgSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        korgUpgrade1.SetActive(false);
                        korgUpgrade2.SetActive(false);
                        korgUpgrade3.SetActive(true);
                        korgSkillMaxed.SetActive(false);
                korgSkillEmpty.SetActive(false);
                korgSkillOneThird.SetActive(false);
                korgSkillTwoThird.SetActive(true);
                korgSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {
                        korgUpgrade1.SetActive(false);
                        korgUpgrade2.SetActive(false);
                        korgUpgrade3.SetActive(false);
                        korgSkillMaxed.SetActive(true);
                korgSkillEmpty.SetActive(false);
                korgSkillOneThird.SetActive(false);
                korgSkillTwoThird.SetActive(false);
                korgSkillFull.SetActive(true);
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
                axelSkillEmpty.SetActive(true);
                axelSkillOneThird.SetActive(false);
                axelSkillTwoThird.SetActive(false);
                axelSkillFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                        axelUpgradeButton.SetActive(true);
                        axelUpgrade1.SetActive(false);
                        axelUpgrade2.SetActive(true);
                        axelUpgrade3.SetActive(false);
                        axelSkillMaxed.SetActive(false);
                axelSkillEmpty.SetActive(false);
                axelSkillOneThird.SetActive(true);
                axelSkillTwoThird.SetActive(false);
                axelSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        axelUpgradeButton.SetActive(true);
                        axelUpgrade1.SetActive(false);
                        axelUpgrade2.SetActive(false);
                        axelUpgrade3.SetActive(true);
                        axelSkillMaxed.SetActive(false);
                axelSkillEmpty.SetActive(false);
                axelSkillOneThird.SetActive(false);
                axelSkillTwoThird.SetActive(true);
                axelSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {;
                        axelUpgrade1.SetActive(false);
                        axelUpgrade2.SetActive(false);
                        axelUpgrade3.SetActive(false);
                        axelSkillMaxed.SetActive(true);
                axelSkillEmpty.SetActive(false);
                axelSkillOneThird.SetActive(false);
                axelSkillTwoThird.SetActive(false);
                axelSkillFull.SetActive(true);
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
                xavierSkillEmpty.SetActive(true);
                xavierSkillOneThird.SetActive(false);
                xavierSkillTwoThird.SetActive(false);
                xavierSkillFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 1) {
                        xavierUpgradeButton.SetActive(true);
                        xavierUpgrade1.SetActive(false);
                        xavierUpgrade2.SetActive(true);
                        xavierUpgrade3.SetActive(false);
                        xavierSkillMaxed.SetActive(false);
                xavierSkillEmpty.SetActive(false);
                xavierSkillOneThird.SetActive(true);
                xavierSkillTwoThird.SetActive(false);
                xavierSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        xavierUpgradeButton.SetActive(true);
                        xavierUpgrade1.SetActive(false);
                        xavierUpgrade2.SetActive(false);
                        xavierUpgrade3.SetActive(true);
                        xavierSkillMaxed.SetActive(false);
                xavierSkillEmpty.SetActive(false);
                xavierSkillOneThird.SetActive(false);
                xavierSkillTwoThird.SetActive(true);
                xavierSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") >= 3) {
                        xavierUpgrade1.SetActive(false);
                        xavierUpgrade2.SetActive(false);
                        xavierUpgrade3.SetActive(false);
                        xavierSkillMaxed.SetActive(true);
                xavierSkillEmpty.SetActive(false);
                xavierSkillOneThird.SetActive(false);
                xavierSkillTwoThird.SetActive(false);
                xavierSkillFull.SetActive(true);
            }
                }
            }

            //UPGRADES
            public void UpgradeSkillForKorg() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Skill", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
                    korgSkillMaxed.SetActive(false); // For resets
                    korgUpgradeButton.SetActive(true); // For resets
                    korgUpgrade2.SetActive(true);
                    korgUpgrade1.SetActive(false);
            korgSkillEmpty.SetActive(false);
            korgSkillOneThird.SetActive(true);
            korgSkillTwoThird.SetActive(false);
            korgSkillFull.SetActive(false);
        }
                else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0) {
                    PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                        korgSkillMaxed.SetActive(false); // For resets
                        korgUpgradeButton.SetActive(true); // For resets
                        korgUpgrade3.SetActive(true);
                        korgUpgrade2.SetActive(false);
                korgSkillEmpty.SetActive(false);
                korgSkillOneThird.SetActive(false);
                korgSkillTwoThird.SetActive(true);
                korgSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                        korgUpgrade3.SetActive(false);
                        axelUpgradeButton.SetActive(false);
                        korgSkillMaxed.SetActive(true);
                korgSkillEmpty.SetActive(false);
                korgSkillOneThird.SetActive(false);
                korgSkillTwoThird.SetActive(false);
                korgSkillFull.SetActive(true);
                    }
                }

            }

            public void UpgradeSkillForAxel() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Skill", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
                    axelSkillMaxed.SetActive(false); // For resets
                    axelUpgradeButton.SetActive(true); // For resets
                    axelUpgrade2.SetActive(true);
                    axelUpgrade1.SetActive(false);
            axelSkillEmpty.SetActive(false);
            axelSkillOneThird.SetActive(true);
            axelSkillTwoThird.SetActive(false);
            axelSkillFull.SetActive(false);
        }
                else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0) {

                    PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                        axelSkillMaxed.SetActive(false); // For resets
                        axelUpgradeButton.SetActive(true); // For resets
                        axelUpgrade3.SetActive(true);
                        axelUpgrade2.SetActive(false);
                axelSkillEmpty.SetActive(false);
                axelSkillOneThird.SetActive(false);
                axelSkillTwoThird.SetActive(true);
                axelSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                        axelUpgrade3.SetActive(false);
                        axelUpgradeButton.SetActive(false);
                        axelSkillMaxed.SetActive(true);
                axelSkillEmpty.SetActive(false);
                axelSkillOneThird.SetActive(false);
                axelSkillTwoThird.SetActive(false);
                axelSkillFull.SetActive(true);
                    }
                }

            }

            public void UpgradeSkillForXavier() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Skill") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Skill", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade1Value);
                    xavierSkillMaxed.SetActive(false); // For resets
                    xavierUpgradeButton.SetActive(true); // For resets
                    xavierUpgrade2.SetActive(true);
                    xavierUpgrade1.SetActive(false);
            xavierSkillEmpty.SetActive(false);
            xavierSkillOneThird.SetActive(true);
            xavierSkillTwoThird.SetActive(false);
            xavierSkillFull.SetActive(false);
        }
                else if (PlayerPrefs.GetInt(selectedChar + "Skill") > 0) {

                    PlayerPrefs.SetInt(selectedChar + "Skill", PlayerPrefs.GetInt(selectedChar + "Skill") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Skill") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade2Value);
                        xavierSkillMaxed.SetActive(false); // For resets
                        xavierUpgradeButton.SetActive(true); // For resets
                        xavierUpgrade3.SetActive(true);
                        xavierUpgrade2.SetActive(false);
                xavierSkillEmpty.SetActive(false);
                xavierSkillOneThird.SetActive(false);
                xavierSkillTwoThird.SetActive(true);
                xavierSkillFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Skill") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - upgrade3Value);
                        xavierUpgrade3.SetActive(false);
                        xavierUpgradeButton.SetActive(false);
                        xavierSkillMaxed.SetActive(true);
                xavierSkillEmpty.SetActive(false);
                xavierSkillOneThird.SetActive(false);
                xavierSkillTwoThird.SetActive(false);
                xavierSkillFull.SetActive(true);
                    }
                }

            }

            public void CloseKorgUpgrade() {
                audioS.PlayOneShot(pressingSound);
                korgSkillUpgrade.SetActive(false);
                interactables.SetActive(true);
            }
            public void CloseAxelUpgrade() {
                audioS.PlayOneShot(pressingSound);
                axelSkillUpgrade.SetActive(false);
                interactables.SetActive(true);
            }

            public void CloseXavierUpgrade() {
                audioS.PlayOneShot(pressingSound);
                xavierSkillUpgrade.SetActive(false);
                interactables.SetActive(true);
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
                audioS.PlayOneShot(pressingSound);
                interactables.SetActive(false);
                // KORG
                if (selectedIndex == 0) {
                    korgPowerupUpgrade.SetActive(true);
                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                        korgPowerupButton.SetActive(true);
                        korgPowerup1.SetActive(true);
                        korgPowerup2.SetActive(false);
                        korgPowerup3.SetActive(false);
                        korgPowerupMaxed.SetActive(false);
                korgPowerupEmpty.SetActive(true);
                korgPowerupOneThird.SetActive(false);
                korgPowerupTwoThird.SetActive(false);
                korgPowerupFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        korgPowerup1.SetActive(false);
                        korgPowerup2.SetActive(true);
                        korgPowerup3.SetActive(false);
                        korgPowerupMaxed.SetActive(false);
                korgPowerupEmpty.SetActive(false);
                korgPowerupOneThird.SetActive(true);
                korgPowerupTwoThird.SetActive(false);
                korgPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        korgPowerup1.SetActive(false);
                        korgPowerup2.SetActive(false);
                        korgPowerup3.SetActive(true);
                        korgPowerupMaxed.SetActive(false);
                korgPowerupEmpty.SetActive(false);
                korgPowerupOneThird.SetActive(false);
                korgPowerupTwoThird.SetActive(true);
                korgPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") >= 3) {
                        korgPowerup1.SetActive(false);
                        korgPowerup2.SetActive(false);
                        korgPowerup3.SetActive(false);
                        korgPowerupMaxed.SetActive(true);
                korgPowerupEmpty.SetActive(false);
                korgPowerupOneThird.SetActive(false);
                korgPowerupTwoThird.SetActive(false);
                korgPowerupFull.SetActive(true);
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
                axelPowerupEmpty.SetActive(true);
                axelPowerupOneThird.SetActive(false);
                axelPowerupTwoThird.SetActive(false);
                axelPowerupFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        axelPowerupButton.SetActive(true);
                        axelPowerup1.SetActive(false);
                        axelPowerup2.SetActive(true);
                        axelPowerup3.SetActive(false);
                        axelPowerupMaxed.SetActive(false);
                axelPowerupEmpty.SetActive(false);
                axelPowerupOneThird.SetActive(true);
                axelPowerupTwoThird.SetActive(false);
                axelPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        axelPowerupButton.SetActive(true);
                        axelPowerup1.SetActive(false);
                        axelPowerup2.SetActive(false);
                        axelPowerup3.SetActive(true);
                        axelPowerupMaxed.SetActive(false);
                axelPowerupEmpty.SetActive(false);
                axelPowerupOneThird.SetActive(false);
                axelPowerupTwoThird.SetActive(true);
                axelPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") >= 3) {
                        ;
                        axelPowerup1.SetActive(false);
                        axelPowerup2.SetActive(false);
                        axelPowerup3.SetActive(false);
                        axelPowerupMaxed.SetActive(true);
                axelPowerupEmpty.SetActive(false);
                axelPowerupOneThird.SetActive(false);
                axelPowerupTwoThird.SetActive(false);
                axelPowerupFull.SetActive(true);
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
                xavierPowerupEmpty.SetActive(true);
                xavierPowerupOneThird.SetActive(false);
                xavierPowerupTwoThird.SetActive(false);
                xavierPowerupFull.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 1) {
                        xavierPowerupButton.SetActive(true);
                        xavierPowerup1.SetActive(false);
                        xavierPowerup2.SetActive(true);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupMaxed.SetActive(false);
                xavierPowerupEmpty.SetActive(false);
                xavierPowerupOneThird.SetActive(true);
                xavierPowerupTwoThird.SetActive(false);
                xavierPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        xavierPowerupButton.SetActive(true);
                        xavierPowerup1.SetActive(false);
                        xavierPowerup2.SetActive(false);
                        xavierPowerup3.SetActive(true);
                        xavierPowerupMaxed.SetActive(false);
                xavierPowerupEmpty.SetActive(false);
                xavierPowerupOneThird.SetActive(false);
                xavierPowerupTwoThird.SetActive(true);
                xavierPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") >= 3) {
                        xavierPowerup1.SetActive(false);
                        xavierPowerup2.SetActive(false);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupMaxed.SetActive(true);
                xavierPowerupEmpty.SetActive(false);
                xavierPowerupOneThird.SetActive(false);
                xavierPowerupTwoThird.SetActive(false);
                xavierPowerupFull.SetActive(true);
            }
                }
            }

            //POWERUPS
            public void UpgradePowerupForKorg() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup1Value);
                    korgPowerupMaxed.SetActive(false); // For resets
                    korgPowerupButton.SetActive(true); // For resets
                    korgPowerup2.SetActive(true);
                    korgPowerup1.SetActive(false);
            korgPowerupEmpty.SetActive(false);
            korgPowerupOneThird.SetActive(true);
            korgPowerupTwoThird.SetActive(false);
            korgPowerupFull.SetActive(false);
        }
                else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup2Value);
                        korgPowerupMaxed.SetActive(false); // For resets
                        korgPowerupButton.SetActive(true); // For resets
                        korgPowerup3.SetActive(true);
                        korgPowerup2.SetActive(false);
                korgPowerupEmpty.SetActive(false);
                korgPowerupOneThird.SetActive(false);
                korgPowerupTwoThird.SetActive(true);
                korgPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup3Value);
                        korgPowerup3.SetActive(false);
                        axelPowerupButton.SetActive(false);
                        korgPowerupMaxed.SetActive(true);
                korgPowerupEmpty.SetActive(false);
                korgPowerupOneThird.SetActive(false);
                korgPowerupTwoThird.SetActive(false);
                korgPowerupFull.SetActive(true);
                    }
                }

            }

            public void UpgradePowerupForAxel() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup1Value);
                    axelPowerupMaxed.SetActive(false); // For resets
                    axelPowerupButton.SetActive(true); // For resets
                    axelPowerup2.SetActive(true);
                    axelPowerup1.SetActive(false);
            axelPowerupEmpty.SetActive(false);
            axelPowerupOneThird.SetActive(true);
            axelPowerupTwoThird.SetActive(false);
            axelPowerupFull.SetActive(false);
        }
                else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0) {

                    PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup2Value);
                        axelPowerupMaxed.SetActive(false); // For resets
                        axelPowerupButton.SetActive(true); // For resets
                        axelPowerup3.SetActive(true);
                        axelPowerup2.SetActive(false);
                axelPowerupEmpty.SetActive(false);
                axelPowerupOneThird.SetActive(false);
                axelPowerupTwoThird.SetActive(true);
                axelPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup3Value);
                        axelPowerup3.SetActive(false);
                        axelPowerupButton.SetActive(false);
                        axelPowerupMaxed.SetActive(true);
                axelPowerupEmpty.SetActive(false);
                axelPowerupOneThird.SetActive(false);
                axelPowerupTwoThird.SetActive(false);
                axelPowerupFull.SetActive(true);
                    }
                }

            }

            public void UpgradePowerupForXavier() {
                audioS.PlayOneShot(upgradeSound);
                if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 0) {
                    PlayerPrefs.SetInt(selectedChar + "Powerup", 1);
                    PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup1Value);
                    xavierPowerupMaxed.SetActive(false); // For resets
                    xavierPowerupButton.SetActive(true); // For resets
                    xavierPowerup2.SetActive(true);
                    xavierPowerup1.SetActive(false);
            xavierPowerupEmpty.SetActive(false);
            xavierPowerupOneThird.SetActive(true);
            xavierPowerupTwoThird.SetActive(false);
            xavierPowerupFull.SetActive(false);
        }
                else if (PlayerPrefs.GetInt(selectedChar + "Powerup") > 0) {

                    PlayerPrefs.SetInt(selectedChar + "Powerup", PlayerPrefs.GetInt(selectedChar + "Powerup") + 1);

                    if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 2) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup2Value);
                        xavierPowerupMaxed.SetActive(false); // For resets
                        xavierPowerupButton.SetActive(true); // For resets
                        xavierPowerup3.SetActive(true);
                        xavierPowerup2.SetActive(false);
                xavierPowerupEmpty.SetActive(false);
                xavierPowerupOneThird.SetActive(false);
                xavierPowerupTwoThird.SetActive(true);
                xavierPowerupFull.SetActive(false);
            }
                    else if (PlayerPrefs.GetInt(selectedChar + "Powerup") == 3) {
                        PlayerPrefs.SetFloat("Coins", coinsCollectedAll - powerup3Value);
                        xavierPowerup3.SetActive(false);
                        xavierPowerupButton.SetActive(false);
                        xavierPowerupMaxed.SetActive(true);
                xavierPowerupEmpty.SetActive(false);
                xavierPowerupOneThird.SetActive(false);
                xavierPowerupTwoThird.SetActive(false);
                xavierPowerupFull.SetActive(true);
                    }
                }

            }

            public void CloseKorgPowerup() {
                audioS.PlayOneShot(pressingSound);
                korgPowerupUpgrade.SetActive(false);
                interactables.SetActive(true);
            }
            public void CloseAxelPowerup() {
                audioS.PlayOneShot(pressingSound);
                axelPowerupUpgrade.SetActive(false);
                interactables.SetActive(true);
            }

            public void CloseXavierPowerup() {
                audioS.PlayOneShot(pressingSound);
                xavierPowerupUpgrade.SetActive(false);
                interactables.SetActive(true);
            }

            public void ResetPowerupLevel() {
                PlayerPrefs.SetInt(selectedChar + "Powerup", 0);
            }





            public void SwitchCharLeft() {
                audioS.PlayOneShot(pressingSound);
                selectedIndex--;
                if (selectedIndex < 0) {
                    selectedIndex = 0;
                }
                selectedChar = allChar[selectedIndex];
                bgSprite.GetComponent<Image>().sprite = bgSprites[selectedIndex];
            }

            public void SwitchCharRight() {
                audioS.PlayOneShot(pressingSound);
                selectedIndex++;
                if (selectedIndex > allChar.Count - 1) {
                    selectedIndex = allChar.Count - 1;
                }
                selectedChar = allChar[selectedIndex];
                bgSprite.GetComponent<Image>().sprite = bgSprites[selectedIndex];
            }

            public void Back() {
                audioS.PlayOneShot(pressingSound);
                SceneManager.LoadScene("Level Select");
            }

        }
