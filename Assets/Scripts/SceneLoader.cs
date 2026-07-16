using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Load(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
            SceneManager.LoadScene(sceneName);
        else
            Debug.LogError($"SceneLoader: Scene '{sceneName}' not found.");
    }

    public void LoadAbout() => Load("About");
    public void LoadTitle() => Load("TitleScene");
    public void LoadSettings() => Load("Settings");
    public void LoadReflection() => Load("Reflection");
    public void LoadHistory() => Load("History");
}
