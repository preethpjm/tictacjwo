using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    [Header("UI References :")]
    [SerializeField] private Button uiPlayButton;
    [SerializeField] private Button uiExitUIButton;

    private void Start()
    {
        uiPlayButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        uiExitUIButton.onClick.AddListener(Exit);
    }
    void Exit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
//@preeth.freqjwo