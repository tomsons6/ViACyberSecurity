using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class KeyboardOverseer : MonoBehaviour
{
    [Header("General Settings")]
    public bool m_KeyboardActive = false;
    public TMP_InputField m_CurrentInputField;
    public bool m_ShiftEnabled = false;

    private List<KeyboardKey> m_AllKeys = new List<KeyboardKey>();
    private CanvasGroup m_CanvasGroup;

    private static KeyboardOverseer _instance;

    public static KeyboardOverseer Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();

        GetAllKeys();
        ToggleCaps(true);

        m_CanvasGroup.alpha = 0f;
        m_CanvasGroup.interactable = false;
        m_CanvasGroup.blocksRaycasts = false;
    }

    private void GetAllKeys()
    {
        m_AllKeys.AddRange(gameObject.GetComponentsInChildren<KeyboardKey>());
    }

    private void ToggleCaps(bool state)
    {
        if (m_AllKeys != null || m_AllKeys.Count > 0)
        {
            foreach (var key in m_AllKeys)
            {
                if (key.m_IsSpecialKey == false)
                {
                    key.ToggleCaps(state);
                }
            }
        }
    }

    public void ActivateKeyboard(TMP_InputField inputField)
    {


          m_KeyboardActive = true;
          m_CanvasGroup.interactable = true;
          m_CanvasGroup.blocksRaycasts = true;

          m_CurrentInputField = inputField;
  
    }

    public void DeactivateKeyboard()
    {

          m_KeyboardActive = false;
          m_CanvasGroup.interactable = false;
          m_CanvasGroup.blocksRaycasts = false;

           EventSystem.current.SetSelectedGameObject(null);
           m_CurrentInputField = null;

    }

    // The commented code below is for a input system where the player can set the inputfield caret position and write or delete from that point. For some reason, that doesn't work on Quest, so for now that is replaced with a very simple string adding solution.
    // DO NOT DELETE THOSE COMMENTS!

    public void HandleKeypress(KeyCode value)
    {
        if (m_CurrentInputField == null)
        {
            return;
        }

        string currentText = m_CurrentInputField.text;

        string formattedKey = KeyCodeToChar.GetChar(value);

        // Find KeyCode Sequence
        if (value == KeyCode.Backspace)
        {
            currentText = currentText.Remove(currentText.Length - 1, 1);
            // m_CurrentInputField.text = m_CurrentInputField.text.Remove(m_CurrentInputField.text.Length - 1, 1);
        }
        else if (value == KeyCode.Return)
        {
            PerformEnterKey();
        }
        else if (value == KeyCode.Delete)
        {
            // m_CurrentInputField.text = "";
            currentText = "";
            // PerformClear();
        }
        else
        {
            currentText += formattedKey;

        }
        m_CurrentInputField.text = currentText;
    }

    public void MoveCaretUp()
    {
        StartCoroutine(IncreaseInputFieldCareteRoutine());
    }

    public void MoveCaretBack()
    {
        StartCoroutine(DecreaseInputFieldCareteRoutine());
    }

    IEnumerator IncreaseInputFieldCareteRoutine()
    {
        yield return new WaitForEndOfFrame();
        m_CurrentInputField.caretPosition = m_CurrentInputField.caretPosition + 1;
        m_CurrentInputField.ForceLabelUpdate();
    }

    IEnumerator DecreaseInputFieldCareteRoutine()
    {
        yield return new WaitForEndOfFrame();
        m_CurrentInputField.caretPosition = m_CurrentInputField.caretPosition - 1;
        m_CurrentInputField.ForceLabelUpdate();
    }

    public void PerformEnterKey()
    {
        DeactivateKeyboard();
    }

    public void PerformCancel()
    {
        PerformClear();
        DeactivateKeyboard();
    }

    public void PerformClear()
    {
        m_CurrentInputField.text = "";
    }
}