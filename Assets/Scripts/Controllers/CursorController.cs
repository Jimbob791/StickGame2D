using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Vector3 worldPosition;

    public GameObject cursorSprite;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;

        Cursor.visible = false;

        cursorSprite.transform.position = worldPosition;

        cursorSprite.GetComponent<Animator>().speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
    }
}
