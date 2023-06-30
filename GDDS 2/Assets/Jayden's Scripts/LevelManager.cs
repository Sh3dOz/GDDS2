using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool isAlive;
    public bool isWin;
    PlayerController player;
    public Slider progressSlider;
    public Slider healthSlider;
    public Transform startPos;
    public Transform endPos;

    public int coinCount;
    public Text coinText;
    public AudioSource coinSound;

    [Header("Instrcutions")]
    public GameObject instructionsCanvas;
    public GameObject healthPanel;
    public GameObject movementPanel;
    public GameObject shieldPanel;
    public GameObject scorePanel;
    public GameObject progressPanel;
    public GameObject pausePanel;


    public bool deadedCoStarted = false;
    public TransitionToSpaceship spaceship;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spaceship = FindObjectOfType<TransitionToSpaceship>();
        progressSlider.maxValue = Mathf.Abs(endPos.position.x - startPos.position.x);
        progressSlider.value = 0;
        player = FindObjectOfType<PlayerController>();
        healthSlider.value = player.health;
        if (PlayerPrefs.GetInt("PlayGame") == 0)
        {
            PlayerPrefs.SetInt("PlayGame", 1);
            StartCoroutine(Instructions());
        }
        else
        {
            Destroy(instructionsCanvas);
        }
    }

    // Update is called once per frame
    void Update()
    {
        progressCheck();
    }

    void progressCheck()
    {
        progressSlider.value = Mathf.Abs(player.gameObject.transform.position.x - startPos.position.x);
        if (progressSlider.value == progressSlider.maxValue)
        {
            isWin = true;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
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
        Time.timeScale = 0f;
        healthPanel.SetActive(true);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        healthPanel.SetActive(false);
        scorePanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("Haro?");
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        scorePanel.SetActive(false);
        progressPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        progressPanel.SetActive(false);
        pausePanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        pausePanel.SetActive(false);
        movementPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButtonDown(0));
        movementPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}