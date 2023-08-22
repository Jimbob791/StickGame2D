using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private PlayerControls playerControls;
    private InputAction navigate;
    private InputAction submit;

    private int selected = 0;
    private float lastX;

    void OnEnable()
    {
        navigate = playerControls.UI.Navigate;
        submit = playerControls.UI.Submit;
        navigate.Enable();
        submit.Enable();
    }

    void OnDisable()
    {
        navigate.Disable();
        submit.Disable();
    }

    void Awake()
    {
        playerControls = new PlayerControls();
    }

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

        float inputx = navigate.ReadValue<Vector2>().x;

        if (inputx != 0f && lastX == 0f)
        {
            selected += inputx > 0f ? 1 : -1;
            if (selected < 0)
            {
                selected = levelList.Count - 1;
            }
            if (selected >= levelList.Count)
            {
                selected = 0;
            }
            GameObject.Find("MusicSource").GetComponent<AudioSource>().clip = levelList[selected].levelMusic.songClip;
            GameObject.Find("MusicSource").GetComponent<AudioSource>().time = (levelList[selected].levelMusic.startOffset - 1) * (60 / levelList[selected].levelMusic.songBpm);
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
        }

        if (submit.triggered && submit.ReadValue<float>() > 0f)
        {
            GameObject.Find("LoadManager").GetComponent<LoadManager>().currentLevel = levelList[selected];
            SceneManager.LoadScene(levelList[selected].sceneName, LoadSceneMode.Single);
        }

        lastX = inputx;
    }
}
