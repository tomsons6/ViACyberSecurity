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
    [SerializeField]
    GameObject ResultPanel;
    [SerializeField]
    GameObject AnswerInfo;
    [SerializeField]
    GameObject Content;

    public void StartTask()
    {
        if (TaskPromptCanva.activeInHierarchy)
        {
            TaskPromptCanva.SetActive(false);
        }
        TaskMainCanva.SetActive(true);
        TaskName.text = CurrentTask.TaskName;
        TaskDescription.text = CurrentTask.TaskDescription;
        Answer1.GetComponentInChildren<TMP_Text>().text = "Answer 1: " + CurrentTask.TaskAsnwer1;
        Answer2.GetComponentInChildren<TMP_Text>().text = "Answer 2: " + CurrentTask.TaskAsnwer2;
        Answer3.GetComponentInChildren<TMP_Text>().text = "Answer 3: " + CurrentTask.TaskAsnwer3;
    }
    public void TaskPromt()
    {
        TaskMainCanva.SetActive(false);
        TaskPromptCanva.SetActive(true);
        TaskPromtText.text = CurrentTask.TaskPrompt;
    }

    public void CloseTaskPromt()
    {
        TaskPromptCanva.SetActive(false);
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
