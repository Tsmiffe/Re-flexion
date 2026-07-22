using System.IO;
using System.Text;
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

        // ⭐ Append to bottom
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

    // ⭐ Build a formatted block for UI or export with proper spacing
    public string BuildFormattedEntry(ReflectionEntry entry)
    {
        return
            "\n\n" + // two blank lines before each entry
            "<size=110%><b><color=#D0C7B8>Your Thought</color></b></size>\n" +
            entry.reflectionText + "\n\n" +
            "<b><color=#A8D5BA>Pros</color></b>\n" +
            entry.pros + "\n\n" +
            "<b><color=#E6AFAF>Cons</color></b>\n" +
            entry.cons + "\n\n" +
            "<b><color=#D0C7B8>Decision</color></b>\n" +
            entry.finalDecision + "\n\n" +
            entry.summaryTable + "\n" +
            "<color=#888888>──────────────────────────────</color>\n\n"; // separator + spacing
    }

    // ⭐ Build full history text (for UI or export)
    public string BuildFullHistoryText()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var entry in history.entries)
        {
            sb.Append(BuildFormattedEntry(entry));
        }

        return sb.ToString();
    }

    // ⭐ Clear history safely (manual only)
    public void ClearHistory()
    {
        history.entries.Clear();

        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    // ⭐ Export formatted history (readable text file)
    public void ExportHistory()
    {
        string exportPath = Path.Combine(Application.persistentDataPath, "Re-flexion_Export.txt");

        string formatted = BuildFullHistoryText();

        File.WriteAllText(exportPath, formatted);
    }
}
