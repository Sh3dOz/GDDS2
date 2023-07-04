using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour {
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;
    PlayerController thePlayer;
    public GameObject theDialougeBox;
    public GameObject TriggerDial;

    public AudioClip clickSound;
    public AudioSource audioS;

    void Start() {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    public void OpenDialouge(Message[] messages, Actor[] actors) {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        theDialougeBox.SetActive(true);
        Debug.Log("Started Convo" + messages.Length);
        Time.timeScale = 0f;
        DisplayMessage();

    }

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        thePlayer.canMove = false;
    }
    public void NextMessage() {
        activeMessage++;
        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        }
        else {
            Time.timeScale = 1f;
            Debug.Log("Convo Ended");
            isActive = false;
            theDialougeBox.SetActive(false);
            TriggerDial.SetActive(false);
            thePlayer.canMove = true;

        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isActive == true) {
            NextMessage();
            audioS.PlayOneShot(clickSound);
        }
    }
}
