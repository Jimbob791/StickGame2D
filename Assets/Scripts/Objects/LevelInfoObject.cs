using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelInfoObject", menuName = "Levels/LevelInfoObject")]
public class LevelInfoObject : ScriptableObject
{
    public string sceneName;
    public string levelName;
    public Sprite levelImage;
    public LevelMusicObject levelMusic;
    public int worldNum;
    public int levelNum;
}
