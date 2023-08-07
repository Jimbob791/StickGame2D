using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerManager : MonoBehaviour
{
    public float minHeight, maxHeight;
    public VisualizerObject[] visualizerObjects;
    public AudioClip audioClip;
    public float updateSpeed = 0.5f;
    public float multiplier = 5f;
    public bool loop = true;
    [Space(15), Range(64, 8192)]
    public int visualizerSamples;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        visualizerObjects = GetComponentsInChildren<VisualizerObject>();

        if (!audioClip)
            return;

        audioSource.loop = loop;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrumData = new float[visualizerSamples];

        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<Transform>().localScale;
            newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * multiplier), updateSpeed), minHeight, maxHeight);
            visualizerObjects[i].GetComponent<Transform>().localScale = newSize;
        }
    }
}
