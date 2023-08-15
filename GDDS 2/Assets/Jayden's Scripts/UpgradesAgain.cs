using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesAgain : MonoBehaviour
{
    public Text coinText;
    float coinCollected;
    public static bool canInput = true;

    [Header("SwitchCharacter")]
    public Image BG;
    public GameObject Character;
    public static string selectedCharacter;
    public List<string> allChar;
    public List<Sprite> charSprites;
    public List<Sprite> bgSprites;
    public List<RuntimeAnimatorController> charAnim;
    public KorgIdle korg;
    public AxelIdle axel;
    public XavierIdle x;
    public int index;
    public GameObject skillButton;
    public GameObject lockedPanel;

    [Header("Upgrades")]
    public GameObject passivePanel;
    public GameObject skillPanel;
    public GameObject powerupPanel;
    public GameObject healthPanel;

    [Header("SkillPreview")]
    public GameObject hoverSkill, landSkill;
    // Start is called before the first frame update
    void Start()
    {
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
        index = 0;
        selectedCharacter = allChar[index];
    }
    private void Update()
    {
        
    }

    void MaxCheck()
    {
        if (PlayerPrefs.GetInt("Baller") != 1)
        {
            if (PlayerPrefs.GetInt("KorgFullyMax") == 1 && PlayerPrefs.GetInt("AxelFullyMax") == 1 && PlayerPrefs.GetInt("XFullyMax") == 1)
            {
                PlayerPrefs.SetInt("Baller", 1);
                PlayerPrefs.SetInt("Achievement", PlayerPrefs.GetInt("Achievement") + 1);
                AchievementManager.instance.GetAchievement(5);
            }
            if (PlayerPrefs.GetInt(selectedCharacter + "FullyMax") != 1)
            {
                if (selectedCharacter == "X")
                {
                    if (PlayerPrefs.GetInt(selectedCharacter + "PassiveMax") == 1 && PlayerPrefs.GetInt(selectedCharacter + "PowerupMax") == 1)
                    {
                        PlayerPrefs.SetInt(selectedCharacter + "FullyMax", 1);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt(selectedCharacter + "PassiveMax") == 1 && PlayerPrefs.GetInt(selectedCharacter + "SkillMax") == 1 && PlayerPrefs.GetInt(selectedCharacter + "PowerupMax") == 1)
                    {
                        PlayerPrefs.SetInt(selectedCharacter + "FullyMax", 1);
                    }
                }
            }
        }
        if (PlayerPrefs.GetInt("Achievement") == 10)
        {
            PlayerPrefs.SetInt("GOD DID!", 1);
        }
    }

    public void SwitchCharacterLeft()
    {
        if(index -1 >= 0)
        {
            index--;
            BG.sprite = bgSprites[index];
            Character.GetComponent<Image>().sprite = charSprites[index];
            Character.GetComponent<Animator>().runtimeAnimatorController = charAnim[index];
            selectedCharacter = allChar[index];
            if(PlayerPrefs.GetInt(selectedCharacter) != 1)
            {
                lockedPanel.SetActive(true);
            }
            else
            {
                lockedPanel.SetActive(false);
            }
        }
        if(index == 0)
        {
            hoverSkill.SetActive(true);
            korg.enabled = true;
        }
        else
        {
            hoverSkill.SetActive(false);
            korg.enabled = false;
        }
        if(index == 1)
        {
            axel.enabled = true;
        }
        else
        {
            axel.enabled = false;
        }
        if (index == 2)
        {
            landSkill.SetActive(false);
            skillButton.SetActive(false);
            x.enabled = true;
        }
        else
        {
            landSkill.SetActive(true);
            skillButton.SetActive(true);
            x.enabled = false;
        }
        Debug.Log(selectedCharacter);
    }

    public void SwitchCharacterRight()
    {
        if (index + 1 <= allChar.Count-1)
        {
            index++;
            BG.sprite = bgSprites[index];
            Character.GetComponent<Image>().sprite = charSprites[index];
            Character.GetComponent<Animator>().runtimeAnimatorController = charAnim[index];
            selectedCharacter = allChar[index];
            if (PlayerPrefs.GetInt(selectedCharacter) != 1)
            {
                lockedPanel.SetActive(true);
            }
            else
            {
                lockedPanel.SetActive(false);
            }
        }
        if (index == 0)
        {
            hoverSkill.SetActive(true);
            korg.enabled = true;
        }
        else
        {
            hoverSkill.SetActive(false);
            korg.enabled = false;
        }
        if (index == 1)
        {
            axel.enabled = true;
        }
        else
        {
            axel.enabled = false;
        }
        if (index == 2)
        {
            landSkill.SetActive(false);
            skillButton.SetActive(false);
            x.enabled = true;
        }
        else
        {
            landSkill.SetActive(true);
            skillButton.SetActive(true);
            x.enabled = false;
        }
        Debug.Log(selectedCharacter);
    }
    
    public void OpenPassive()
    {
        if (canInput == false) return;
        passivePanel.SetActive(true);
        canInput = false;
    }

    public void OpenSkill()
    {
        if (canInput == false) return;
        skillPanel.SetActive(true);
        canInput = false;
    }

    public void OpenPowerup()
    {
        if (canInput == false) return;
        powerupPanel.SetActive(true);
        canInput = false;
    }

    public void OpenHealth()
    {
        if (canInput == false) return;
        healthPanel.SetActive(true);
        canInput = false;
    }

    public void UpgradePassive(PassivePanel ability)
    {
        if (coinCollected < ability.upgradeCost[ability.abilityIndex]) return;
        PlayerPrefs.SetInt(selectedCharacter + "Passive", PlayerPrefs.GetInt(selectedCharacter + "Passive") + 1);
        PlayerPrefs.SetFloat("Coins", coinCollected - ability.upgradeCost[ability.abilityIndex]);
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
        if(PlayerPrefs.GetInt(selectedCharacter + "Passive") == 3) 
        {
            PlayerPrefs.SetInt(selectedCharacter + "PassiveMax", 1);
        }
        MaxCheck();
    }

    public void UpgradeSkill(PassivePanel ability)
    {
        if (coinCollected < ability.upgradeCost[ability.abilityIndex]) return;
        PlayerPrefs.SetInt(selectedCharacter + "Skill", PlayerPrefs.GetInt(selectedCharacter + "Skill") + 1);
        PlayerPrefs.SetFloat("Coins", coinCollected - ability.upgradeCost[ability.abilityIndex]);
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
        if (PlayerPrefs.GetInt(selectedCharacter + "Skill") == 3)
        {
            PlayerPrefs.SetInt(selectedCharacter + "SkillMax", 1);
        }
        MaxCheck();
    }

    public void UpgradePoweup(PassivePanel ability)
    {
        if (coinCollected < ability.upgradeCost[ability.abilityIndex]) return;
        PlayerPrefs.SetInt(selectedCharacter + "Powerup", PlayerPrefs.GetInt(selectedCharacter + "Powerup") + 1);
        PlayerPrefs.SetFloat("Coins", coinCollected - ability.upgradeCost[ability.abilityIndex]);
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
        if (PlayerPrefs.GetInt(selectedCharacter + "Powerup") == 3)
        {
            PlayerPrefs.SetInt(selectedCharacter + "PowerupMax", 1);
        }
        MaxCheck();
    }
}
