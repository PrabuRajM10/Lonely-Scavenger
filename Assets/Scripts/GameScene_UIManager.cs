using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameScene_UIManager : MonoBehaviour
{
    public static GameScene_UIManager uiManager;

    Text gameScoreTxt, gameEndScoreTxt, highScoreTxt;

    public Transform gameSceneCanvas;
    [HideInInspector] public GameObject gamePanel, gameEndPanal;

    GameObject retryBtn, mainMenuBtn, quitBtn, resumeBtn;

    CanvasGroup gameEndPanal_CanvasGrp;

    Transform startTimer, scoreTxt , objectiveTxtTrans;


    Vector3 currentScoreScale, highScoreScale, retryBtnPos, mainMenuBtnPos, quitBtnPos, resumeBtnPos;


    private void Awake()
    {
        if (uiManager != null) Destroy(uiManager);
        else uiManager = this;
        SetCanvasRef();
        gameEndPanal_CanvasGrp = gameEndPanal.GetComponent<CanvasGroup>();
        gameEndPanal_CanvasGrp.alpha = 0;

    }
    public void UpdateScore(int score)
    {
        gameScoreTxt.text = "SCORE : " + score.ToString();
    }

    void SetCanvasRef()
    {

        gamePanel = gameSceneCanvas.Find("GamePanel").gameObject;
        gameEndPanal = gameSceneCanvas.Find("GameEndPanel").gameObject;

        gameScoreTxt = gamePanel.transform.Find("ScoreText").GetComponent<Text>();
        gameEndScoreTxt = gameEndPanal.transform.Find("CurrentScoreText").GetComponent<Text>();
        highScoreTxt = gameEndPanal.transform.Find("HighScoreText").GetComponent<Text>();

        objectiveTxtTrans = gamePanel.transform.Find("ObjectiveText");
        retryBtn = gameEndPanal.transform.Find("RetryButton").gameObject;
        mainMenuBtn = gameEndPanal.transform.Find("MainMenuBtn").gameObject;
        quitBtn = gameEndPanal.transform.Find("QuitBtn").gameObject;
        resumeBtn = gameEndPanal.transform.Find("ResumeButton").gameObject;




        startTimer = gamePanel.transform.Find("StartTimer");
        scoreTxt = gamePanel.transform.Find("ScoreText");

        currentScoreScale = gameEndScoreTxt.transform.localScale;
        highScoreScale = highScoreTxt.transform.localScale;

        retryBtnPos = retryBtn.transform.localPosition;
        resumeBtnPos = resumeBtn.transform.localPosition;
        mainMenuBtnPos = mainMenuBtn.transform.localPosition;
        quitBtnPos = quitBtn.transform.localPosition;

        retryBtn.transform.localPosition = new Vector2(0, -Screen.height);
        resumeBtn.transform.localPosition = new Vector2(0, -Screen.height);
        mainMenuBtn.transform.localPosition = new Vector2(0, -Screen.height);
        quitBtn.transform.localPosition = new Vector2(0, -Screen.height);

        gameEndScoreTxt.transform.localScale = Vector2.zero;
        highScoreTxt.transform.localScale = Vector2.zero;
    }

    public void EnableGameOverPanel(int currentScore)
    {
        gamePanel.SetActive(false);
        gameEndScoreTxt.text = "SCORE : " + currentScore.ToString();
        FadeGameEndPanel(true);

    }
    public void DisableGameOverPanel()
    {
        gamePanel.SetActive(true);
        FadeGameEndPanel(false);

    }

    void FadeGameEndPanel(bool fadeIn)
    {
        GameObject btn = null;
        retryBtn.SetActive(true);
        resumeBtn.SetActive(true);
        if (GameManager.gameManager.isPaused)
        {
            btn = resumeBtn;
            retryBtn.SetActive(false);
        }
        else
        {
            btn = retryBtn;
            resumeBtn.SetActive(false);
        }

        if (fadeIn)
        {
            gameEndPanal.SetActive(true);
            gameEndPanal_CanvasGrp.LeanAlpha(1, 0.5f);
            ShowEndPanelText(gameEndScoreTxt, currentScoreScale, 0.2f);
            ShowEndPanelText(highScoreTxt, highScoreScale, 0.4f);

            ShowOptionButtons(btn.transform, 0, mainMenuBtn.transform, mainMenuBtnPos.y, quitBtn.transform, quitBtnPos.y);

        }
        else
        {
            ShowEndPanelText(gameEndScoreTxt, Vector3.zero, 0.2f);
            ShowEndPanelText(highScoreTxt, Vector3.zero, 0.4f);
            HidedOptionButtons(-Screen.height, quitBtn.transform, mainMenuBtn.transform, btn.transform);
            if (!GameManager.gameManager.isPaused) gameEndPanal_CanvasGrp.LeanAlpha(0, 0.5f).setOnComplete(DisableGameEndPanel).delay = 0.8f;
            else gameEndPanal_CanvasGrp.LeanAlpha(0, 0.5f).setOnComplete(FadeOut_Action).delay = 0.8f;
        }
    }

    void DisableGameEndPanel()
    {
        gameEndPanal.SetActive(false);
        startTimer.gameObject.SetActive(true);
        StartCoroutine(GameManager.gameManager.StartGame());
    }

    void FadeOutGameEndPanel()
    {
        gameEndPanal.SetActive(true);
    }

    void ShowEndPanelText(Text textObj, Vector3 val, float delay)
    {
        textObj.transform.LeanScale(val, 0.5f).delay = delay;
    }
    void ShowOptionButtons(Transform btn1, float btn1ScaleVal, Transform btn2, float btn2ScaleVal, Transform btn3, float btn3ScaleVal)
    {
        btn1.LeanMoveLocalY(btn1ScaleVal, 0.5f).setEaseInOutQuad().delay = 0.3f;
        btn2.LeanMoveLocalY(btn2ScaleVal, 0.5f).setEaseInOutQuad().delay = 0.5f;
        btn3.LeanMoveLocalY(btn3ScaleVal, 0.5f).setEaseInOutQuad().setOnComplete(ShowOptionButtons_Action).delay = 0.7f;
    }
    void HidedOptionButtons(float hidePos, Transform btn1, Transform btn2, Transform btn3)
    {
        btn1.LeanMoveLocalY(hidePos, 0.5f).setEaseInOutQuad().delay = 0.3f;
        btn2.LeanMoveLocalY(hidePos, 0.5f).setEaseInOutQuad().delay = 0.5f;
        btn3.LeanMoveLocalY(hidePos, 0.5f).setEaseInOutQuad().delay = 0.7f;
        //btn3.LeanMoveLocalY(hidePos, 0.5f).setEaseInOutQuad().delay = 0.7f;
    }
    void ShowOptionButtons_Action()
    {
        Time.timeScale = 0;
    }
    void FadeOut_Action()
    {
        PlayerMov.playerMov.canMove = true;
        GameManager.gameManager.isPaused = false;
        gameEndPanal.SetActive(false);
    }
    public void Retry()
    {
        FadeGameEndPanel(false);
        gamePanel.SetActive(true);
        scoreTxt.gameObject.SetActive(false);
    }

    public Transform GetStartTimerText()
    {
        return startTimer;
    }
    public Transform GetScoreText()
    {
        return scoreTxt;
    }
    public Transform GetHighScoreText()
    {
        return highScoreTxt.transform;
    }

    public Transform GetObjectiveText()
    {
        return objectiveTxtTrans;
    }
}
