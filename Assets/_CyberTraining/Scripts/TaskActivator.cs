using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskActivator : MonoBehaviour
{
    [SerializeField]
    public Task ActivationTask;
    public bool isInteracable = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "grabber")
        {
            TaskActivation();
        }
    }
    public void TaskActivation()
    {
        if(ActivationTask == TaskManager.Instance.controller.CurrentTask)
        {
            TaskManager.Instance.controller.StartTask();
            this.enabled = false;
        }
    }

    public void CloseTaskPromt()
    {
        TaskManager.Instance.controller.CloseTaskPromt();
    }

}
