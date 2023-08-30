using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    int selectedScene;
    [SerializeField]
    GameObject ResetGameQuestion;
    [SerializeField]
    TMP_Text homeScenario;
    [SerializeField]
    TMP_Text publicScenario;
    [SerializeField]
    TMP_Text workScenario;
    [SerializeField]
    TMP_Text question;
    [SerializeField]
    TMP_Text yesField;
    [SerializeField]
    TMP_Text noField;

    private void Start()
    {
        LocalizationController.Instance.onLanguageChange += ChangeTexts;
        ChangeTexts();
    }
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

    void ChangeTexts()
    {
        if(LocalizationController.Instance.language == LocalizationController.Language.english)
        {
            homeScenario.text = "HOME SPACE";
            publicScenario.text = "PUBLIC SPACE";
            workScenario.text = "WORK SPACE";
            question.text = "START NEW GAME?";
            yesField.text = "YES";
            noField.text = "NO";
        }
        else
        {
            homeScenario.text = "MĀJAS VIDE";
            publicScenario.text = "PUBLISKĀ VIDE";
            workScenario.text = "DARBA VIDE";
            question.text = "SĀKT NO JAUNA?";
            yesField.text = "JĀ";
            noField.text = "NĒ";
        }
    }
    private void OnDestroy()
    {
        LocalizationController.Instance.onLanguageChange -= ChangeTexts;
    }
}
