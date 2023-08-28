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
        // Stop old music
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
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
        musicSource.clip = levelInfo.levelMusic.songClip;
        musicSource.time = levelInfo.levelMusic.startOffset * (60 / levelInfo.levelMusic.songBpm);
        musicSource.Play();
        GameObject.Find("BeatManager").GetComponent<BeatManager>().NewBpm(levelInfo.levelMusic.songBpm);
    }

    public void ExitToMain()
    {
        // Load main menu
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }
}
