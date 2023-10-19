using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformSwitcher : MonoBehaviour
{
    [SerializeField]
    GameObject XrRig;
    [SerializeField]
    GameObject FPSController;
    [SerializeField]
    GameObject EventSystem;

    public static PlatformSwitcher Instance { get; private set; }
    // Start is called before the first frame update
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
        SceneManager.sceneLoaded += SceneReloadWrapper;
    }
    void SceneReloadWrapper(Scene scene, LoadSceneMode mode)
    {
        FindControllers();
#if UNITY_STANDALONE || UNITY_WEBGL
        StandaloneMode();
#endif
#if UNITY_ANDROID
        VRMode();
#endif
    }
    void FindControllers()
    {
        XrRig = GameObject.FindGameObjectWithTag("XrRig");
        FPSController = GameObject.FindGameObjectWithTag("FPSController");
    }
    void VRMode()
    {
        XrRig.SetActive(true);

        if (FPSController != null)
        {
            FPSController.SetActive(false);
        }
        Camera.main.transform.rotation.Set(0f, 0f, 0f, 0f);
    }
    void StandaloneMode()
    {
        Debug.Log("Standalone");

        FPSController.SetActive(true);
        if (XrRig != null)
        {
            XrRig.SetActive(false);
        }

        EventSystem = GameObject.Find("EventSystem");
        if (EventSystem != null)
        {
            StartCoroutine(DisableVRSystem(EventSystem));
        }
    }

    IEnumerator DisableVRSystem(GameObject ES)
    {
        while (ES.GetComponent<VRUISystem>() == null)
        {
            yield return null;
        }
        if(ES.GetComponent<VRUISystem>() != null)
        {
            ES.GetComponent<VRUISystem>().enabled = false;
            yield return null;
        }
    }
    void ClearCacheObjects()
    {
        EventSystem = null;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneReloadWrapper;
    }
}
