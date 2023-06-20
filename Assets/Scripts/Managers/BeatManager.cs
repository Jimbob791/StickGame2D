using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm;
    public float multiplier;

    public void NewBpm(float _bpm)
    {
        multiplier = bpm / 60;
        bpm = _bpm;
    }
}
