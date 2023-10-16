using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField]
    EmailLetter letter;
    [SerializeField]
    TMP_Text TP_Sender;
    [SerializeField]
    TMP_Text TP_Title;
    [SerializeField]
    TMP_Text TP_Header;
    [SerializeField]
    TMP_Text TP_LetterMainBody;
    GameObject PlusOneIcon;

    void Start()
    {
        GetComponent<OpenButton>().OpenMail += OpenLetterWrapper;
        GetComponent<OpenButton>().OpeningPanel = this.transform.root.FindChildRecursive("OpenedLetter").GetComponent<RectTransform>();
        GetComponent<TaskActivator>().ActivationTask = TaskManager.Instance.controller.CurrentTask;
        if (TaskManager.Instance.controller.CurrentTask.EmailLetter != null)
        {
            letter = TaskManager.Instance.controller.CurrentTask.EmailLetter;
        }
        TP_Header = this.transform.root.FindChildRecursive("Header").GetComponent<TMP_Text>();
        TP_LetterMainBody = this.transform.root.FindChildRecursive("Body").GetComponent<TMP_Text>();
        PlusOneIcon = GameObject.Find("PlusOnelogo");
        if(LocalizationController.Instance.language == LocalizationController.Language.english)
        {
            TP_Sender.text = letter.Sender;
            TP_Title.text = letter.Title;
        }
        else
        {
            TP_Sender.text = letter.SenderLV;
            TP_Title.text = letter.TitleLV;
        }

    }

    void OpenLetterWrapper()
    {
        StartCoroutine(OpenLetter());
    }

    IEnumerator OpenLetter()
    {
#if UNITY_ANDROID
        TaskManager.Instance.controller.CloseTaskPromt();
#endif
        if (PlusOneIcon != null) { PlusOneIcon.SetActive(false); }
        if(LocalizationController.Instance.language == LocalizationController.Language.english) 
        {
            TP_Header.text = letter.Sender + "   " + letter.Title;
            TP_LetterMainBody.text = letter.Body;
        }
        else
        {
            TP_Header.text = letter.SenderLV + "   " + letter.TitleLV;
            TP_LetterMainBody.text = letter.BodyLV;
        }

        yield return new WaitForSeconds(3f);
        if (GetComponent<TaskActivator>().enabled)
        {
            GetComponent<TaskActivator>().TaskActivation();
        }
        //GetComponent<TaskActivator>().enabled = false;
    }
    
}
