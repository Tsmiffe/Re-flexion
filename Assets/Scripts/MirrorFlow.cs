using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MirrorFlow : MonoBehaviour
{
    [Header("Panels")]
    public GameObject titleScreenPanel;
    public GameObject questionPanel;
    public GameObject prosConsPanel;
    public GameObject conclusionPanel;

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
        ResetAllPanels();
        ClearAllFields();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            HandleEnterKey();

        if (Input.GetKeyDown(KeyCode.Escape))
            QuitApp();
    }

    private void HandleEnterKey()
    {
        if (titleScreenPanel.activeSelf)
            BeginFlow();
        else if (questionPanel.activeSelf)
            SubmitMainThought();
        else if (prosConsPanel.activeSelf)
            BuildConclusion();
        else if (conclusionPanel.activeSelf)
            StartOver();
    }

    private void ResetAllPanels()
    {
        titleScreenPanel.SetActive(true);
        questionPanel.SetActive(false);
        prosConsPanel.SetActive(false);
        conclusionPanel.SetActive(false);
    }

    private void ClearAllFields()
    {
        userInput.text = "";
        prosInput.text = "";
        consInput.text = "";
        conclusionOutput.text = "";
        if (prosConsHeader != null)
            prosConsHeader.text = "";
    }

    public void BeginFlow()
    {
        titleScreenPanel.SetActive(false);
        questionPanel.SetActive(true);
    }

    public void SubmitMainThought()
    {
        userInput.DeactivateInputField();
        mainThought = userInput.text.Trim();

        if (string.IsNullOrWhiteSpace(mainThought))
            return;

        prosConsHeader.text =
            "<align=\"center\"><b><size=150%>Your Thought:</size></b>\n" +
            $"<size=130%>{mainThought}</size></align>\n\n" +

            "<align=\"left\">" +
            "<b>Instructions:</b>\n" +
            "• Add pros or cons\n" +
            "• Paste multiple lines\n" +
            "• Press Build when ready" +
            "</align>";

        questionPanel.SetActive(false);
        prosConsPanel.SetActive(true);
    }

    public void AddPro()
    {
        string p = prosInput.text.Trim();
        if (p.Length > 0)
        {
            prosList.Add(p);
            prosInput.text = "";
        }
    }

    public void AddCon()
    {
        string c = consInput.text.Trim();
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

    public void BuildConclusion()
    {
        AddRemainingLines(prosInput, prosList);
        AddRemainingLines(consInput, consList);

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

        prosConsPanel.SetActive(false);
        conclusionPanel.SetActive(true);
    }

    private void AddRemainingLines(TMP_InputField input, List<string> list)
    {
        string raw = input.text.Trim();
        if (raw.Length == 0)
            return;

        string[] lines = raw.Split('\n');
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.Length > 0)
                list.Add(trimmed);
        }

        input.text = "";
    }

    public void StartOver()
    {
        mainThought = "";
        prosList.Clear();
        consList.Clear();

        ClearAllFields();
        ResetAllPanels();
    }

    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
