using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassivePanel : MonoBehaviour
{
    [Header("Ability")]
    public bool isPassive;
    public bool isSkill;
    public bool isPowerup;
    public bool isHealth;
    public int abilityIndex;
    string abilityName;
    public TMP_Text abilityText;

    [Header("ItemsToChange")]
    public Image coinImage;
    public Text coinCost;
    public Image firstBar, secondBar, thirdBar;
    public Sprite firstEmpty, secondEmpty, thirdEmpty, firstFilled, secondFilled, thirdFilled;
    public List<float> upgradeCost;
    public List<string> upgradeText;
    public GameObject upgradeButton;
    public GameObject maxedPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (isPassive)
        {
            abilityName = "Passive";
        }
        if (isSkill)
        {
            abilityName = "Skill";
        }
        if (isPowerup)
        {
            abilityName = "Powerup";
        }
        if (isHealth)
        {
            abilityName = "Health";
        }
        UpdatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
    }

    public void UpdatePoints()
    {
        abilityIndex = PlayerPrefs.GetInt(UpgradesAgain.selectedCharacter + abilityName);
        switch (abilityIndex)
        {
            case 0:
                coinCost.text = upgradeCost[abilityIndex].ToString();
                abilityText.text = upgradeText[abilityIndex];
                firstBar.sprite = firstEmpty;
                secondBar.sprite = secondEmpty;
                thirdBar.sprite = thirdEmpty;
                break;

            case 1:
                coinCost.text = upgradeCost[abilityIndex].ToString();
                abilityText.text = upgradeText[abilityIndex];
                firstBar.sprite = firstFilled;
                secondBar.sprite = secondEmpty;
                thirdBar.sprite = thirdEmpty;
                break;
            case 2:
                coinCost.text = upgradeCost[abilityIndex].ToString();
                abilityText.text = upgradeText[abilityIndex];
                firstBar.sprite = firstFilled;
                secondBar.sprite = secondFilled;
                thirdBar.sprite = thirdEmpty;
                break;
            case 3:
                coinCost.text = upgradeCost[abilityIndex].ToString();
                abilityText.text = upgradeText[abilityIndex];
                firstBar.sprite = firstFilled;
                secondBar.sprite = secondFilled;
                thirdBar.sprite = thirdFilled;
                coinImage.gameObject.SetActive(false);
                coinCost.gameObject.SetActive(false);
                upgradeButton.SetActive(false);
                maxedPanel.SetActive(true);
                break;
        }
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        UpgradesAgain.canInput = true;
    }
}
