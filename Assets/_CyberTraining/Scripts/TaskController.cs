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
    public GameObject TaskPromptCanva;
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
        if(LocalizationController.Instance.language == LocalizationController.Language.english)
        {
            TaskName.text = CurrentTask.TaskName;
            TaskDescription.text = CurrentTask.TaskDescription;
            TaskManager.Instance.currentTaskCount++;
            TaskNumber.text = TaskManager.Instance.currentTaskCount.ToString() + " from " + TaskManager.Instance.TotalTaskCount(TaskManager.Instance.CurrentScenario).ToString();
            Answer1.GetComponentInChildren<TMP_Text>().text = "Answer 1: " + CurrentTask.TaskAsnwer1;
            Answer2.GetComponentInChildren<TMP_Text>().text = "Answer 2: " + CurrentTask.TaskAsnwer2;
            Answer3.GetComponentInChildren<TMP_Text>().text = "Answer 3: " + CurrentTask.TaskAsnwer3;
        }
        else
        {
            TaskName.text = CurrentTask.TaskNameLV;
            TaskDescription.text = CurrentTask.TaskDescriptionLV;
            TaskManager.Instance.currentTaskCount++;
            TaskNumber.text = TaskManager.Instance.currentTaskCount.ToString() + " no " + TaskManager.Instance.TotalTaskCount(TaskManager.Instance.CurrentScenario).ToString();
            Answer1.GetComponentInChildren<TMP_Text>().text = "Atbilde 1: " + CurrentTask.TaskAnswer1LV;
            Answer2.GetComponentInChildren<TMP_Text>().text = "Atbilde 2: " + CurrentTask.TaskAnswer2LV;
            Answer3.GetComponentInChildren<TMP_Text>().text = "Atbilde 3: " + CurrentTask.TaskAnswer3LV;
        }

    }
    public IEnumerator TaskPromtWrapper()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        GetComponentInParent<FPSManager>().EnableControls(true);
#endif
        TaskPromt();
        yield return new WaitForSeconds(2f);
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
        if(LocalizationController.Instance.language == LocalizationController.Language.english)
        {
            TaskPromtText.text = CurrentTask.TaskPrompt;
        }
        else
        {
            TaskPromtText.text = CurrentTask.TaskPromptLV;
        }

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
        foreach(TMP_Text txtfield in ResultPanel.GetComponentsInChildren<TMP_Text>(true))
        {
            if(txtfield.name == "Title")
            {
                if(LocalizationController.Instance.language == LocalizationController.Language.english)
                {
                    txtfield.text = "Results";
                }
                else
                {
                    txtfield.text = "Rezultāti";
                }
            }
            if(txtfield.name == "BackButtonTxt")
            {
                if(LocalizationController.Instance.language == LocalizationController.Language.english)
                {
                    txtfield.text = "Back to menu";
                }
                else
                {
                    txtfield.text = "Atpakaļ uz sākumu";
                }
            }
        }
        int i = 1;
        foreach (PressedAnswer Answer in TaskManager.Instance.AllAnswers)
        {
            GameObject TempAnswer = Instantiate(AnswerInfo, Content.transform);
            if(LocalizationController.Instance.language == LocalizationController.Language.english)
            {
                TempAnswer.GetComponent<TMP_Text>().text = "Question " + i.ToString() + " - " + Answer.currentTask.TaskName + ", " + Answer.answer;
            }
            else
            {
                if(Answer.answer == Task.AnswerGrading.notCorrect)
                {
                    TempAnswer.GetComponent<TMP_Text>().text = "Jautājums " + i.ToString() + " - " + Answer.currentTask.TaskNameLV + ", " + "nepareizi";
                }
                if(Answer.answer == Task.AnswerGrading.semiCorrect)
                {
                    TempAnswer.GetComponent<TMP_Text>().text = "Jautājums " + i.ToString() + " - " + Answer.currentTask.TaskNameLV + ", " + "daļēji pareizi";
                }
                if (Answer.answer == Task.AnswerGrading.Correct)
                {
                    TempAnswer.GetComponent<TMP_Text>().text = "Jautājums " + i.ToString() + " - " + Answer.currentTask.TaskNameLV + ", " + "pareizi";
                }
            }
           
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
        TaskManager.Instance.CurrentScenario = null;
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
