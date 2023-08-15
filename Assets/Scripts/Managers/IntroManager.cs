using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Animator needleAnim;
    [SerializeField] private Animator metronomeAnim;
    [SerializeField] private Animator vignetteAnim;

    public LevelMusicObject menuMusic;
    private AudioSource musicSource;

    [SerializeField] private Light2D globalLight;

    private IEnumerator Start()
    {
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        musicSource.clip = menuMusic.songClip;
        GameObject.Find("BeatManager").GetComponent<BeatManager>().NewBpm(menuMusic.songBpm);

        musicSource.Play();

        needleAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        vignetteAnim.speed = 1f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        
        yield return new WaitForSeconds(GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier * menuMusic.beatsBeforeDrop);

        vignetteAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        metronomeAnim.SetTrigger("StartIntro");
        vignetteAnim.SetTrigger("IntroTransition");
    }
}