using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TaskController : MonoBehaviour
{
    public Task CurrentTask;
    [SerializeField]
    TMP_Text TaskName;
    [SerializeField]
    TMP_Text TaskNumber;
    [SerializeField]
    GameObject centreEyeAnchor;
    [SerializeField]
    TMP_Text TaskDescription;
    [SerializeField]
    TMP_Text TaskPromtText;
    [SerializeField]
    GameObject TaskPromptCanva;
    [SerializeField]
    GameObject TaskMainCanva;
    [SerializeField]
    Button Answer1;
    [SerializeField]
    Button Answer2;
    [SerializeField]
    Button Answer3;
    //EmailTasks
    [SerializeField]
    GameObject PlusOneEmailLogo;
    [SerializeField]
    GameObject EmailLetterPrefab;
    [SerializeField]
    GameObject LetterParent;
    //WebCamTask
    [SerializeField]
    GameObject WebCamLight;
    //UI Panels
    [SerializeField]
    GameObject ResultPanel;
    [SerializeField]
    GameObject AnswerInfo;
    [SerializeField]
    GameObject Content;

    public void StartTask()
    {
        TaskManager.Instance.TaskStarted = true;
#if PLATFORM_STANDALONE_WIN || UNITY_WEBGL

        if (CurrentTask.EmailLetter != null || CurrentTask.name.ToLower() == "safenetworking")
        {
            GetComponentInParent<FPSManager>().DisablePCScreen();
            //GetComponentInParent<FPSManager>().EnableOnlyMouse(true);
            GetComponentInParent<FPSManager>().EnableTaskCanva();
            Debug.Log("Email task ");
        }
        else
        {
            Debug.Log("Regular task");
            GetComponentInParent<FPSManager>().EnableTaskCanva();
        }


#endif
        if (TaskPromptCanva.activeInHierarchy)
        {
            TaskPromptCanva.SetActive(false);
        }
        TaskMainCanva.SetActive(true);
#if PLATFORM_ANDROID
        MoveTaskCanva();
#endif
        TaskName.text = CurrentTask.TaskName;
        TaskDescription.text = CurrentTask.TaskDescription;
        TaskManager.Instance.currentTaskCount++;
        TaskNumber.text = TaskManager.Instance.currentTaskCount.ToString() + " from " + TaskManager.Instance.TotalTaskCount().ToString();
        Answer1.GetComponentInChildren<TMP_Text>().text = "Answer 1: " + CurrentTask.TaskAsnwer1;
        Answer2.GetComponentInChildren<TMP_Text>().text = "Answer 2: " + CurrentTask.TaskAsnwer2;
        Answer3.GetComponentInChildren<TMP_Text>().text = "Answer 3: " + CurrentTask.TaskAsnwer3;
    }
    public IEnumerator TaskPromtWrapper()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        GetComponentInParent<FPSManager>().EnableControls(true);
#endif
        TaskPromt();
        yield return new WaitForSeconds(3f);
        if (!TaskManager.Instance.TaskStarted)
        {
            CloseTaskPromt();
        }
        yield return null;
    }
    public void TaskPromt()
    {
#if PLATFORM_ANDROID
        MoveTaskCanva();
#endif
        TaskMainCanva.SetActive(false);
        TaskPromptCanva.SetActive(true);
        TaskPromtText.text = CurrentTask.TaskPrompt;
    }

    public void CloseTaskPromt()
    {
        TaskPromptCanva.SetActive(false);
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        //GetComponentInParent<FPSManager>().DisableTaskCanva();
#endif
    }
    public void Answer1Presed()
    {
        CurrentTask.taskCompleted = true;
        if (!TaskManager.Instance.AllAnswers.Any(x => x.currentTask == CurrentTask))
        {
            TaskManager.Instance.AllAnswers.Add(new PressedAnswer(CurrentTask, CurrentTask.Answer1));
        }
        TaskManager.Instance.ChangeTask();
    }
    public void Answer2Pressed()
    {
        CurrentTask.taskCompleted = true;
        if (!TaskManager.Instance.AllAnswers.Any(x => x.currentTask == CurrentTask))
        {
            TaskManager.Instance.AllAnswers.Add(new PressedAnswer(CurrentTask, CurrentTask.Answer2));
        }
        TaskManager.Instance.ChangeTask();
    }
    public void Answer3Pressed()
    {
        CurrentTask.taskCompleted = true;
        if (!TaskManager.Instance.AllAnswers.Any(x => x.currentTask == CurrentTask))
        {
            TaskManager.Instance.AllAnswers.Add(new PressedAnswer(CurrentTask, CurrentTask.Answer3));
        }

        TaskManager.Instance.ChangeTask();
    }

    public void ShowResults()
    {
        if (TaskPromptCanva.activeInHierarchy)
        {
            TaskPromptCanva.SetActive(false);
        }
        if (TaskMainCanva.activeInHierarchy)
        {
            TaskMainCanva.SetActive(false);
        }
        int i = 1;
        foreach (PressedAnswer Answer in TaskManager.Instance.AllAnswers)
        {
            GameObject TempAnswer = Instantiate(AnswerInfo, Content.transform);
            TempAnswer.GetComponent<TMP_Text>().text = "Question " + i.ToString() + " - " + Answer.currentTask.TaskName + ", " + Answer.answer;
            i++;
        }
        ResultPanel.SetActive(true);

    }

    public void EmailTask()
    {
        if (CurrentTask.EmailLetter != null)
        {
            PlusOneEmailLogo.SetActive(true);
            Instantiate(EmailLetterPrefab, LetterParent.transform);

        }
    }
    public void WebCamTask()
    {
        Material Light = WebCamLight.GetComponent<Renderer>().material;
        Light.EnableKeyword("_EMISSION");
    }
    public void BackToMainMenu()
    {
        TaskManager.Instance.ClearSavedGame();
        SceneManager.LoadScene(0);
    }
#if UNITY_ANDROID
    public void MoveTaskCanva()
    {
        this.transform.position = centreEyeAnchor.transform.position + centreEyeAnchor.transform.forward * 1f;
        this.transform.LookAt(centreEyeAnchor.transform);
        this.transform.Rotate(0, 180f, 0);
    }
#endif
}
