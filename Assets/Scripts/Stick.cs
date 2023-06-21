using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Point pointA, pointB;
    public float length;

    void Start()
    {
        length = Vector2.Distance(pointA.position, pointB.position);
    }
}
