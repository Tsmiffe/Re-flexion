using UnityEngine;
using TMPro;

public class HistoryDisplay : MonoBehaviour
{
    public HistoryManager historyManager;
    public GameObject entryPrefab;
    public Transform contentParent;

    public void RefreshHistory()
    {
        // Clear old UI entries
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Load new entries
        foreach (var entry in historyManager.history.entries)
        {
            GameObject obj = Instantiate(entryPrefab, contentParent);

            TMP_Text text = obj.GetComponentInChildren<TMP_Text>();

            // ⭐ Add spacing between entries
            text.text = entry.summaryTable + "\n\n";

            // OPTIONAL: Add a separator line
            // text.text += "<color=#888888>──────────────────────────────</color>\n\n";
        }
    }

    public void ClearAllHistory()
    {
        historyManager.ClearHistory();
        RefreshHistory();
    }
}
