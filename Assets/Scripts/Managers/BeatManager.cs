using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm;
    public float multiplier;

    private float timeSinceLastBeat;

    void Start()
    {
        NewBpm(bpm);
        EventManager.current.InputAction += PlayerInput;
        timeSinceLastBeat = 0f;
    }

    void Update()
    {
        timeSinceLastBeat += Time.deltaTime;

        if (timeSinceLastBeat >= 1 / multiplier) 
        {
            timeSinceLastBeat = 0;
            Debug.Log("beat");
        }
    }

    public void NewBpm(float _bpm)
    {
        multiplier = bpm / 60;
        bpm = _bpm;
    }

    private void PlayerInput()
    {
        if (timeSinceLastBeat < 0.1f || timeSinceLastBeat > (1 / multiplier) - 0.1f)
        {
            EventManager.current.StartFullBeat();
        }
        if (timeSinceLastBeat > ((1 / multiplier) / 2) - 0.1f || timeSinceLastBeat < ((1 / multiplier) / 2) + 0.1f)
        {
            EventManager.current.StartHalfBeat();
        }
    }
}
