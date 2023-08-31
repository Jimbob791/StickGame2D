using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public IEnumerator ExitScene(string sceneToLoad)
    {
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 2f, 0f));
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().SetBool("idle", false);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        gameObject.GetComponent<Animator>().SetBool("idle", true);
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 2f, 0.07f));
    }
}
