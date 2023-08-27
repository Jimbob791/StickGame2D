using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StyleManager : MonoBehaviour
{
    public StyleLevel currentLevel;
    public float totalStyle;

    [SerializeField] private Slider scoreMeter = null;
    [SerializeField] private Image meterBacking = null;
    [SerializeField] private Image meterFill = null;
    [SerializeField] private Image rightBacking = null;
    [SerializeField] private Image barEnd1 = null;
    [SerializeField] private Image barEnd2 = null;

    [SerializeField] private GameObject styleEventObject = null;
    [SerializeField] private Animator barLeftAnim = null;
    [SerializeField] private Animator barBottomAnim = null;

    [SerializeField] private StyleEvent onBeatEvent = null;
    [SerializeField] private StyleEvent onHalfBeatEvent = null;

    [SerializeField] private Image styleLogo = null;
    [SerializeField] private List<StyleLevel> levels = new List<StyleLevel>();


    private Vector3 eventOffset = new Vector3(618, 353, 0);
    private int numEventsActive = 0;
    private List<GameObject> styleObjects = new List<GameObject>();

    private float style = 150f;
    private int levelNum = 0;

    void Start()
    {
        EventManager.current.OnKillEvent += StyleEventTrigger;
        EventManager.current.FullBeat += FullBeatEvent;
        EventManager.current.HalfBeat += HalfBeatEvent;
        totalStyle = 0f;
    }

    void OnDisable()
    {
        EventManager.current.OnKillEvent -= StyleEventTrigger;
        EventManager.current.FullBeat -= FullBeatEvent;
        EventManager.current.HalfBeat -= HalfBeatEvent;
    }

    void Update()
    {
        // Lower Style Value
        style -= currentLevel.multiplier * 15 * Time.deltaTime;
        style = style < 0 ? 0 : style;

        // Update CurrentLevel
        if (style > currentLevel.maxValue)
        {
            if (levelNum == levels.Count - 1)
            {
                style = currentLevel.maxValue;
            }
            else
            {
                levelNum += 1;
                currentLevel = levels[levelNum];
            }
        }
        else if (style < currentLevel.minValue)
        {
            levelNum -= 1;
            currentLevel = levels[levelNum];
        }

        // Set Image and Slider
        scoreMeter.maxValue = currentLevel.maxValue - currentLevel.minValue;
        scoreMeter.value = style - currentLevel.minValue;
        styleLogo.sprite = currentLevel.icon;

        meterBacking.color = currentLevel.darkCol;
        rightBacking.color = currentLevel.darkCol;
        barEnd2.color = currentLevel.darkCol;

        meterFill.color = currentLevel.lightCol;
        barEnd1.color = currentLevel.lightCol;

        // Set Animator Speeds
        barBottomAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        barLeftAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
    }

    private void StyleEventTrigger(StyleEvent sEvent)
    {
        style += sEvent.points;
        totalStyle += sEvent.points;
        Debug.Log(sEvent);

        GameObject newObj = GameObject.Instantiate(styleEventObject, eventOffset + new Vector3(0, -50 * (numEventsActive), 0), Quaternion.identity);
        numEventsActive += 1;
        styleObjects.Add(newObj);

        newObj.GetComponent<StyleEventController>().slot = numEventsActive;
        newObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sEvent.description;
        newObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = sEvent.textColour;

        newObj.transform.SetParent(this.gameObject.transform, false);

        StartCoroutine(DestroyEvent(newObj));
    }

    IEnumerator DestroyEvent(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        numEventsActive -= 1;
        styleObjects.Remove(obj);
        foreach (GameObject g in styleObjects)
        {
            if (g.GetComponent<StyleEventController>().slot > obj.GetComponent<StyleEventController>().slot)
                g.GetComponent<StyleEventController>().slot -= 1; 
        }
        if (numEventsActive < 0)
            numEventsActive = 0;
        Destroy(obj);
    }

    private void FullBeatEvent()
    {
        StyleEventTrigger(onBeatEvent);
    }

    private void HalfBeatEvent()
    {
        StyleEventTrigger(onHalfBeatEvent);
    }
}
