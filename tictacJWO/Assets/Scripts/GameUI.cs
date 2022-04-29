using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("UI References :")]
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject uiInGameCanvas;
    [SerializeField] private Text uiWinnerText;
    [SerializeField] private Button uiRestartButton;
    [SerializeField] private Button uiHomeButton;
    [SerializeField] private Button uiExitButton;

    public GameObject pausepanel;

    [Header("Board Reference :")]
    [SerializeField] private Board board;

    private void Start()
    {
        uiRestartButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        uiHomeButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        uiExitButton.onClick.AddListener(ExitFunction);
        board.OnWinAction += OnWinEvent;

        uiCanvas.SetActive(false);
        uiInGameCanvas.SetActive(true);
    }

    private void OnWinEvent(XnO xno, Color color)
    {
        uiWinnerText.text = (xno == XnO.None) ? "Everybody is a Winner!!" : xno.ToString() + " is the Winner!!!!";
        uiWinnerText.color = color;

        uiCanvas.SetActive(true);
        uiInGameCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        uiRestartButton.onClick.RemoveAllListeners();
        uiHomeButton.onClick.RemoveAllListeners();
        uiExitButton.onClick.RemoveAllListeners();
        board.OnWinAction -= OnWinEvent;
    }
    void ExitFunction()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            pausepanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pausepanel.SetActive(false);
        }
    }
}
//@preeth.freqjwo
