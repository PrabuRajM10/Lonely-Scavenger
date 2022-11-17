using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    GameObject gamePanel;
    int maxTimer = 3;

    Transform startTimer, scoreTxt;
    int highScore;
    float score = 0;

    bool canCountScore = false;
    [HideInInspector] public bool isPaused;

    [SerializeField] bool resetHighScore;
    string PlayerPrefKey_highScore = "HighScore";

    private void Awake()
    {
        Time.timeScale = 1;

        if (gameManager != null) Destroy(gameManager);
        else gameManager = this;
    }
    private void Start()
    {
        StartCoroutine(StartGame());
        if (resetHighScore)
        {
            PlayerPrefs.SetInt(PlayerPrefKey_highScore, 0);
            resetHighScore = false;
        }
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))  Pause();
        if(Input.GetKeyUp(KeyCode.Space)) Resume();
        if (Input.GetKeyUp(KeyCode.LeftShift)) StopGame();



    }
    public IEnumerator StartGame()
    {
        canCountScore = true;
        var timer = 0 ;
        var gamePanel = GameScene_UIManager.uiManager.gamePanel;
        var startTimer = GameScene_UIManager.uiManager.GetStartTimerText();
        var scoreTxt = GameScene_UIManager.uiManager.GetScoreText();
        var objTxt = GameScene_UIManager.uiManager.GetObjectiveText();

        var text = startTimer.GetComponent<Text>();
        text.text = maxTimer.ToString();
        var scale = startTimer.transform.localScale;

        startTimer.gameObject.SetActive(true);
        objTxt.gameObject.SetActive(true);

        while (timer < maxTimer)
        {
            startTimer.transform.LeanScale(Vector3.zero, 1f).setEaseInOutQuart();
            yield return new WaitForSeconds(1f);
            timer++;
            text.text = (maxTimer - timer).ToString();
            //startTimer.transform.LeanScale(scale, 0.1f).setEaseOutExpo();
            startTimer.transform.localScale = scale;
        }
        startTimer.gameObject.SetActive(false);
        objTxt.gameObject.SetActive(false);


        var scoreTxtScale = scoreTxt.localScale;
        scoreTxt.localScale = Vector3.zero;

        GameScene_UIManager.uiManager.UpdateScore(((int)score));

        scoreTxt.gameObject.SetActive(true);

        scoreTxt.transform.LeanScale(scoreTxtScale, 0.2f).setEaseOutExpo();
        Spawner.spawner.StartSpawning();
        PlayerMov.playerMov.canMove = true;
        
    }

    void TimerAnimation()
    {
    }

    void CountDownText()
    {

    }

    public void IncrementScore()
    {
        if(!isPaused)
        {
            score++;
            GameScene_UIManager.uiManager.UpdateScore(((int)score));
        }
    }

    public void StopGame()
    {
        Spawner.spawner.StopSpawn();
        GameScene_UIManager.uiManager.EnableGameOverPanel(((int)score));
        HandleHighScore();
        canCountScore = false;

    }
    public void Retry()
    {
        Time.timeScale = 1;
        Spawner.spawner.Reset();
        PlayerMov.playerMov.Reset();
        GameScene_UIManager.uiManager.Retry();
        canCountScore = true;
        PlayerMov.playerMov.canMove = false;

    }

    void HandleHighScore()
    {
        var highScore = PlayerPrefs.GetInt(PlayerPrefKey_highScore);
        if (highScore < score) PlayerPrefs.SetInt(PlayerPrefKey_highScore , (int)score );
        var highScorTxt = GameScene_UIManager.uiManager.GetHighScoreText();
        highScorTxt.GetComponent<Text>().text = "HIGH SCORE : " + PlayerPrefs.GetInt(PlayerPrefKey_highScore);
        score = 0;
    }

    public void Pause()
    {
        PlayerMov.playerMov.canMove = false;
        isPaused = true;
        GameScene_UIManager.uiManager.EnableGameOverPanel(((int)score));

    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameScene_UIManager.uiManager.DisableGameOverPanel();
    }

    public void LoadMainMenu()
    {
        SceneHandler.sceneHandler.LoadMainMenuScene();
        Time.timeScale = 1;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
