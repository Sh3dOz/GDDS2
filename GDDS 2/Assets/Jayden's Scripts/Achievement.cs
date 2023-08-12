using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievement : MonoBehaviour
{
    public string achievementName;
    public string descriptionLit;
    public string nameLit;
    [SerializeField] TMP_Text description, title;
    [SerializeField] Image icon;
    [SerializeField] Sprite litIcon;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(achievementName)== 1)
        {
            icon.sprite = litIcon;
            description.text = descriptionLit;
            title.text = nameLit;
        }    
    }

    // Update is called once per frame
    void Update()
    { 
        
    }


}
