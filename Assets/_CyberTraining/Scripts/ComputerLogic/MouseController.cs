using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseController : MonoBehaviour
{
    [SerializeField]
    Image Cursor;
    Vector3 PreviousPozition;
    Vector3 MouseStartPosition;
    [SerializeField]
    float DPI = 100f;

    [SerializeField]
    float CursorTopBoarder;
    [SerializeField]
    float CursorBottomBoarder;
    [SerializeField]
    float CursorLeftBoarder;
    [SerializeField]
    float CursorRightBoarder;

    Grabbable MouseGrab;
    Rigidbody MouseRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        MouseRigidbody = GetComponent<Rigidbody>();
        MouseGrab = GetComponent<Grabbable>();
        MouseStartPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseGrab.BeingHeld)
        {
            if (MouseRigidbody.isKinematic)
            {
                MouseRigidbody.isKinematic = false;
                CursorMove();
            }
            else
            {
                CursorMove();
            }
        }
        else
        {
            MouseRigidbody.isKinematic=true;
        }

        LeftClick();
    }

    void CursorMove()
    {
        Vector3 Currentposition = transform.position - MouseStartPosition;
        Vector3 CursorCurrentPosition = new Vector3(-Currentposition.z * DPI, Currentposition.x * DPI, -0.0025f);

        if (CursorCurrentPosition.x >= CursorRightBoarder)
        {
            if (CursorCurrentPosition.x > PreviousPozition.x)
            {
                Debug.Log("Was moving right");
                return;
            }
        }
        if (CursorCurrentPosition.x <= CursorLeftBoarder)
        {
            if (CursorCurrentPosition.x < PreviousPozition.x)
            {
                Debug.Log("Was moving left");
                return;
            }
        }
        if (CursorCurrentPosition.y >= CursorTopBoarder)
        {
            if (CursorCurrentPosition.y > PreviousPozition.y)
            {
                return;
            }
        }
        if (CursorCurrentPosition.y <= CursorBottomBoarder)
        {
            if (CursorCurrentPosition.y < PreviousPozition.y)
            {
                return;
            }
        }
        Cursor.transform.localPosition = CursorCurrentPosition;
        PreviousPozition = Currentposition;
    }
    void LeftClick()
    {
        RaycastHit hit;
        //if (GetComponent<Grabbable>().BeingHeld)
        //{
            if (Input.GetKeyDown(KeyCode.R) || InputBridge.Instance.RightTriggerDown)
            {
                Debug.DrawRay(Cursor.transform.position, Vector3.right * 2f, Color.red, 2f);
                if (Physics.Raycast(Cursor.transform.position, Vector3.right * 2f, out hit))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.tag == "openbutton")
                    {
                        Debug.Log("open");
                        if (hit.transform.GetComponent<OpenButton>() != null)
                        {
                            hit.transform.GetComponent<OpenButton>().OpenPanel();
                        }
                    }
                    if (hit.transform.tag == "backbutton")
                    {
                        if (hit.transform.GetComponent<CloseButton>() != null)
                        {
                            Debug.Log("close");
                            hit.transform.GetComponent<CloseButton>().ClosePanel();
                        }
                    }
                }
            }
        //}
    }
}
