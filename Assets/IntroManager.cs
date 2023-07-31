using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Animator metronomeAnim;
    [SerializeField] private VolumeProfile mainVolume;
    [SerializeFiled] private Light2D globalLight;

    private IEnumerator Start()
    {
        metronomeAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        for (int i = 0; i < 120; i++)
        {
            globalLight.color = 
        }
        globalLight.color
        mainVolume.
        
    }
}
