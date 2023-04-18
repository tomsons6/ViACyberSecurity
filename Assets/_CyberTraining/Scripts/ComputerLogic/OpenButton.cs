using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    public RectTransform OpeningPanel;

    public delegate void OpenEmailLetter();
    public event OpenEmailLetter OpenMail;

    public delegate void OpenInternetBrowser();
    public event OpenInternetBrowser OpenBrowser;

    public void OpenPanel()
    {
        if (!OpeningPanel.gameObject.activeSelf)
        {
            OpeningPanel.gameObject.SetActive(true);
            OpenMail?.Invoke();
            OpenBrowser?.Invoke();
        }
    }
}
