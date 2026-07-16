using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject titleGroup;
    public GameObject questionGroup;
    public GameObject proConGroup;
    public GameObject conclusionGroup;

    private ScreenFader fader;

    void Start()
    {
        fader = FindObjectOfType<ScreenFader>();

        if (fader == null)
            Debug.LogError("UIController: No ScreenFader found in scene.");
    }

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

    // ⭐ FIX: Remove fade when restarting → prevents double fade
    public void GoBackToTitle()
    {
        SceneManager.LoadScene("TitleScene");   // instant load, no fade
    }
}
