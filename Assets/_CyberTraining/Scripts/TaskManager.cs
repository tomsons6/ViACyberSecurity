using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    Scenario scenario;
    [SerializeField]
    public TaskController controller;
    [SerializeField]
    public List<PressedAnswer> AllAnswers = new List<PressedAnswer>();
    GameObject Programms;

    public static TaskManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        //controller.CurrentTask = scenario.tasks[0];
    }
    void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChangeWrapper;
        //ClearSavedGame();

    }
    void OnSceneChangeWrapper(Scene current, Scene Next)
    {
        if(Next.buildIndex == 0)
        {
            scenario = null;
        }
        else
        {
            OnSceneChange();
        }
    }
    public void OnSceneChange()
    {
        controller = GameObject.FindGameObjectWithTag("TaskController").GetComponent<TaskController>();
        Programms = GameObject.FindGameObjectWithTag("Programms");
        ChangeTask();
    }

    public void SelectScenario(Scenario selectScenario)
    {
        scenario = selectScenario;
    }
    public void ChangeTask()
    {
        if(Programms != null)
        {
            foreach(Transform child in Programms.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        if (scenario.tasks.All(x => x.taskCompleted))
        {
            controller.ShowResults();
        }
        foreach (Task task in scenario.tasks)
        {
            if (!task.taskCompleted)
            {
                controller.CurrentTask = task;
                if (task.TaskPrompt == "")
                {
                    Debug.Log("Start task");
                    controller.StartTask();
                }
                else
                {
                    Debug.Log("Task Promt");
                    controller.TaskPromt();
                    if (controller.CurrentTask.EmailLetter != null)
                    {
                        controller.EmailTask();
                    }
                    if (task.TaskPrompt.Contains("webcam"))
                    {
                        controller.WebCamTask();
                    }
                }
                return;
            }
        }
    }
    public void ClearSavedGame()
    {
        foreach (Task task in scenario.tasks)
        {
            task.taskCompleted = false;
        }
    }
}
[Serializable]
public class PressedAnswer
{
    [SerializeField]
    public Task currentTask;
    [SerializeField]
    public Task.AnswerGrading answer;

    public PressedAnswer(Task currentTask, Task.AnswerGrading Answer)
    {
        this.currentTask = currentTask;
        this.answer = Answer;
    }
}