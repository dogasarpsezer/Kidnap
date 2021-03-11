using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    Vector2 mousePosition;
    void Start()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }


    void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.normalized.x, Input.mousePosition.normalized.y);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
