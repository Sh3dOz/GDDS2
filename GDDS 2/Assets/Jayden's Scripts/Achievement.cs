using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievement : MonoBehaviour
{
    public string achievementName;
    public string descriptionLit;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;
    [SerializeField] Sprite litIcon;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(achievementName)== 1)
        {
            icon.sprite = litIcon;
            description.text = descriptionLit;
        }    
    }

    // Update is called once per frame
    void Update()
    { 
        
    }


}
