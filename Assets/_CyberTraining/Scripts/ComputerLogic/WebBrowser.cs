using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBrowser : MonoBehaviour
{
    //WebPopup
    [SerializeField]
    GameObject WebPopUp;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<OpenButton>().OpenBrowser += PopUpTask;
    }
    public void PopUpTask()
    {
        StartCoroutine(ActivatePopUp());
    }

    IEnumerator ActivatePopUp()
    {
        TaskManager.Instance.controller.CloseTaskPromt();
        yield return new WaitForSeconds(3f);
        WebPopUp.SetActive(true);
        GetComponent<TaskActivator>().TaskActivation();
    }
}
