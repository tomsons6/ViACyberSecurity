using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "ScriptableObjects/Scenario", order = 1)]
public class Scenario : ScriptableObject
{
    public List<Task> tasks = new List<Task>();
}
