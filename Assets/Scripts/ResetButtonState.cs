using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetButtonState : MonoBehaviour
{
    public GameObject border;   // optional, if you use a border highlight

    void OnEnable()
    {
        // Clear Unity's selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Reset Unity's highlight state
        var btn = GetComponent<Button>();
        if (btn != null)
            btn.OnDeselect(null);

        // Reset your hover border (if used)
        if (border != null)
            border.SetActive(false);
    }
}
