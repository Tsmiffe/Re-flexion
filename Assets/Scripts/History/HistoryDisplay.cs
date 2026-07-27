using UnityEngine;
using TMPro;
using System.Text;
using System.Text.RegularExpressions;

public class HistoryDisplay : MonoBehaviour
{
    public HistoryManager historyManager;
    public TMP_Text historyText;   // Assign this in the inspector

    public void RefreshHistory()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var entry in historyManager.history.entries)
        {
            sb.AppendLine("=====================================");
            sb.AppendLine(entry.summaryTable);
            sb.AppendLine();
        }

        historyText.text = sb.ToString();
    }

    public void ExportHistoryToText()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var entry in historyManager.history.entries)
        {
            string clean = Regex.Replace(entry.summaryTable, "<.*?>", string.Empty);

            sb.AppendLine("=====================================");
            sb.AppendLine(clean);
            sb.AppendLine("=====================================");
            sb.AppendLine();
        }

        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "ReflexionHistory.txt");
        System.IO.File.WriteAllText(filePath, sb.ToString());

        Debug.Log("History exported to: " + filePath);
    }

    public void ClearAllHistory()
    {
        historyManager.ClearHistory();
        RefreshHistory();
    }
}
