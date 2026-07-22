using System.IO;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    private string filePath;
    public ReflectionHistory history;

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "history.json");
        history = LoadHistory();
    }

    // FULL ENTRY VERSION (reflection + pros + cons + decision + summary)
    public void AddReflection(
        string reflectionText,
        string pros,
        string cons,
        string finalDecision,
        string summaryTable
    )
    {
        ReflectionEntry entry = new ReflectionEntry(
            reflectionText,
            pros,
            cons,
            finalDecision,
            summaryTable
        );

        history.entries.Add(entry);
        SaveHistory();
    }

    // SIMPLE VERSION (only reflection text)
    public void AddReflection(string reflectionText)
    {
        AddReflection(reflectionText, "", "", "", "");
    }

    public void SaveHistory()
    {
        string json = JsonUtility.ToJson(history, true);
        File.WriteAllText(filePath, json);
    }

    public ReflectionHistory LoadHistory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ReflectionHistory>(json);
        }

        return new ReflectionHistory();
    }

    // ⭐ UPDATED CLEAR HISTORY
    public void ClearHistory()
    {
        // Clear in-memory list
        history.entries.Clear();

        // Delete file if it exists
        if (File.Exists(filePath))
            File.Delete(filePath);

        // ⭐ Immediately recreate empty history file
        SaveHistory();
    }

    public void ExportHistory()
    {
        string exportPath = Path.Combine(Application.persistentDataPath, "Re-flexion_Export.txt");
        string json = JsonUtility.ToJson(history, true);
        File.WriteAllText(exportPath, json);
    }
}
