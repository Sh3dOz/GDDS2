using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    public GameObject achievementPrefab;
    public List<Sprite> achievementIcon;
    public List<string> achievmentTitle;
    public List<string> achievementDescription;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void GetAchievement(int index)
    {

        GameObject achievement = Instantiate(achievementPrefab);
        achievement.transform.SetParent(this.transform);
        achievement.transform.GetChild(1).GetComponent<Image>().sprite = achievementIcon[index];
        achievement.transform.GetChild(3).GetComponent<TMP_Text>().text = achievementDescription[index];
        achievement.transform.GetChild(2).GetComponent<TMP_Text>().text = achievmentTitle[index];
        StartCoroutine(MoveAchievement(achievement));
    }

    public IEnumerator MoveAchievement(GameObject achievement)
    {
        CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

        float rate = 1.0f / 2f;
        int startAlpha = 0;
        int endAlpha = 1;

        for(int i = 0; i < 2; i++)
        {
            float progress = 0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }
            yield return new WaitForSeconds(3f);
            startAlpha = 1;
            endAlpha = 0;
        }

        Destroy(gameObject, 3f);
        
    }
}
