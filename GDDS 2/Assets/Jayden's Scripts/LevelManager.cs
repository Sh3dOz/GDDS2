using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public bool isAlive;
    public bool isWin;
    [SerializeField]PlayerController player;
    public Slider progressSlider;
    public Slider healthSlider;
    public Button landButton, spaceButton;
    public Image landCooldown, spaceCooldown;
    public GameObject spaceCharge;
    public Transform startPos;
    public Transform endPos;

    public int coinCount;
    public Text coinText;
    public AudioSource coinSound;
    public CinemachineVirtualCamera cam;

    [Header("Instrcutions")]
    [SerializeField] GameObject instructionsCanvas;
    [SerializeField] GameObject healthPanel;
    [SerializeField] GameObject movementPanel;
    [SerializeField] GameObject shieldPanel;
    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject progressPanel;
    [SerializeField] GameObject pausePanel;

    [Header("Characters")]
    [SerializeField] GameObject korg;
    [SerializeField] Sprite shieldIcon, shieldCooldown, missileIcon, missileCooldown;
    [SerializeField] GameObject axel;
    [SerializeField] Sprite empIcon, empCooldown, deflectIcon, deflectCooldown;
    [SerializeField] GameObject x;
    [SerializeField] Sprite metoriteIcon, metoriteCooldown;

    public bool deadedCoStarted = false;
    public TransitionToSpaceship spaceship;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("PlayGame") == 0)
        {
            PlayerPrefs.SetInt("PlayGame", 1);
            korg.SetActive(true);
            StartCoroutine(Instructions());
        }
        else
        {
            switch (PlayerPrefs.GetString("Character"))
            {
                case "Korg":
                    korg.SetActive(true);
                    landButton.image.overrideSprite = shieldIcon;
                    landCooldown.overrideSprite = shieldCooldown;
                    spaceButton.image.overrideSprite = missileIcon;
                    spaceCooldown.overrideSprite = missileCooldown;
                    cam.Follow = korg.transform;
                    //ResultScreen.player = korg.GetComponent<PlayerController>();
                    break;
                case "Axel":
                    axel.SetActive(true);
                    landButton.image.overrideSprite = empIcon;
                    landCooldown.overrideSprite = empCooldown;
                    spaceButton.image.overrideSprite = deflectIcon;
                    spaceCooldown.overrideSprite = deflectCooldown;
                    cam.Follow = axel.transform;
                    //ResultScreen.player = axel.GetComponent<PlayerController>();
                    break;
                case "X":
                    x.SetActive(true);
                    landButton.image.overrideSprite = null;
                    landCooldown.overrideSprite = null;
                    spaceButton.image.overrideSprite = metoriteIcon;
                    spaceCooldown.overrideSprite = metoriteCooldown;
                    spaceCharge.SetActive(true);
                    cam.Follow = x.transform;
                    break;
                default:
                    korg.SetActive(true);
                    landButton.image.overrideSprite = shieldIcon;
                    landCooldown.overrideSprite = shieldCooldown;
                    spaceButton.image.overrideSprite = missileIcon;
                    spaceCooldown.overrideSprite = missileCooldown;
                    cam.Follow = korg.transform;
                    //ResultScreen.player = korg.GetComponent<PlayerController>();
                    break;
            }
            Destroy(instructionsCanvas);
        player = FindObjectOfType<PlayerController>();
        spaceship = FindObjectOfType<TransitionToSpaceship>();
        progressSlider.maxValue = Mathf.Abs(endPos.position.x - startPos.position.x);
        progressSlider.value = 0;
        healthSlider.value = player.health;
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>(); // So that it updates after the switch
        progressCheck();
    }

    void progressCheck()
    {
        if (player.onLand)
        {
            progressSlider.value = Mathf.Abs(player.gameObject.transform.position.x - startPos.position.x);
            if (progressSlider.value == progressSlider.maxValue)
            {
                isWin = true;
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            }
        }
        else if (player.isInSpace)
        {

        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "= " + coinCount;
        coinSound.Play();
        //PlayerPrefs.SetInt("CoinCount", coinCount);

    }

    IEnumerator Instructions()
    {
        //First time launching
        player.canMove = false;
        Time.timeScale = 0f;
        healthPanel.SetActive(true);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        healthPanel.SetActive(false);
        scorePanel.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        scorePanel.SetActive(false);
        progressPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        progressPanel.SetActive(false);
        pausePanel.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        pausePanel.SetActive(false);
        movementPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(.5f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        movementPanel.SetActive(false);
        Time.timeScale = 1f;
        player.canMove = true;
    }

}