using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationController : MonoBehaviour
{
    public static LocalizationController Instance { get; private set; }
    [Serializable]
    public enum Language { english, latvian };
    public Language language;

    public delegate void OnLanguageChange();
    public OnLanguageChange onLanguageChange;
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
    }
    public void SelectLatvian()
    {
        language = Language.latvian;
        onLanguageChange?.Invoke();
    }
    public void SelectEnglish()
    {
        language = Language.english;
        onLanguageChange?.Invoke();
    }
}
