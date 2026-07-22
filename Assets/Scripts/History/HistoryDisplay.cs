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

            text.text =
                $"Reflection: {entry.reflectionText}\n" +
                $"Pros: {entry.pros}\n" +
                $"Cons: {entry.cons}\n" +
                $"Decision: {entry.finalDecision}\n" +
                $"Summary: {entry.summaryTable}\n" +
                $"Time: {entry.timestamp}";
        }
    }

    public void ClearAllHistory()
    {
        historyManager.ClearHistory();   // wipe file + memory
        RefreshHistory();                // wipe UI + reload empty
    }
}
