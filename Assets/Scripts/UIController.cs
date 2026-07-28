using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    // ⭐ NEW FLAG
    public static bool splashShown = false;

    [Header("Panels")]
    public GameObject splashPanel;
    public GameObject titlePanel;
    public GameObject questionPanel;
    public GameObject prosConsPanel;
    public GameObject conclusionPanel;
    public GameObject historyPanel;

    public HistoryDisplay historyDisplay;
    public MirrorFlow mirrorFlow;
    public TMPro.TMP_Text exportPathText;

    private ScreenFader fader;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        fader = FindFirstObjectByType<ScreenFader>();

        // ⭐ SPLASH ONLY ONCE
        if (!splashShown)
        {
            splashShown = true;

            splashPanel.SetActive(true);
            titlePanel.SetActive(false);
            questionPanel.SetActive(false);
            prosConsPanel.SetActive(false);
            conclusionPanel.SetActive(false);
            historyPanel.SetActive(false);

            StartCoroutine(SplashSequence());
        }
        else
        {
            // ⭐ SKIP SPLASH FOREVER AFTER FIRST TIME
            splashPanel.SetActive(false);
            titlePanel.SetActive(true);
            questionPanel.SetActive(false);
            prosConsPanel.SetActive(false);
            conclusionPanel.SetActive(false);
            historyPanel.SetActive(false);
        }
    }

    IEnumerator SplashSequence()
    {
        yield return new WaitForSeconds(4f);

        splashPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
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
        exportPathText.text = "";   // ⭐ Clear the export path text
        fader.FadeToUI(historyPanel, titlePanel);
    }

    public void RestartFromConclusion()
    {
        mirrorFlow.StartOver();
        fader.FadeToUI(conclusionPanel, titlePanel);
    }
    public void ShowHistory()
    {
        splashPanel.SetActive(false);
        titlePanel.SetActive(false);
        questionPanel.SetActive(false);
        prosConsPanel.SetActive(false);
        conclusionPanel.SetActive(false);

        historyPanel.SetActive(true);
        historyDisplay.RefreshHistory();
    }

    public void ShowTitleScreen()
    {
        historyPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}
