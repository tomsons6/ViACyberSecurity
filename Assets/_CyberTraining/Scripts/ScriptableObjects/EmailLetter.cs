using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Email_Letter", menuName = "ScriptableObjects/Email_letter", order = 2)]
public class EmailLetter : ScriptableObject
{
    public string Sender = "FROM: ";
    public string Title = "TITLE: ";
    public string Body;
}
