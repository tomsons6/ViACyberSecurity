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
        SceneManager.sceneLoaded += FindControllersWrapper;
    }
    void Start()
    {
        FindControllers();
#if UNITY_STANDALONE_WIN
        StandaloneMode();
#endif
#if UNITY_ANDROID
        VRMode();
#endif
    }
    void FindControllersWrapper(Scene scene, LoadSceneMode mode)
    {
        FindControllers();
    }
    void FindControllers()
    {
        XrRig = GameObject.FindGameObjectWithTag("XrRig");
        FPSController = GameObject.FindGameObjectWithTag("FPSController");
    }
    void VRMode()
    {
        if (XrRig != null)
        {
            XrRig.SetActive(true);
            FPSController.SetActive(false);
        }
    }
    void StandaloneMode()
    {
        if (FPSController != null)
        {
            FPSController.SetActive(true);
            XrRig.SetActive(false);
        }
    }
}
