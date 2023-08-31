using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public LevelInfoObject currentLevel;
    public BeatManager beatManager;

    private float levelTime;
    private float levelCompleteTime;
    private int beats;
    private float finalStyle;
    public bool isWin;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        EventManager.current.FullBeat += RhythmBeat;
    }

    void Update()
    {
        levelTime += Time.deltaTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioSource musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();

        if (scene.name == "SampleScene")
        {
            // Start level timer
            levelTime = 0f;
            isWin = true;

            // Set music to selected song
            beatManager.NewBpm(currentLevel.levelMusic.songBpm);
            musicSource.clip = currentLevel.levelMusic.songClip;
            musicSource.time = 0f;
            musicSource.Play();
        }
        else if (scene.name == "LevelCompleteScene")
        {
            LevelCompleteManager levelLoader = GameObject.Find("LevelCompleteManager").GetComponent<LevelCompleteManager>();
            levelLoader.levelInfo = currentLevel;
            levelLoader.timeSeconds = levelCompleteTime;
            levelLoader.numBeats = beats;
            levelLoader.levelStyle = finalStyle;
            levelLoader.win = isWin;
        }
    }

    public void LevelComplete()
    {
        levelCompleteTime = levelTime;
        finalStyle = GameObject.FindGameObjectsWithTag("StyleMeter")[0].GetComponent<StyleManager>().totalStyle;
        Time.timeScale = 0.5f;
        StartCoroutine(GameObject.Find("TransitionManager").GetComponent<Transitions>().ExitScene("LevelCompleteScene"));
    }

    private void RhythmBeat()
    {
        beats += 1;
    }

}
