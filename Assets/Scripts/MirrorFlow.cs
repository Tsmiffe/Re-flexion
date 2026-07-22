using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MirrorFlow : MonoBehaviour
{
    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject questionPanel;
    public GameObject prosConsPanel;
    public GameObject conclusionPanel;
    public GameObject historyPanel;

    public HistoryDisplay historyDisplay;

    [Header("Input Fields")]
    public TMP_InputField userInput;
    public TMP_InputField prosInput;
    public TMP_InputField consInput;

    [Header("Text Outputs")]
    public TextMeshProUGUI prosConsHeader;
    public TextMeshProUGUI conclusionOutput;

    private string mainThought = "";
    private readonly List<string> prosList = new();
    private readonly List<string> consList = new();

    void Start()
    {
        // UIController handles panel activation
    }

    private void ClearAllFields()
    {
        userInput.text = "";
        prosInput.text = "";
        consInput.text = "";
        conclusionOutput.text = "";
    }

    // TITLE → QUESTION
    public void BeginFlow()
    {
        prosConsHeader.text =
            "<align=\"left\">" +
            "<b>Instructions:</b>\n" +
            "• Add pros or cons\n" +
            "• Paste multiple lines\n" +
            "• Press Enter to continue" +
            "</align>";
    }

    // QUESTION → PRO/CON (EnterKeyController calls this)
    public void SubmitMainThought()
    {
        // Force TMP to commit whatever is typed
        userInput.DeactivateInputField();
        userInput.ForceLabelUpdate();

        string rawInput = userInput.textComponent != null
            ? userInput.textComponent.text
            : userInput.text;

        Debug.Log("RAW INPUT = '" + rawInput + "'");

        mainThought = rawInput.Trim();
        Debug.Log("MAIN THOUGHT READ = '" + mainThought + "'");

        if (string.IsNullOrWhiteSpace(mainThought))
            return;

        prosConsHeader.text =
            "<align=\"center\"><b><size=150%>Your Thought:</size></b>\n" +
            $"<size=130%>{mainThought}</size></align>\n\n" +
            "<align=\"left\">" +
            "<b>Instructions:</b>\n" +
            "• Add pros or cons\n" +
            "• Paste multiple lines\n" +
            "• Press Enter to continue" +
            "</align>";
    }

    public void AddPro()
    {
        prosInput.DeactivateInputField();
        prosInput.ForceLabelUpdate();

        string p = prosInput.textComponent != null
            ? prosInput.textComponent.text.Trim()
            : prosInput.text.Trim();

        if (p.Length > 0)
        {
            prosList.Add(p);
            prosInput.text = "";
        }
    }

    public void AddCon()
    {
        consInput.DeactivateInputField();
        consInput.ForceLabelUpdate();

        string c = consInput.textComponent != null
            ? consInput.textComponent.text.Trim()
            : consInput.text.Trim();

        if (c.Length > 0)
        {
            consList.Add(c);
            consInput.text = "";
        }
    }

    private string BuildSoftList(List<string> items, string colorHex)
    {
        System.Text.StringBuilder sb = new();
        foreach (string item in items)
            sb.AppendLine($"<color={colorHex}>• {item}</color>");
        return sb.ToString();
    }

    // PRO/CON → CONCLUSION
    public void BuildConclusion()
    {
        Debug.Log("BUILD CONCLUSION MAIN THOUGHT = '" + mainThought + "'");

        string today = System.DateTime.Now.ToString("MMMM dd, yyyy");

        conclusionOutput.text =
            "<align=\"center\"><b><size=160%><color=#D0C7B8>Your Thought</color></size></b>\n" +
            $"<size=130%>{mainThought}</size></align>\n\n" +
            "<b><size=140%><color=#D0C7B8>Date</color></size></b>\n" +
            today + "\n\n" +
            "<b><size=140%><color=#A8D5BA>Pros</color></size></b>\n" +
            BuildSoftList(prosList, "#A8D5BA") + "\n" +
            "<b><size=140%><color=#E6AFAF>Cons</color></size></b>\n" +
            BuildSoftList(consList, "#E6AFAF") + "\n" +
            "<align=\"center\"><b><size=150%><color=#D0C7B8>What reflection do you see?</color></size></b></align>";

        string prosCombined = string.Join("\n", prosList);
        string consCombined = string.Join("\n", consList);

        historyDisplay.historyManager.AddReflection(
            mainThought,
            prosCombined,
            consCombined,
            "",
            conclusionOutput.text
        );
    }

    public void StartOver()
    {
        mainThought = "";
        prosList.Clear();
        consList.Clear();
        ClearAllFields();
    }
}
