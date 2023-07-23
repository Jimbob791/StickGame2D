using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStyleEvent", menuName = "StyleMeter/StyleEvent")]
public class StyleEvent : ScriptableObject
{
    public string description;
    public int points;
    public Color textColour;
}
