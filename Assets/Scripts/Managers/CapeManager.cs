using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeManager : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private GameObject hook = null;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        hook.transform.position = player.position + offset;
    }
}
