using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public List<LevelInfoObject> levelList = new List<LevelInfoObject>();
    public GameObject previewPrefab;
    [SerializeField] private float updateSpeed;
    [SerializeField] private TextMeshProUGUI levelTitle;
    [SerializeField] private TextMeshProUGUI levelScore;
    [SerializeField] private TextMeshProUGUI levelScoreBacking;

    private List<GameObject> previews = new List<GameObject>();

    [Header("Input System")]

    private int selected = 0;
    private float lastX;

    void Start()
    {
        for (int i = 0; i < levelList.Count; i++)
        {
            GameObject newPreview = Instantiate(previewPrefab, new Vector3(i * 20, 3.8f, 0), Quaternion.identity);
            previews.Add(newPreview);
        }
        GameObject.Find("MusicSource").GetComponent<AudioSource>().clip = levelList[selected].levelMusic.songClip;
        GameObject.Find("MusicSource").GetComponent<AudioSource>().time = (levelList[selected].levelMusic.startOffset - 1) * (60 / levelList[selected].levelMusic.songBpm);
        GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        foreach (GameObject preview in previews)
        {
            int levelIndex = previews.IndexOf(preview);
            Vector3 targetScale = levelIndex == selected ? new Vector3(2, 2, 2) : new Vector3(1.6f, 1.6f, 1.6f);
            preview.GetComponent<SpriteRenderer>().color = levelIndex == selected ? new Color(1f,1f,1f,1f) : new Color(1f,1f,1f,0.3f);
            preview.transform.GetComponentsInChildren<SpriteRenderer>()[1].sprite = levelList[levelIndex].levelImage;
            preview.transform.localScale = Vector3.Lerp(preview.transform.localScale, targetScale, updateSpeed);
            preview.transform.position = Vector3.Lerp(preview.transform.position, new Vector3((levelIndex - selected) * 20, 3.8f, 0), updateSpeed);
        }

        levelTitle.text = levelList[selected].levelName + " - " + levelList[selected].levelMusic.songBpm + " BPM";
        if (PlayerPrefs.HasKey("VolumePreference"))
        {
            levelScore.text = PlayerPrefs.GetInt(levelList[selected].levelName + "Score").ToString();
            levelScoreBacking.text = PlayerPrefs.GetInt(levelList[selected].levelName + "Score").ToString();
        }
        else
        {
            levelScore.text = "0 Score";
            levelScoreBacking.text = "0 Score";
        }

        float inputx = Input.GetAxisRaw("Horizontal");

        if (inputx != 0f && lastX == 0f)
        {
            StartCoroutine(FadeBack());
            selected += inputx > 0f ? 1 : -1;
            if (selected < 0)
            {
                selected = levelList.Count - 1;
            }
            if (selected >= levelList.Count)
            {
                selected = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("LoadManager").GetComponent<LoadManager>().currentLevel = levelList[selected];
            StartCoroutine(GameObject.Find("TransitionManager").GetComponent<Transitions>().ExitScene(levelList[selected].sceneName));
        }

        lastX = inputx;
    }

    IEnumerator FadeBack()
    {
        StartCoroutine(FadeAudioSource.StartFade(GameObject.Find("MusicSource").GetComponent<AudioSource>(), 0.2f, 0f));

        yield return new WaitForSeconds(0.1f);
        GameObject.Find("MusicSource").GetComponent<AudioSource>().clip = levelList[selected].levelMusic.songClip;
        GameObject.Find("MusicSource").GetComponent<AudioSource>().time = (levelList[selected].levelMusic.startOffset - 1) * (60 / levelList[selected].levelMusic.songBpm);
        GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(FadeAudioSource.StartFade(GameObject.Find("MusicSource").GetComponent<AudioSource>(), 0.2f, 0.07f));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(GameObject.Find("TransitionManager").GetComponent<Transitions>().ExitScene("IntroScene"));
    }
}
