using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardKey : MonoBehaviour
{
    // public string m_Key;
    public KeyCode m_Keycode;

    public bool m_IsSpecialKey = false;

    public TextMeshProUGUI m_KeyLabel;

    // Start is called before the first frame update
    void Start()
    {
        // m_KeyLabel.text = m_Key;
        SetKeyLabel();
    }

    public void OnPress()
    {

        KeyboardOverseer.Instance.HandleKeypress(m_Keycode);
        // if (m_IsSpecialKey == false)
        // {
        //     // KeyboardOverseer.Instance.WriteToInputField (m_Key);
        //     KeyboardOverseer.Instance.WriteToInputField(m_Keycode);
        // }
        // else
        // {
        //     // KeyboardOverseer.Instance.PerformSpecialKey(m_Key);
        //     KeyboardOverseer.Instance.PerformSpecialKey(m_Keycode);
        // }
    }

    public void ToggleCaps(bool state)
    {
        if (state)
        {
            m_KeyLabel.fontStyle = FontStyles.UpperCase;
        }
        else
        {
            m_KeyLabel.fontStyle = FontStyles.LowerCase;
        }
        // SetKeyLabel();
    }

    private void SetKeyLabel()
    {
        if (m_KeyLabel != null)
        {

            switch (m_Keycode)
            {
                case KeyCode.Space:
                    m_KeyLabel.text = "____";
                    m_KeyLabel.characterSpacing = -3f;
                    break;
                case KeyCode.Backspace:
                    m_KeyLabel.text = "<----";
                    m_KeyLabel.characterSpacing = -10f;
                    break;
                case KeyCode.Delete:
                    m_KeyLabel.text = "Clear";
                    m_KeyLabel.characterSpacing = -10f;
                    break;
                case KeyCode.Return:
                    m_KeyLabel.text = "Enter";
                    m_KeyLabel.characterSpacing = -10f;
                    break;
                default:
                    m_KeyLabel.text = KeyCodeToChar.GetChar(m_Keycode);
                    m_KeyLabel.characterSpacing = 0f;
                    break;

            }
            // if (m_Keycode == KeyCode.Space)
            // {
            //     m_KeyLabel.text = "____";
            //     m_KeyLabel.characterSpacing = -3f;
            // }
            // else if (m_Keycode == KeyCode.Backspace)
            // {
            //     m_KeyLabel.text = "<----";
            //     m_KeyLabel.characterSpacing = -10f;
            // }
            // else
            // {
            //     m_KeyLabel.text = KeyCodeToChar.GetChar(m_Keycode);
            //     m_KeyLabel.characterSpacing = 0f;
            // }

            // if (m_Key.Equals("space", StringComparison.OrdinalIgnoreCase))
            // {
            //     m_KeyLabel.text = "____";
            //     m_KeyLabel.characterSpacing = -3f;
            // }
            // else if (m_Key.Equals("backspace", StringComparison.OrdinalIgnoreCase))
            // {
            //     m_KeyLabel.text = "<----";
            //     m_KeyLabel.characterSpacing = -10f;
            // }
            // else
            // {
            //     m_KeyLabel.text = m_Key;
            //     m_KeyLabel.characterSpacing = 0f;
            // }

        }
    }

    private void OnValidate()
    {
        SetKeyLabel();
    }
}