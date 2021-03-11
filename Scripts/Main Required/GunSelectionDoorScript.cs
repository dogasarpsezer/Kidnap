using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectionDoorScript : MonoBehaviour
{
    public GameObject doorLeft, doorRight;
    public Rigidbody2D rb2dLeft, rb2dRight;
    Vector2 leftLimit, rightLimit;
    bool isOpening = false;
    public float speedMultiplier;
    private void Start()
    {
        leftLimit = doorLeft.transform.position;
        rightLimit = doorRight.transform.position;
    }

    private void FixedUpdate()
    {
        if( isOpening)
        {
            rb2dLeft.velocity = new Vector2(-1 * speedMultiplier, 0f);
            rb2dRight.velocity = new Vector2(1 * speedMultiplier, 0f);

            if( doorRight.transform.position.x - rightLimit.x >= 1.1f)
            {
                rb2dLeft.velocity = new Vector2(0f, 0f);
                rb2dRight.velocity = new Vector2(0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.CompareTag("Player"))
        {
            isOpening = true;
        }
    }
}
