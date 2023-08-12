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
    bool gameOver;
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
    public GameObject landCollider, spaceCollider;
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
    public bool activateShop;

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
            landButton.gameObject.SetActive(false);
            spaceButton.gameObject.SetActive(true);
            landCollider.SetActive(false);
            spaceCollider.SetActive(true);
            joystick.SetActive(true);
            if(player.sr == null)
            {
                player.sr = player.GetComponent<SpriteRenderer>();
            }
            player.sr.sprite = player.spaceShip;
            vcam.Follow = null;
            player.UpdateSprite();
            transform.localScale = new Vector3(1f, 1f, 1f);
            player.onLand = !player.onLand;
            player.isInSpace = !player.isInSpace;
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
                landButton.gameObject.SetActive(false);
                spaceButton.gameObject.SetActive(true);
                landCollider.SetActive(true);
                spaceCollider.SetActive(false);
                joystick.SetActive(true);
                player.sr.sprite = player.spaceShip;
                vcam.Follow = null;
                player.UpdateSprite();
                transform.localScale = new Vector3(1f, 1f, 1f);
                player.onLand = !player.onLand;
                player.isInSpace = !player.isInSpace;
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
                GameWin(true, 3, PlayerPrefs.GetString("Difficulty"));
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
                        GameWin(false, 1, PlayerPrefs.GetString("Difficulty"));
                    }
                }
                else if (player.isInSpace)
                {
                    if (EnemySpawn.currWave > EnemySpawn.maxWave)
                    {
                        Debug.Log("Space Win");
                        isWin = true;
                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                        GameWin(false, 2, PlayerPrefs.GetString("Difficulty"));
                    }
                    else
                    {
                        float spaceSegment = (progressSlider.maxValue - landDistance) / EnemySpawn.maxWave;
                        progressSlider.value = landDistance + (spaceSegment * EnemySpawn.currWave - 1);
                    }
                }
            }
        }
    }

    void GameWin(bool isBoss, int level, string difficulty)
    {
        if (gameOver) return;
        string character = PlayerPrefs.GetString("Character");
        PlayerPrefs.SetInt(PlayerPrefs.GetString("Character") + difficulty + level, 1);
        if (isBoss == true)
        {
            Debug.Log("Run?");
            if (level == 3)
            {
                if (PlayerPrefs.GetInt("Not a Jetpack Joyride rip-off") != 1)
                {
                    PlayerPrefs.SetInt("Not a Jetpack Joyride rip-off", 1);
                }
                if (PlayerPrefs.GetInt("Canon Event") != 1)
                {
                    if (PlayerPrefs.GetInt("KorgBoss") == 1 && PlayerPrefs.GetInt("AxelBoss") == 1 && PlayerPrefs.GetInt("XBoss") == 1)
                    {
                        PlayerPrefs.SetInt("Canon Event", 1);
                    }
                    else
                    {
                        if (PlayerPrefs.GetString("Character") == "Korg")
                        {
                            PlayerPrefs.SetInt("KorgBoss", 1);
                        }
                        else if (PlayerPrefs.GetString("Character") == "Axel")
                        {
                            PlayerPrefs.SetInt("AxelBoss", 1);
                        }
                        else if (PlayerPrefs.GetString("Character") == "X")
                        {
                            PlayerPrefs.SetInt("XBoss", 1);
                        }
                    }
                }
            }            
        }
        else
        {
            PlayerPrefs.SetFloat("Distance", PlayerPrefs.GetFloat("Distance") + progressSlider.value);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            if (level == 1)
            {
                if (PlayerPrefs.GetInt("Why are you running") != 1)
                {
                    PlayerPrefs.SetInt("Why are you running", 1);
                }
                if (PlayerPrefs.GetInt("Axel") != 1)
                {
                    PlayerPrefs.SetInt("Axel", 1);
                }
            }
            else if (level == 2)
            {
                if (PlayerPrefs.GetInt("Where's my money") != 1)
                {
                    PlayerPrefs.SetInt("Where's my money", 1);
                }
                if (PlayerPrefs.GetInt("X") != 1)
                {
                    PlayerPrefs.SetInt("X", 1);
                }
                if (PlayerPrefs.GetInt("FINISH HIM") != 1)
                {
                    if (PlayerPrefs.GetString("Character") == "X")
                    {
                        if (PlayerPrefs.GetInt("AllKilledMetor") == 1)
                        {
                            PlayerPrefs.SetInt("FINISH HIM", 1);
                        }
                    }
                }
            }
        }
        if (PlayerPrefs.GetInt("Touch some grass") != 1)
        {
            if (PlayerPrefs.GetInt("KorgAll") != 1 && PlayerPrefs.GetInt("AxelAll") != 1 && PlayerPrefs.GetInt("XAll") != 1)
            {
                if (PlayerPrefs.GetInt("KorgEasy1") == 1 && PlayerPrefs.GetInt("KorgEasy2") == 1 && PlayerPrefs.GetInt("KorgEasy3") == 1 && PlayerPrefs.GetInt("KorgNormal1") == 1 && PlayerPrefs.GetInt("KorgNormal2") == 1 && PlayerPrefs.GetInt("KorgNormal3") == 1 && PlayerPrefs.GetInt("KorgHard1") == 1 && PlayerPrefs.GetInt("KorgHard2") == 1 && PlayerPrefs.GetInt("KorgHard3") == 1)
                {
                    PlayerPrefs.SetInt("KorgAll", 1);
                }
                else if (PlayerPrefs.GetInt("AxelEasy1") == 1 && PlayerPrefs.GetInt("AxelEasy2") == 1 && PlayerPrefs.GetInt("AxelEasy3") == 1 && PlayerPrefs.GetInt("AxelNormal1") == 1 && PlayerPrefs.GetInt("AxelNormal2") == 1 && PlayerPrefs.GetInt("AxelNormal3") == 1 && PlayerPrefs.GetInt("AxelHard1") == 1 && PlayerPrefs.GetInt("AxelHard2") == 1 && PlayerPrefs.GetInt("AxelHard3") == 1)
                {
                    PlayerPrefs.SetInt("AxelAll", 1);
                }
                else if (PlayerPrefs.GetInt("XEasy1") == 1 && PlayerPrefs.GetInt("XEasy2") == 1 && PlayerPrefs.GetInt("XEasy3") == 1 && PlayerPrefs.GetInt("XNormal1") == 1 && PlayerPrefs.GetInt("XNormal2") == 1 && PlayerPrefs.GetInt("XNormal3") == 1 && PlayerPrefs.GetInt("XHard1") == 1 && PlayerPrefs.GetInt("XHard2") == 1 && PlayerPrefs.GetInt("XHard3") == 1)
                {
                    PlayerPrefs.SetInt("XAll", 1);
                }
            }
        }
        if (PlayerPrefs.GetInt("Rock Hard!") != 1)
        {
            if (PlayerPrefs.GetString("Character") == "Korg")
            {
                if (level == 1)
                {
                    if (player.health == player.maxHealth)
                    {
                        PlayerPrefs.SetInt("KorgFlawless1", 1);
                    }
                }
                else if (level == 2)
                {
                    if (player.health == player.maxHealth)
                    {
                        PlayerPrefs.SetInt("KorgFlawless2", 1);
                    }
                }
                else if (level == 3)
                {
                    if (player.health == player.maxHealth)
                    {
                        PlayerPrefs.SetInt("KorgFlawless3", 1);
                    }
                }
                if (PlayerPrefs.GetInt("KorgFlawless1") == 1 && PlayerPrefs.GetInt("KorgFlawless2") == 1 && PlayerPrefs.GetInt("KorgFlawless3") == 1)
                {
                    PlayerPrefs.SetInt("Rock Hard!", 1);
                }
            }
            if (PlayerPrefs.GetInt("Flip Flop") != 1)
            {
                if (PlayerPrefs.GetString("Character") == "Axel")
                {
                    if (level == 1)
                    {
                        if (PlayerPrefs.GetInt("AxelAbility" ) == 0)
                        {
                            PlayerPrefs.SetInt("AxelNoAbility1", 1);
                        }
                    }
                    else if (level == 2)
                    {
                        if (PlayerPrefs.GetInt("AxelAbility") == 0)
                        {
                            PlayerPrefs.SetInt("AxelNoAbility2", 1);
                        }
                    }
                    else if (level == 3)
                    {
                        if (PlayerPrefs.GetInt("AxelAbility") == 0)
                        {
                            PlayerPrefs.SetInt("AxelNoAbility3", 1);
                        }
                    }
                    if (PlayerPrefs.GetInt("AxelNoAbility1") == 1 && PlayerPrefs.GetInt("AxelNoAbility2") == 1 && PlayerPrefs.GetInt("AxelNoAbility3") == 1)
                    {
                        PlayerPrefs.SetInt("Flip Flop", 1);
                    }
                }
            }
        }
        gameOver = true;
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
        yield return new WaitUntil(() => isWin == true || player == null);
        activateShop = true;
    }

    public void SwitchMode()
    {
        if(player.onLand)
        {
            landButton.gameObject.SetActive(false);
            spaceButton.gameObject.SetActive(true);
            landCollider.SetActive(true);
            spaceCollider.SetActive(false);
            joystick.SetActive(true);
            player.sr.sprite = player.spaceShip;
            vcam.Follow = null;
            player.UpdateSprite();
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (player.isInSpace)
        {
            landButton.gameObject.SetActive(true);
            spaceButton.gameObject.SetActive(false);
            landCollider.SetActive(false);
            spaceCollider.SetActive(true);
            joystick.SetActive(false);
            player.sr.sprite = player.playerSprite;
            vcam.Follow = player.transform;
            player.UpdateSprite();
        }
        player.onLand = !player.onLand;
        player.isInSpace = !player.isInSpace;
    }

    void CheckAchievement()
    {
        if(PlayerPrefs.GetInt("Frantic Runner") != 1)
        {
            if(PlayerPrefs.GetFloat("Distance") >= 1000f)
            {
                PlayerPrefs.SetInt("Frantic Runner", 1);
            }
        }
    }
}