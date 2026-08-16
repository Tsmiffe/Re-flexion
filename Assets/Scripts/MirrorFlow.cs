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

    // Weight inputs (1–5)
    public TMP_InputField proWeightInput;
    public TMP_InputField conWeightInput;

    [Header("Text Outputs")]
    public TextMeshProUGUI prosConsHeader;
    public TextMeshProUGUI conclusionOutput;

    [Header("Character Limit Warning")]
    public TMP_Text warningText;

    private string mainThought = "";

    // Store pros/cons WITH weights
    private readonly List<WeightedItem> prosList = new();
    private readonly List<WeightedItem> consList = new();

    void Start()
    {
        if (warningText != null)
            warningText.enabled = false;

        SetupField(proWeightInput);
        SetupField(conWeightInput);
    }

    // Enforces the 1–5 rule
    void SetupField(TMP_InputField field)
    {
        field.contentType = TMP_InputField.ContentType.IntegerNumber;

        field.onValueChanged.AddListener(value =>
        {
            if (int.TryParse(value, out int num))
            {
                if (num < 1 || num > 5)
                    field.text = "";
            }
            else
            {
                field.text = "";
            }
        });
    }

    public void OnQuestionChanged()
    {
        if (warningText == null) return;
        warningText.enabled = userInput.text.Length >= userInput.characterLimit;
    }

    private void ClearAllFields()
    {
        userInput.text = "";
        prosInput.text = "";
        consInput.text = "";
        conclusionOutput.text = "";

        if (warningText != null)
            warningText.enabled = false;
    }

    public void BeginFlow()
    {
        prosConsHeader.text =
            "<align=\"left\">" +
            "<b>Instructions:</b>\n" +
            "• Add pros or cons\n" +
            "• Add weight (1–5)\n" +
            "• Press Enter to continue" +
            "</align>";
    }

    public void SubmitMainThought()
    {
        userInput.DeactivateInputField();
        userInput.ForceLabelUpdate();

        string rawInput = userInput.text;
        mainThought = rawInput.Trim();

        if (string.IsNullOrWhiteSpace(mainThought))
            return;

        prosConsHeader.text =
            "<align=\"center\"><b><size=150%>Your Thought:</size></b>\n" +
            $"<size=130%>{mainThought}</size></align>\n\n" +
            "<align=\"left\">" +
            "<b>Instructions:</b>\n" +
            "• Add pros or cons\n" +
            "• Add weight (1–5)\n" +
            "• Press Enter to continue" +
            "</align>";
    }

    public void AddPro()
    {
        prosInput.DeactivateInputField();
        prosInput.ForceLabelUpdate();

        string p = prosInput.text.Trim();

        if (p.Length > 0 && int.TryParse(proWeightInput.text, out int w))
        {
            prosList.Add(new WeightedItem(p, w));
            prosInput.text = "";
            proWeightInput.text = "";
        }
    }

    public void AddCon()
    {
        consInput.DeactivateInputField();
        consInput.ForceLabelUpdate();

        string c = consInput.text.Trim();

        if (c.Length > 0 && int.TryParse(conWeightInput.text, out int w))
        {
            consList.Add(new WeightedItem(c, w));
            consInput.text = "";
            conWeightInput.text = "";
        }
    }

    private string BuildSoftList(List<WeightedItem> items, string colorHex)
    {
        System.Text.StringBuilder sb = new();
        foreach (var item in items)
            sb.AppendLine($"<color={colorHex}>• {item.text} ({item.weight})</color>");
        return sb.ToString();
    }

    public void BuildConclusion()
    {
        string today = System.DateTime.Now.ToString("MMMM dd, yyyy");

        int proScore = 0;
        int conScore = 0;

        foreach (var p in prosList) proScore += p.weight;
        foreach (var c in consList) conScore += c.weight;

        // Color-coded verdict
        string resultText;
        string resultColor;

        if (proScore > conScore)
        {
            resultText = "Pros outweigh cons";
            resultColor = "#7ED957"; // green
        }
        else if (conScore > proScore)
        {
            resultText = "Cons outweigh pros";
            resultColor = "#FF6B6B"; // red
        }
        else
        {
            resultText = "Pros and cons are equal";
            resultColor = "#FFD966"; // yellow
        }

        conclusionOutput.text =
            "<align=\"center\"><b><size=160%><color=#D0C7B8>Your Thought</color></size></b>\n" +
            $"<size=130%>{mainThought}</size></align>\n\n" +

            "<b><size=140%><color=#D0C7B8>Date</color></size></b>\n" +
            today + "\n\n" +

            "<b><size=140%><color=#A8D5BA>Pros</color></size></b>\n" +
            BuildSoftList(prosList, "#A8D5BA") + "\n" +

            "<b><size=140%><color=#E6AFAF>Cons</color></size></b>\n" +
            BuildSoftList(consList, "#E6AFAF") + "\n" +

            $"<align=\"center\"><b><size=150%><color=#D0C7B8>Scores</color></size></b>\n" +
            $"Pros: {proScore}   |   Cons: {conScore}\n" +
            $"<color={resultColor}><b>{resultText}</b></color></align>";

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

        UIController.Instance.ShowTitleScreen();
    }
}

public class WeightedItem
{
    public string text;
    public int weight;

    public WeightedItem(string t, int w)
    {
        text = t;
        weight = w;
    }

    public override string ToString() => $"{text} ({weight})";
}
