using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewStyleLevel", menuName = "StyleMeter/StyleLevel")]
public class StyleLevel : ScriptableObject
{
    public float multiplier = 1f;
    public int minValue = 200;
    public int maxValue = 299;
    public Sprite icon;
    public Color lightCol;
    public Color darkCol;
    public int mana;
}