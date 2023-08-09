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
    public int index;
    public GameObject skillButton;

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

    public void SwitchCharacterLeft()
    {
        if(index -1 >= 0)
        {
            index--;
            BG.sprite = bgSprites[index];
            Character.GetComponent<Image>().sprite = charSprites[index];
            Character.GetComponent<Animator>().runtimeAnimatorController = charAnim[index];
            selectedCharacter = allChar[index];
        }
        if(index != 0)
        {
            hoverSkill.SetActive(false);
        }
        else
        {
            hoverSkill.SetActive(true);
        }
        if (index == 2)
        {
            landSkill.SetActive(false);
            skillButton.SetActive(false);
        }
        else
        {
            landSkill.SetActive(true);
            skillButton.SetActive(true);
        }
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
        }
        if (index == 0)
        {
            hoverSkill.SetActive(true);
        }
        else
        {
            hoverSkill.SetActive(false);
        }
        if (index == 2)
        {
            landSkill.SetActive(false);
            skillButton.SetActive(false);
        }
        else
        {
            landSkill.SetActive(true);
            skillButton.SetActive(true);
        }
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
    }

    public void UpgradeSkill(PassivePanel ability)
    {
        if (coinCollected < ability.upgradeCost[ability.abilityIndex]) return;
        PlayerPrefs.SetInt(selectedCharacter + "Skill", PlayerPrefs.GetInt(selectedCharacter + "Skill") + 1);
        PlayerPrefs.SetFloat("Coins", coinCollected - ability.upgradeCost[ability.abilityIndex]);
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
    }

    public void UpgradePoweup(PassivePanel ability)
    {
        if (coinCollected < ability.upgradeCost[ability.abilityIndex]) return;
        PlayerPrefs.SetInt(selectedCharacter + "Powerup", PlayerPrefs.GetInt(selectedCharacter + "Powerup") + 1);
        PlayerPrefs.SetFloat("Coins", coinCollected - ability.upgradeCost[ability.abilityIndex]);
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
    }

    public void UpgradeHealth(PassivePanel ability)
    {
        if (coinCollected < ability.upgradeCost[ability.abilityIndex]) return;
        PlayerPrefs.SetInt(selectedCharacter + "Health", PlayerPrefs.GetInt(selectedCharacter + "Health") + 1);
        PlayerPrefs.SetFloat("Coins", coinCollected - ability.upgradeCost[ability.abilityIndex]);
        coinCollected = PlayerPrefs.GetFloat("Coins");
        coinText.text = "=" + " " + coinCollected;
    }
}
