using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Animator metronomeAnim;
    [SerializeField] private Animator vignetteAnim;
    [SerializeField] private UnityEngine.Rendering.VolumeProfile introProfile;
    [SerializeField] private Light2D globalLight;

    private IEnumerator Start()
    {
        metronomeAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        vignetteAnim.speed = 1f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        
        yield return null;
    }
}
