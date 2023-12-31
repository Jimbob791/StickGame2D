using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator backgroundAnim;
    [SerializeField] private Animator eyeAnim;

    void Start()
    {
        backgroundAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        eyeAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        Debug.Log(GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevelSelect()
    {
        StartCoroutine(GameObject.Find("TransitionManager").GetComponent<Transitions>().ExitScene("levelSelectScene"));
    }
}
