using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UIManager : MonoBehaviour
{

    [SerializeField] Transform mainMenuCanvas;

    GameObject mainMenuPanel, playBtn, quitBtn , titleName;
    Vector3 playBtnPos, quitBtnPos;
    void Start()
    {
        SetRef();
        StartAnimation();
    }
    void SetRef()
    {
        mainMenuPanel = mainMenuCanvas.Find("MainMenuPanel").gameObject;
        titleName = mainMenuPanel.transform.Find("GameTitle").gameObject;
        playBtn = mainMenuPanel.transform.Find("PlayBtn").gameObject;
        quitBtn= mainMenuPanel.transform.Find("QuitBtn").gameObject;
        playBtnPos = playBtn.transform.localPosition;
        quitBtnPos = quitBtn.transform.localPosition;

    }

    void StartAnimation()
    {
        var grpCanvas = titleName.GetComponent<CanvasGroup>();
        grpCanvas.alpha = 0;
        playBtn.transform.localPosition = new Vector3(0, -Screen.height, 0);
        quitBtn.transform.localPosition = new Vector3(0, -Screen.height, 0);


        grpCanvas.LeanAlpha(1, 1).setEaseInCubic();
        playBtn.LeanMoveLocalY(playBtnPos.y, 1f).setEaseInBounce();
        quitBtn.LeanMoveLocalY(quitBtnPos.y, 1f).setEaseInBounce().delay = 0.2f;

    }
}
