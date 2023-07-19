using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityStandardAssets.Characters.FirstPerson;

public class FPSManager : MonoBehaviour
{
    [SerializeField]
    public Canvas Taskcanva;
    [SerializeField]
    Transform PressEText;

    Canvas PCScreen;
    Vector3 ScreenPosition;
    Quaternion ScreenRotation;
    Vector3 ScreenScale;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "TaskCanva")
            {
                Taskcanva = t.gameObject.GetComponent<Canvas>();
            }
        }
        Taskcanva.renderMode = 0;
        //Taskcanva.gameObject.SetActive(false);
        TaskManager.Instance.TaskDelegate += EnableCanvaWrapper;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            EnableTaskCanva();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            DisableTaskCanva();
        }
        CameraRayCast();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisablePCScreen();
        }

    }
    void CameraRayCast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 2f))
        {
            Transform objectHit = hit.transform;
            //Debug.Log(objectHit.name);
            if (objectHit.CompareTag("Interactable"))
            {
                PressEText.gameObject.SetActive(true);
            }
            else
            {
                PressEText.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                DetectTaskActivator(objectHit);
                EnablePCScreen(objectHit);
            }

        }

    }
    void DetectTaskActivator(Transform RayCastObj)
    {
        if (RayCastObj.GetComponent<TaskActivator>() != null)
        {

            RayCastObj.GetComponent<TaskActivator>().TaskActivation();
        }
    }
    void EnablePCScreen(Transform RayCastObj)
    {
        if(RayCastObj.GetComponentInParent<Canvas>() != null)
        {
            PCScreen = RayCastObj.GetComponentInParent<Canvas>();
            ScreenPosition = PCScreen.GetComponent<RectTransform>().position;
            ScreenRotation = PCScreen.GetComponent<RectTransform>().rotation;
            ScreenScale = PCScreen.GetComponent<RectTransform>().localScale;
            PCScreen.renderMode = RenderMode.ScreenSpaceOverlay;
            EnableControls(false);
        }
    }
    public void DisablePCScreen()
    {
        if(PCScreen != null)
        {
            EnableControls(true);
            PCScreen.renderMode = RenderMode.WorldSpace;
            PCScreen.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
            PCScreen.GetComponent<RectTransform>().position = ScreenPosition;
            PCScreen.GetComponent<RectTransform>().rotation = ScreenRotation;
            PCScreen.GetComponent<RectTransform>().localScale = ScreenScale;
            PCScreen = null;
        }
    }
    public void EnableCanvaWrapper()
    {
        if (TaskManager.Instance.TaskPromtActive)
        {
            Debug.Log("Task promt only");
            Taskcanva.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Disable controls");
            EnableTaskCanva();
        }
    }
    public void EnableTaskCanva()
    {
        Taskcanva.gameObject.SetActive(true);
        EnableControls(false);
    }

    public void DisableTaskCanva()
    {
        Taskcanva.gameObject.SetActive(false);
        EnableControls(true);
    }
    public void EnableControls(bool enable)
    {
        GetComponent<FirstPersonController>().enabled = enable;
        EnableOnlyMouse(enable);
    }
    public void EnableOnlyMouse(bool enable)
    {
        Cursor.visible = !enable;
        Cursor.lockState = enable ? CursorLockMode.Locked : CursorLockMode.None;
    }
    private void OnDestroy()
    {
        TaskManager.Instance.TaskDelegate -= EnableCanvaWrapper;
    }
}
