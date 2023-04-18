using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    int selectedScene;
    [SerializeField]
    GameObject ResetGameQuestion;
    public void ChooseScenario(int select)
    {
        switch (select)
        {
            case 1:
                selectedScene = 1;
                ResetGameQuestion.SetActive(true);
                break;
            case 2:
                selectedScene = 2;
                ResetGameQuestion.SetActive(true);
                break;
            case 3:
                selectedScene = 3;
                ResetGameQuestion.SetActive(true);
                break;
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(selectedScene);
    }
}
