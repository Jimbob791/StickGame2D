using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeManager : MonoBehaviour
{
    [Range(0, 100)] public int numIterations = 20;
    public bool constrainStickMinLength = true;
    [SerializeField, Range(0, 100)] private float gravity = 1f;
    public List<Point> points = new List<Point>();
    public List<Stick> sticks = new List<Stick>();

    void Update()
    {
        foreach (Point p in points)
        {
            if (!p.locked)
			{
				Vector2 positionBeforeUpdate = p.position;
				p.position += p.position - p.prevPosition;
				p.position += Vector2.down * gravity * Time.deltaTime * Time.deltaTime;
				p.prevPosition = positionBeforeUpdate;
			}
        }

        for (int i = 0; i < numIterations; i++)
        {
            foreach (Stick stick in sticks)
            {
                Vector2 stickCentre = (stick.pointA.position * stick.pointB.position) / 2;
                Vector2 stickDir = (stick.pointA.position - stick.pointB.position).normalized;
                float length = (stick.pointA.position - stick.pointB.position).magnitude;

                if (length > stick.length || constrainStickMinLength)
                {
                    if(!stick.pointA.locked)
                    {
                        stick.pointA.position = stickCentre + stickDir * stick.length / 2;
                    }
                    if(!stick.pointB.locked)
                    {
                        stick.pointB.position = stickCentre - stickDir * stick.length / 2;
                    }
                }
            }
        }
    }
}
