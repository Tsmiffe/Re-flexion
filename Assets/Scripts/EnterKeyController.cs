using UnityEngine;

public class EnterKeyController : MonoBehaviour
{
    public MirrorFlow mirrorFlow;

    public GameObject titlePanel;
    public GameObject questionPanel;
    public GameObject prosConsPanel;
    public GameObject conclusionPanel;
    public GameObject historyPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (titlePanel.activeSelf)
            {
                UIController.Instance.GoToQuestion();
            }
            else if (questionPanel.activeSelf)
            {
                mirrorFlow.SubmitMainThought();
                UIController.Instance.GoToProCon();
            }
            else if (prosConsPanel.activeSelf)
            {
                mirrorFlow.BuildConclusion();
                UIController.Instance.GoToConclusion();
            }
            else if (conclusionPanel.activeSelf)
            {
                UIController.Instance.GoBackToTitle();
            }
            else if (historyPanel.activeSelf)
            {
                UIController.Instance.ShowTitleScreen();
            }
        }
    }
}
