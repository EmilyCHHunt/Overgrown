using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "ScriptableObjects/Achievement", order = 1)]
public class Achievement : ScriptableObject
{
    public int id;
    public string type;
    public string achName;
    public string description;
    public Sprite image;
    public int desiredNum;
    public bool complete;
}