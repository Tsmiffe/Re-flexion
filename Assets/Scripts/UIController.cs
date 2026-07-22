using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public GameObject titlePanel;
    public GameObject questionPanel;
    public GameObject prosConsPanel;
    public GameObject conclusionPanel;

    public GameObject historyPanel;
    public HistoryDisplay historyDisplay;

    public MirrorFlow mirrorFlow;

    private ScreenFader fader;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        fader = FindFirstObjectByType<ScreenFader>();

        titlePanel.SetActive(true);
        questionPanel.SetActive(false);
        prosConsPanel.SetActive(false);
        conclusionPanel.SetActive(false);
        historyPanel.SetActive(false);
    }

    public void GoToQuestion()
    {
        mirrorFlow.BeginFlow();
        fader.FadeToUI(titlePanel, questionPanel);
    }

    public void GoToProCon()
    {

        fader.FadeToUI(questionPanel, prosConsPanel);
    }

    public void GoToConclusion()
    {
        fader.FadeToUI(prosConsPanel, conclusionPanel);
    }

    public void GoBackToTitle()
    {
        mirrorFlow.StartOver();
        SceneManager.LoadScene("TitleScene");
    }

    public void ShowHistory()
    {
        titlePanel.SetActive(false);
        historyPanel.SetActive(true);
        historyDisplay.RefreshHistory();
    }

    public void ShowTitleScreen()
    {
        historyPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}
