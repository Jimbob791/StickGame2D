using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelMusicObject", menuName = "Music/LevelMusicObject")]
public class LevelMusicObject : ScriptableObject
{
    public AudioClip songClip;
    public float songBpm;
    public float beatsBeforeDrop;
    public float startOffset;
}
