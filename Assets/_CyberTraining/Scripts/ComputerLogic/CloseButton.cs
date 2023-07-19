using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField]
    RectTransform ClosingPanel;

    public void ClosePanel()
    {
        if (ClosingPanel.gameObject.activeSelf)
        {
            ClosingPanel.gameObject.SetActive(false);
        }
    }
}
