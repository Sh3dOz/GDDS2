using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public Coin[] coins;
    public Score score;
    public bool isAlive;
    public bool isWin;
    public bool isBossLevel;
    public bool gotSpace;
    public static bool playerSpawned;
    public bool gotLand = true;
    [SerializeField]PlayerController player;
    [SerializeField] BossController boss;
    public Slider progressSlider;
    float landDistance;
    public Slider healthSlider;
    public Slider bossHealthBar;
    public Button landButton, spaceButton;
    public GameObject joystick;
    public Image landCooldown, spaceCooldown;
    public GameObject spaceCharge;
    public Transform startPos;
    public Transform endPos;

    public AudioSource audioS;
    public int coinCount;
    public Text coinText;
    public AudioSource coinSound;
    public CinemachineVirtualCamera vcam;

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
        coins = FindObjectsOfType<Coin>();
        score = FindObjectOfType<Score>();
        spaceship = FindObjectOfType<TransitionToSpaceship>();
        if (PlayerPrefs.GetInt("PlayGame") == 0)
        {
            PlayerPrefs.SetInt("PlayGame", 1);
            korg.SetActive(true);
            player = FindObjectOfType<PlayerController>();
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
                    vcam.Follow = korg.transform;
                    player = FindObjectOfType<PlayerController>();
                    if (PlayerPrefs.GetInt("PassiveForKorg") == 1)
                    {
                        score.scoreDeducted = 29;
                        score.newScoreDeducted = 60;
                    }
                    break;
                case "Axel":
                    axel.SetActive(true);
                    landButton.image.overrideSprite = empIcon;
                    landCooldown.overrideSprite = empCooldown;
                    spaceButton.image.overrideSprite = deflectIcon;
                    spaceCooldown.overrideSprite = deflectCooldown;
                    vcam.Follow = axel.transform;
                    player = FindObjectOfType<PlayerController>();
                    if (PlayerPrefs.GetInt("PassiveForAxel") == 1)
                    {
                        player.health = 5;
                    }
                    break;
                case "X":
                    x.SetActive(true);
                    landButton.image.sprite = null;
                    landButton.image.color = new Color(0f, 0f, 0f, 0f);
                    landCooldown.sprite = null;
                    landCooldown.color = new Color(0f, 0f, 0f, 0f);
                    spaceButton.image.sprite = metoriteIcon;
                    spaceCooldown.sprite = metoriteCooldown;
                    spaceCharge.SetActive(true);
                    vcam.Follow = x.transform;
                    player = FindObjectOfType<PlayerController>();
                    if (PlayerPrefs.GetInt("PassiveForXavier") == 1)
                    {
                        foreach (Coin coin in coins)
                        {
                            coin.coinValue = 2;
                        }
                    }
                    break;
                default:
                    korg.SetActive(true);
                    landButton.image.overrideSprite = shieldIcon;
                    landCooldown.overrideSprite = shieldCooldown;
                    spaceButton.image.overrideSprite = missileIcon;
                    spaceCooldown.overrideSprite = missileCooldown;
                    vcam.Follow = korg.transform;
                    player = FindObjectOfType<PlayerController>();
                    break;
            }
            Destroy(instructionsCanvas);
        }
        if (isBossLevel)
        {
            boss = FindObjectOfType<BossController>();
            progressSlider.gameObject.SetActive(false);
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.maxValue = boss.maxHealth;
            bossHealthBar.value = bossHealthBar.maxValue;
            healthSlider.value = player.health;
        }
        else
        {
            if (gotLand)
            {
                landDistance = Mathf.Abs(endPos.position.x - startPos.position.x);
                progressSlider.maxValue = landDistance + (gotSpace ? landDistance : 0);
            }
            else
            {
                landDistance = 0;
                progressSlider.maxValue = 1;
                GetComponent<EnemySpawn>().enabled = true;
            }
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
        if (isBossLevel)
        {
            bossHealthBar.value = boss.currentHealth;
            if (bossHealthBar.value == bossHealthBar.minValue)
            {
                Debug.Log("Boss Win");
                isWin = true;
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            }
        }
        else
        {
            if (player && player.onLand)
            {
                progressSlider.value = Mathf.Abs(player.gameObject.transform.position.x - startPos.position.x);
                if (progressSlider.value >= landDistance)
                {
                    if (gotSpace)
                    {
                        SwitchMode();
                        GetComponent<EnemySpawn>().enabled = true;
                    }
                    else
                    {
                        Debug.Log("Land Win");
                        isWin = true;
                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                        if (PlayerPrefs.GetInt("Axel") != 1) {
                            if (SceneManager.GetActiveScene().name == "Level 1 Easy" || SceneManager.GetActiveScene().name == "Level 1 Normal" || SceneManager.GetActiveScene().name == "Level 1 Hard") {
                                PlayerPrefs.SetInt("Axel", 1);
                            }
                        }
                        if (PlayerPrefs.GetInt("X") != 1) {
                            if (SceneManager.GetActiveScene().name == "level 2 easy" || SceneManager.GetActiveScene().name == "level 2 normal" || SceneManager.GetActiveScene().name == "level 2 hard") {
                                PlayerPrefs.SetInt("X", 1);
                            }
                        }
                        
                    }
                }
            }
            else if (player.isInSpace)
            {
                if (EnemySpawn.currWave > EnemySpawn.maxWave)
                {
                    Debug.Log("Space Win");
                    isWin = true;
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                }
                else
                {
                    float spaceSegment = (progressSlider.maxValue -landDistance) / EnemySpawn.maxWave;
                    progressSlider.value = landDistance + (spaceSegment * EnemySpawn.currWave - 1);
                }
            }
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

    public void SwitchMode()
    {
        if(!player.onLand)
        {
            landButton.gameObject.SetActive(false);
            spaceButton.gameObject.SetActive(true);
            joystick.SetActive(true);
            player.sr.sprite = player.spaceShip;
            vcam.Follow = null;
            player.UpdateSprite();
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (!player.isInSpace)
        {
            landButton.gameObject.SetActive(true);
            spaceButton.gameObject.SetActive(false);
            joystick.SetActive(false);
            player.sr.sprite = player.playerSprite;
            vcam.Follow = player.transform;
            player.UpdateSprite();
        }
        player.onLand = !player.onLand;
        player.isInSpace = !player.isInSpace;
    }
}