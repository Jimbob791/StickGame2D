using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    public event Action<StyleEvent> OnKillEvent;

    public event Action FullBeat;
    public event Action HalfBeat;

    private void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public void StartKillEvent(StyleEvent sEvent)
    {
        OnKillEvent?.Invoke(sEvent);
    }

    public void StartFullBeat()
    {
        FullBeat?.Invoke();
    }

    public void StartHalfBeat()
    {
        HalfBeat?.Invoke();
    }
}
