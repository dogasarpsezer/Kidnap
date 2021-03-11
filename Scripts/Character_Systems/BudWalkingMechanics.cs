using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudWalkingMechanics : MonoBehaviour
{
    #region Variables
    public Rigidbody2D myrb2d;
    public GameObject player;
    public float walkMultiplier;
    bool grounded = false;
    public Animator animator;
    public GameObject closedDoor;
    public GameObject openDoor;
    bool isStopped = false;
    bool isDoorOpen = false;
    #endregion

    private void FixedUpdate()
    {
        if (grounded && !isStopped)
        {
            if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) <= 4)
            {
                myrb2d.velocity = new Vector2(walkMultiplier * 1, 0f);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                animator.SetBool("isWalking", true);
            }
            else if (gameObject.transform.position.x - player.transform.position.x > 4)
            {
                myrb2d.velocity = new Vector2(0f, 0f);
                animator.SetBool("isWalking", false);
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Door"))
        {
            if (!isDoorOpen)
            {
                closedDoor.SetActive(false);
                openDoor.SetActive(true);
                isDoorOpen = true;
            }
            else
            {
                closedDoor.SetActive(true);
                openDoor.SetActive(false);
                isDoorOpen = false;
            }
       
        }
        else if (collision.gameObject.CompareTag("StopTrigger"))
        {
            isStopped = true;
        }

    }
}
