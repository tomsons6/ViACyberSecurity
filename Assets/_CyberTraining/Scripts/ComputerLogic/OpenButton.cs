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
            if (this.gameObject.name.Contains("Letter"))
            { 
                GetComponent<Image>().color = new Color32(41, 32, 32, 100);
            }
            OpeningPanel.gameObject.SetActive(true);
            OpenMail?.Invoke();
            OpenBrowser?.Invoke();
        }
    }
}
