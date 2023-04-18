using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task",menuName = "ScriptableObjects/Task",order = 1)]
public class Task : ScriptableObject
{
    public string TaskName;
    [Tooltip  ("Only enter task promt for task that have an action")]
    public string TaskPrompt;
    public string TaskDescription;
    public string TaskAsnwer1;
    public string TaskAsnwer2;
    public string TaskAsnwer3;
    public EmailLetter EmailLetter;
    public enum AnswerGrading { notCorrect = 0, semiCorrect = 1, Correct = 2 };
    public AnswerGrading Answer1;
    public AnswerGrading Answer2;
    public AnswerGrading Answer3;
    public bool taskCompleted;
}
