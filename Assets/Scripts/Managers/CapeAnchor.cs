using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeAnchor : MonoBehaviour
{
    public Vector2 partOffset = Vector2.zero;
    public float lerpSpeed = 20f;

    private Transform[] capeParts;
    private Transform capeAnchor;

    private void Awake() 
    {
        capeAnchor = GetComponent<Transform>();
        capeParts = GetComponentsInChildren<Transform>();
    }

    private void Update() 
    {
        Transform pieceToFollow = capeAnchor;

        foreach(Transform capePart in capeParts)
        {
            // make sure we're not including the cape anchor, only the hair parts
            if (!capePart.Equals(capeAnchor))
            {
                Vector2 targetPosition = (Vector2) pieceToFollow.position + partOffset;
                Vector2 newPositionLerped = Vector2.Lerp(capePart.position, targetPosition, Time.deltaTime * lerpSpeed);
               
                capePart.position = newPositionLerped;
                pieceToFollow = capePart;
            }
        }
    }

}