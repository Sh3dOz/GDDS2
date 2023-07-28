using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Video;

public class SkillPreview : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    UpgradesAgain upgrades;
    [Range(1.0f, 10.0f)]
    public float seconds = 1.0f;
    public UnityEvent onPressedOverSeconds;

    [Header("SkillPreview")]
    public VideoPlayer videoPlayer;
    public List<VideoClip> videoClip;

    private void Start()
    {
        upgrades = FindObjectOfType<UpgradesAgain>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(TrackTimePressed());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        videoPlayer.clip = null;
        videoPlayer.gameObject.SetActive(false);
    }

    private IEnumerator TrackTimePressed()
    {
        float time = 0;


        while (time < this.seconds)
        {
            time += Time.deltaTime;
            yield return null;
        }

        onPressedOverSeconds.Invoke();
    }

    public void SkillPre()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = videoClip[upgrades.index];
    }
}