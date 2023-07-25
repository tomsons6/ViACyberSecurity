using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Scenario CurrentScenario;
    [SerializeField]
    public TaskController controller;
    [SerializeField]
    public List<PressedAnswer> AllAnswers = new List<PressedAnswer>();
    GameObject Programms;
    [SerializeField]
    Scenario HomeScenario;
    [SerializeField]
    Scenario publicScenario;
    [SerializeField]
    Scenario workScenario;

    public delegate void StartTask();
    public StartTask TaskDelegate;
    public static TaskManager Instance { get; private set; }

    public bool TaskPromtActive;
    public bool TaskStarted;

    public int currentTaskCount;

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
        if (Next.buildIndex == 0)
        {
            CurrentScenario = null;
            GameObject.Find("HomeScenario").GetComponent<Button>().onClick.AddListener(delegate { ButtonPress(HomeScenario); });
            GameObject.Find("PublicScenario").GetComponent<Button>().onClick.AddListener(delegate { ButtonPress(publicScenario); });
            GameObject.Find("WorkScenario").GetComponent<Button>().onClick.AddListener(delegate { ButtonPress(workScenario); });
        }
        else
        {
            //GameObject.Find("HomeScenario").GetComponent<Button>().onClick.RemoveAllListeners();
            OnSceneChange();
        }
    }
    public void OnSceneChange()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        controller = GameObject.FindGameObjectWithTag("FPSController").GetComponentInChildren<TaskController>();
#endif
#if UNITY_ANDROID
        controller = GameObject.FindGameObjectWithTag("XrRig").GetComponentInChildren<TaskController>();
#endif

        Programms = GameObject.FindGameObjectWithTag("Programms");
        ChangeTask();
    }
    void ButtonPress(Scenario scenario)
    {
        CurrentScenario = scenario;
    }
    public void SelectScenario(Scenario selectScenario)
    {
        CurrentScenario = selectScenario;
    }
    public void ChangeTask()
    {
        TaskStarted = false;
        if (Programms != null)
        {
            foreach (Transform child in Programms.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        if (CurrentScenario.tasks.All(x => x.taskCompleted))
        {
            controller.ShowResults();
        }
        foreach (Task task in CurrentScenario.tasks)
        {
            if (!task.taskCompleted)
            {
                controller.CurrentTask = task;
#if UNITY_STANDALONE || UNITY_WEBGL
                if (task.TaskPrompt == "")
                {
                    TaskPromtActive = false;
                    TaskDelegate?.Invoke();
                    Debug.Log("Start task");
                    controller.StartTask();
                }
                else
                {
                    Debug.Log("Task Promt");
                    TaskPromtActive = true;
                    TaskDelegate?.Invoke();
                    StartCoroutine(controller.TaskPromtWrapper());
                    if (controller.CurrentTask.EmailLetter != null)
                    {
                        controller.EmailTask();
                    }
                    if (task.TaskPrompt.Contains("webcam"))
                    {
                        controller.WebCamTask();
                    }
                }
#endif
#if UNITY_ANDROID
                if (task.TaskPrompt == "")
                {
                    Debug.Log("Start task");
                    controller.StartTask();
                }
                else
                {
                    Debug.Log("Task Promt");
                    //controller.TaskPromt();
                    StartCoroutine(controller.TaskPromtWrapper());
                    if (controller.CurrentTask.EmailLetter != null)
                    {
                        controller.EmailTask();
                    }
                    if (task.TaskPrompt.Contains("webcam"))
                    {
                        controller.WebCamTask();
                    }
                }
#endif
                return;
            }
        }
    }
    public void ClearSavedGame()
    {
        foreach (Task task in CurrentScenario.tasks)
        {
            task.taskCompleted = false;
            AllAnswers.Clear();
        }
        currentTaskCount = 0;
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        if (controller != null)
        {
            controller.gameObject.GetComponentInParent<FPSManager>().Taskcanva = null;
        }
#endif
    }
    public int TotalTaskCount(Scenario currentScenario)
    {
        return currentScenario.tasks.Count;
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