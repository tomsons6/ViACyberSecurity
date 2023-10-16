using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Email_Letter", menuName = "ScriptableObjects/Email_letter", order = 2)]
public class EmailLetter : ScriptableObject
{
    public string Sender = "FROM: ";
    public string SenderLV = "NO: ";
    public string Title = "TITLE: ";
    public string TitleLV = "Temats: ";
    public string Body;
    public string BodyLV;
}
