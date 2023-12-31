using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm;
    public float multiplier;

    private float timeSinceLastBeat;
    private float beatDuration;

    void Awake()
    {
        NewBpm(bpm);
        EventManager.current.InputAction += PlayerInput;
        timeSinceLastBeat = 0f;
    }

    void OnDisable()
    {
        EventManager.current.InputAction -= PlayerInput;
    }

    void Update()
    {
        timeSinceLastBeat += Time.deltaTime;

        if (timeSinceLastBeat >= beatDuration) 
        {
            timeSinceLastBeat -= beatDuration;
        }
    }

    public void NewBpm(float _bpm)
    {
        multiplier = _bpm / 60;
        bpm = _bpm;
        beatDuration = 1 / multiplier;
    }

    private void PlayerInput()
    {
        if (timeSinceLastBeat < beatDuration / 5 || timeSinceLastBeat > beatDuration - (beatDuration / 5))
        {
            EventManager.current.StartFullBeat();
        }
    }
}
