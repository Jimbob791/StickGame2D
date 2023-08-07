using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator backgroundAnim;
    [SerializeField] private Animator eyeAnim;

    void Start()
    {
        backgroundAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        eyeAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
    }

    void Update()
    {
        
    }
}
