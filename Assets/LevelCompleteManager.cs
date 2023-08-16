using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public LevelInfoObject levelInfo;
    public int levelStyle;
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
        victoryText.text = win ? levelInfo.worldNum + "-" + levelInfo.levelNum + " VICTORY" : levelInfo.worldNum + "-" + levelInfo.levelNum + " FAILURE";
        victoryText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = win ? levelInfo.worldNum + "-" + levelInfo.levelNum + " VICTORY" : levelInfo.worldNum + "-" + levelInfo.levelNum + " FAILURE";
        victoryText.gameObject.transform.parent.GetComponent<Image>().sprite = win ? titleWinSprite : titleLostSprite;

        float finalScore = (levelStyle) + (5000 - (timeSeconds * 10)) + (numBeats * 20);
        scoreText.text = "Score: " + finalScore.ToString();
        scoreText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Score: " + finalScore.ToString();
        scoreText.gameObject.transform.parent.GetComponent<Image>().sprite = win ? scoreWinSprite : scoreLostSprite;

        styleText.text = "Style: " + (levelStyle).ToString();
        styleText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Style: " + (levelStyle).ToString();
        styleText.gameObject.transform.parent.GetComponent<Image>().sprite = win ? infoWinSprite : infoLostSprite;

        speedText.text = "Speed: " + (timeSeconds).ToString();
        speedText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Speed: " + (timeSeconds).ToString();

        rhythmText.text = "Rhythm: " + (numBeats * 20).ToString();
        rhythmText.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Rhythm: " + (numBeats * 20).ToString();



        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        musicSource.clip = levelInfo.levelMusic.songClip;
        musicSource.time = levelInfo.levelMusic.startOffset * (60 / levelInfo.levelMusic.songBpm);
        musicSource.Play();

        GameObject.Find("BeatManager").GetComponent<BeatManager>().NewBpm(levelInfo.levelMusic.songBpm);
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }
}
