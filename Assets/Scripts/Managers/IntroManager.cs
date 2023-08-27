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

    public LevelMusicObject[] menuMusicList;
    private AudioSource musicSource;

    [SerializeField] private Light2D globalLight;

    private IEnumerator Start()
    {
        LevelMusicObject menuMusic = menuMusicList[Random.Range(0, menuMusicList.Length)];

        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        musicSource.clip = menuMusic.songClip;
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 3f, 0.07f));

        GameObject.Find("BeatManager").GetComponent<BeatManager>().NewBpm(menuMusic.songBpm);

        musicSource.time = menuMusic.startOffset * (60 / menuMusic.songBpm);
        musicSource.Play();

        needleAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        vignetteAnim.speed = 1f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        
        yield return new WaitForSeconds((menuMusic.beatsBeforeDrop * (60 / menuMusic.songBpm)) - (menuMusic.startOffset * (60 / menuMusic.songBpm)));

        vignetteAnim.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
        metronomeAnim.SetTrigger("StartIntro");
        vignetteAnim.SetTrigger("IntroTransition");
    }
}
