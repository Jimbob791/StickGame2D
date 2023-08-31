using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelCompleteManager : MonoBehaviour
{
    public LevelInfoObject levelInfo;
    public float levelStyle;
    public int numBeats;
    public bool win;
    public float timeSeconds;

    private AudioSource musicSource;

    [SerializeField] private Sprite titleWinSprite;
    [SerializeField] private Sprite titleLostSprite;
    [SerializeField] private Sprite scoreWinSprite;
    [SerializeField] private Sprite scoreLostSprite;
    [SerializeField] private Sprite infoWinSprite;
    [SerializeField] private Sprite infoLostSprite;

    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI styleText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI rhythmText;

    void Start()
    {
        // Set Level Information
        levelInfo = GameObject.Find("LoadManager").GetComponent<LoadManager>().currentLevel;
        win = GameObject.Find("LoadManager").GetComponent<LoadManager>().isWin;
        levelStyle = GameObject.Find("LoadManager").GetComponent<LoadManager>().finalStyle;
        numBeats = GameObject.Find("LoadManager").GetComponent<LoadManager>().beats;
        timeSeconds = GameObject.Find("LoadManager").GetComponent<LoadManager>().levelCompleteTime; 

        // Stop old music
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        musicSource.volume = 0f;
        musicSource.Stop();

        // Set title
        victoryText.text = win ? levelInfo.worldNum + "-" + levelInfo.levelNum + " VICTORY" : levelInfo.worldNum + "-" + levelInfo.levelNum + " FAILURE";
        victoryText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = win ? levelInfo.worldNum + "-" + levelInfo.levelNum + " VICTORY" : levelInfo.worldNum + "-" + levelInfo.levelNum + " FAILURE";
        victoryText.gameObject.transform.parent.GetComponent<Image>().sprite = win ? titleWinSprite : titleLostSprite;

        // Calculate final score
        float timeScore = 6000 - (timeSeconds * 20);
        timeScore = timeScore < 0 ? 0 : timeScore;
        float finalScore = (levelStyle) + timeScore + (numBeats * 20);

        // Set score text
        scoreText.text = "Score: " + ((int)finalScore).ToString();
        scoreText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Score: " + ((int)finalScore).ToString();
        scoreText.gameObject.transform.parent.GetComponent<Image>().sprite = win ? scoreWinSprite : scoreLostSprite;

        // Set style text
        styleText.text = "Style: " + (levelStyle).ToString();
        styleText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Style: " + (levelStyle).ToString();
        styleText.gameObject.transform.parent.GetComponent<Image>().sprite = win ? infoWinSprite : infoLostSprite;

        // Set time text
        string timeStr = TimeSpan.FromSeconds(timeSeconds).ToString();
        timeStr = "Time: " + timeStr.Substring(3, 5);
        speedText.text = timeStr;
        speedText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timeStr;

        // Set rhythm text
        rhythmText.text = "Rhythm: " + (numBeats * 20).ToString();
        rhythmText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Rhythm: " + (numBeats * 20).ToString();

        // Start music
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 3f, 0.05f));
        musicSource.clip = levelInfo.levelMusic.songClip;
        musicSource.time = levelInfo.levelMusic.startOffset * (60 / levelInfo.levelMusic.songBpm);
        musicSource.Play();
        GameObject.Find("BeatManager").GetComponent<BeatManager>().NewBpm(levelInfo.levelMusic.songBpm);

        // Save info
        PlayerPrefs.SetInt(levelInfo.levelName + "Score", (int)finalScore);
        PlayerPrefs.SetInt(levelInfo.levelName + "Complete", win ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ExitToMain()
    {
        // Load main menu
        StartCoroutine(GameObject.Find("TransitionManager").GetComponent<Transitions>().ExitScene("IntroScene"));
    }

    public void Retry()
    {
        // Load level again
        StartCoroutine(GameObject.Find("TransitionManager").GetComponent<Transitions>().ExitScene(levelInfo.sceneName));
    }
}
