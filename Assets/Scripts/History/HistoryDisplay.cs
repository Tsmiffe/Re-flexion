using UnityEngine;
using TMPro;
using UnityEngine.UI;    // Needed for LayoutRebuilder
using System.Text.RegularExpressions;   // Needed for stripping TMP tags

public class HistoryDisplay : MonoBehaviour
{
    public HistoryManager historyManager;
    public GameObject entryPrefab;
    public Transform contentParent;

    public void RefreshHistory()
    {
        // 1. Clear ONLY the UI objects (NOT the saved history)
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // 2. Rebuild UI entries from saved history
        foreach (var entry in historyManager.history.entries)
        {
            GameObject obj = Instantiate(entryPrefab, contentParent);

            TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
            text.text = entry.summaryTable + "\n\n";   // Add spacing
        }

        // 3. Force Unity to recalc layout so entries don't overlap
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent as RectTransform);
    }


    public void ExportHistoryToText()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var entry in historyManager.history.entries)
        {
            // ⭐ Strip TMP tags like <size>, <color>, <align>, <b>, etc.
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
        historyManager.ClearHistory();   // Clears saved data
        RefreshHistory();                // Rebuilds UI
    }
}
