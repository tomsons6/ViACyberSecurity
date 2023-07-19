using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPointerController : MonoBehaviour
{
    //void Update()
    //{
    //    RaycastWorldUI();
    //}

    //void RaycastWorldUI()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        PointerEventData pointerData = new PointerEventData(EventSystem.current);

    //        //pointerData.position = Input.mousePosition;
    //        pointerData.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    //        Debug.Log(pointerData.position);

    //        List<RaycastResult> results = new List<RaycastResult>();
    //        EventSystem.current.RaycastAll(pointerData, results);

    //        if (results.Count > 0)
    //        {
    //            //WorldUI is my layer name
    //            if (results[0].gameObject.layer == LayerMask.NameToLayer("WorldUI"))
    //            {
    //                string dbg = "Root Element: {0} \n GrandChild Element: {1}";
    //                Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
    //                //Debug.Log("Root Element: "+results[results.Count-1].gameObject.name);
    //                //Debug.Log("GrandChild Element: "+results[0].gameObject.name);
    //                results.Clear();
    //            }
    //        }
    //    }
    //}
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField]
    EventSystem m_EventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(GetComponent<Canvas>().worldCamera != Camera.main)
            {
                GetComponent<Canvas>().worldCamera = Camera.main;
            }

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);

            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Button>() != null)
                {
                    result.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }


}
