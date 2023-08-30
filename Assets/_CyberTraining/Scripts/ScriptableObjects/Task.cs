using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task",menuName = "ScriptableObjects/Task",order = 1)]
public class Task : ScriptableObject
{
    public string TaskName;
    public string TaskNameLV;
    [Tooltip  ("Only enter task promt for task that have an action")]
    public string TaskPrompt;
    public string TaskPromptLV;
    public string TaskDescription;
    public string TaskDescriptionLV;
    public string TaskAsnwer1;
    public string TaskAnswer1LV;
    public string TaskAsnwer2;
    public string TaskAnswer2LV;
    public string TaskAsnwer3;
    public string TaskAnswer3LV;
    public EmailLetter EmailLetter;
    public enum AnswerGrading { notCorrect = 0, semiCorrect = 1, Correct = 2 };
    public AnswerGrading Answer1;
    public AnswerGrading Answer2;
    public AnswerGrading Answer3;
    public bool taskCompleted;
}
