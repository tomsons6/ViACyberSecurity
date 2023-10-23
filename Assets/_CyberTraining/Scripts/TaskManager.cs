using BNG;
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
    GameObject OpenedLetter;
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

    public bool justLaunched = true;

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
        SubscribeToHomeMenuBtns();
    }
    private void Update()
    {
        if(InputBridge.Instance.AButtonDown)
        {
            if(controller.CurrentTask.TaskPrompt != "")
            {
                controller.MoveTaskCanva();
            }
        }
    }
    void OnSceneChangeWrapper(Scene current, Scene Next)
    {
        if (Next.buildIndex == 0)
        {
            CurrentScenario = null;
            SubscribeToHomeMenuBtns();
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
        OpenedLetter = GameObject.Find("OpenedLetter");
        CloseLetters();
        ClosePrograms();
        ChangeTask();
    }
    void SubscribeToHomeMenuBtns()
    {
        GameObject.Find("HomeScenario").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { ButtonPress(HomeScenario); });
        GameObject.Find("PublicScenario").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { ButtonPress(publicScenario); });
        GameObject.Find("WorkScenario").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { ButtonPress(workScenario); });
    }
    void ButtonPress(Scenario scenario)
    {
        CurrentScenario = scenario;
    }
    //public void SelectScenario(Scenario selectScenario)
    //{
    //    CurrentScenario = selectScenario;
    //}
    public void EnableInteraction(Task currentTask)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            TaskActivator tmpTaskActivator;
            tmpTaskActivator = go.GetComponent<TaskActivator>();
            Debug.Log(GameObject.FindGameObjectsWithTag("Interactable").Length + go.name);
            if (tmpTaskActivator != null)
            {
                if (tmpTaskActivator.ActivationTask == currentTask)
                {
                    tmpTaskActivator.isInteracable = true;
                    Debug.Log(go.name + " enable interact");
                    return;
                }
                else
                {
                    tmpTaskActivator.isInteracable = false;
                }
            }
        }
    }
    public void ChangeTask()
    {
        TaskStarted = false;
        CloseLetters();
        ClosePrograms();

        if (CurrentScenario.tasks.All(x => x.taskCompleted))
        {
            controller.ShowResults();
        }
        foreach (Task task in CurrentScenario.tasks)
        {

            if (!task.taskCompleted)
            {
                EnableInteraction(task);
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
    public void ClosePrograms()
    {
        if (Programms != null)
        {
            foreach (Transform child in Programms.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public void CloseLetters()
    {
        if (OpenedLetter != null)
        {
           OpenedLetter.gameObject.SetActive(false);
        }
    }
    public void ClearSavedGame()
    {
        foreach (Task task in CurrentScenario.tasks)
        {
            task.taskCompleted = false;
            AllAnswers.Clear();
        }
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        if (controller != null)
        {
            controller.gameObject.GetComponentInParent<FPSManager>().Taskcanva = null;
        }
#endif
    }
    public void CurrentTaskCount()
    {
        currentTaskCount = 1;
        foreach(Task task in CurrentScenario.tasks)
        {
            if (task.taskCompleted)
            {
                currentTaskCount++;
            }
        }
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