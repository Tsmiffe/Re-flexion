using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    private CanvasGroup fadeGroup;

    public static bool skipNextSceneFadeIn = false;

    void Awake()
    {
        fadeGroup = GetComponent<CanvasGroup>();

        if (fadeGroup == null)
        {
            Debug.LogError("ScreenFader: No CanvasGroup found.");
            return;
        }

        if (skipNextSceneFadeIn)
        {
            fadeGroup.alpha = 0f;
        }
        else
        {
            fadeGroup.alpha = 1f;
        }
    }

    void Start()
    {
        if (skipNextSceneFadeIn)
        {
            skipNextSceneFadeIn = false;
            return;
        }

        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float t = fadeDuration;

        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        fadeGroup.alpha = 0;
    }

    public IEnumerator FadeOut()
    {
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        fadeGroup.alpha = 1;
    }

    public void FadeToUI(GameObject hideThis, GameObject showThis)
    {
        StartCoroutine(FadeUIRoutine(hideThis, showThis));
    }

    private IEnumerator FadeUIRoutine(GameObject hideThis, GameObject showThis)
    {
        // ⭐ FIX: prevents double fade by skipping one frame
        yield return null;

        fadeGroup.alpha = 1f;

        hideThis.SetActive(false);
        showThis.SetActive(true);

        yield return StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitch(sceneName));
    }

    private IEnumerator FadeAndSwitch(string sceneName)
    {
        yield return StartCoroutine(FadeOut());

        if (sceneName == "TitleScene")
            skipNextSceneFadeIn = true;

        SceneManager.LoadScene(sceneName);
    }
}
