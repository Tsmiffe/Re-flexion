using UnityEngine;
using TMPro;

public class AutoSaveReflection : MonoBehaviour
{
    public TMP_InputField userInput;
    public HistoryManager historyManager;

    void Start()
    {
        userInput.onEndEdit.AddListener(OnReflectionFinished);
    }

    void OnReflectionFinished(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            historyManager.AddReflection(text); // calls the single-parameter overload
            userInput.text = ""; // optional: clear after saving
        }
    }
}
