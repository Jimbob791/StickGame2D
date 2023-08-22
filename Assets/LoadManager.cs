using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public LevelInfoObject currentLevel;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioSource musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();

        if (scene.name == "SampleScene")
        {
            GameObject.Find("BeatManager").GetComponent<BeatManager>().NewBpm(currentLevel.levelMusic.songBpm);
            musicSource.clip = currentLevel.levelMusic.songClip;
            musicSource.time = 0f;
            musicSource.Play();
        }
        else if (scene.name == "LevelCompleteScene")
        {
            GameObject.Find("LevelCompleteManager").GetComponent<LevelCompleteManager>().levelInfo = currentLevel;
        }
    }
}
