using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    int selectedScene;
    [SerializeField]
    TMP_Text title;
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
    [SerializeField]
    TMP_Text infoTitle;
    [SerializeField]
    TMP_Text infoDescription;
    [SerializeField]
    TMP_Text infoBtnTitle;
    [SerializeField]
    TMP_Text infoBtnCorrect;
    [SerializeField]
    TMP_Text infoBtnSemiCorrect;
    [SerializeField]
    TMP_Text infoBtnWrong;
    [SerializeField]
    GameObject InfoPanel;

    private void Start()
    {
        LocalizationController.Instance.onLanguageChange += ChangeTexts;
        ChangeTexts();
        //#if UNITY_ANDROID
        //        InfoPanel.SetActive(false);
        //#endif
    }
    public void ChooseScenario(int select)
    {
        switch (select)
        {
            case 1:
                selectedScene = 1;
                StartCoroutine(ResetProgressQuestion());
                break;
            case 2:
                selectedScene = 2;
                StartCoroutine(ResetProgressQuestion());
                break;
            case 3:
                selectedScene = 3;
                StartCoroutine(ResetProgressQuestion());
                break;
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(selectedScene);
    }
    IEnumerator ResetProgressQuestion()
    {
        while (TaskManager.Instance.CurrentScenario == null)
        {
            yield return null;
        }
        foreach (Task task in TaskManager.Instance.CurrentScenario.tasks)
        {
            if (task.taskCompleted)
            {
                ResetGameQuestion.SetActive(true);
                yield break;
            }
            else
            {
                LoadScene();
            }
        }
    }
    void ChangeTexts()
    {
        if (LocalizationController.Instance.language == LocalizationController.Language.english)
        {
            title.text = "CHOOSE SCENARIO";
            homeScenario.text = "HOME SPACE";
            publicScenario.text = "PUBLIC SPACE";
            workScenario.text = "WORK SPACE";
            question.text = "START NEW GAME?";
            yesField.text = "YES";
            noField.text = "NO";
            infoTitle.text = "Information";
#if UNITY_ANDROID
            infoDescription.text = "\t\t\t\t\tControls\r\nLeft analog - Movement\r\nRight analog - 45 degree turn\r\nA button - move task tip infront\r\nX button - open ingame menu";
#else
            infoDescription.text = "\t\t\t\t\tControls\r\nWASD - Movement\r\nQ - To quit pc screen\r\nE - To interact with objects\r\nR - To open ingame menu";
#endif
            infoBtnTitle.text = "Button color meaning";
            infoBtnCorrect.text = "Correct";
            infoBtnSemiCorrect.text = "Semi correct";
            infoBtnWrong.text = "Wrong";

        }
        else
        {
            title.text = "IZVĒLIES SCENĀRIJU";
            homeScenario.text = "MĀJAS VIDE";
            publicScenario.text = "PUBLISKĀ VIDE";
            workScenario.text = "DARBA VIDE";
            question.text = "SĀKT NO JAUNA?";
            yesField.text = "JĀ";
            noField.text = "NĒ";
            infoTitle.text = "Informācija";
#if UNITY_ANDROID
            infoDescription.text = "\t\t\t\t\tKontroles\r\nKreisais analogs - Kustība\r\nLabais analogs - 45 gradu pagrieziens\r\nA poga - nolikt priekšā uzdevuma aprakstu\r\nX poga - Lai atvērtu spēles izvēlni";
#else
  infoDescription.text = "\t\t\t\t\tKontroles\r\nWASD - Kustība\r\nQ - Lai izietu no datora režīma\r\nE - Lai pieskartos objektiem\r\nR - Lai atvērtu spēles izvēlni";
#endif

            infoBtnTitle.text = "Pogu krāsu nozīme";
            infoBtnCorrect.text = "Pareizi";
            infoBtnSemiCorrect.text = "Daļēji pareizi";
            infoBtnWrong.text = "Nepareizi";
        }
    }
    private void OnDestroy()
    {
        LocalizationController.Instance.onLanguageChange -= ChangeTexts;
    }
}
