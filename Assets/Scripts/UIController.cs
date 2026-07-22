using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject titleGroup;
    public GameObject questionGroup;
    public GameObject proConGroup;
    public GameObject conclusionGroup;

    // ⭐ NEW: History panel
    public GameObject historyPanel;
    public HistoryDisplay historyDisplay;

    private ScreenFader fader;

    void Start()
    {
        fader = FindFirstObjectByType<ScreenFader>();

        if (fader == null)
            Debug.LogError("UIController: No ScreenFader found in scene.");
    }

    // -------------------------
    // EXISTING FLOW (unchanged)
    // -------------------------

    public void GoToQuestion()
    {
        if (fader != null)
            fader.FadeToUI(titleGroup, questionGroup);
    }

    public void GoToProCon()
    {
        if (fader != null)
            fader.FadeToUI(questionGroup, proConGroup);
    }

    public void GoToConclusion()
    {
        if (fader != null)
            fader.FadeToUI(proConGroup, conclusionGroup);
    }

    public void GoBackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // -------------------------
    // ⭐ NEW: HISTORY NAVIGATION
    // -------------------------

    public void ShowHistory()
    {
        titleGroup.SetActive(false);
        historyPanel.SetActive(true);

        historyDisplay.RefreshHistory();
    }

    public void ShowTitleScreen()
    {
        historyPanel.SetActive(false);
        titleGroup.SetActive(true);
    }
}
