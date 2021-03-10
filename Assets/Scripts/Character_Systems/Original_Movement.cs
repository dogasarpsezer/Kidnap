using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Original_Movement : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb2d;
    public float moveSpeed;
    float horizontalMove;
    Vector2 mousePosition;
    bool right = true;
    #endregion
    private void Start()
    {
        //Getting the rigidbody 
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    public float walkMulti;
    public Animator animator;
    public bool isWalking;
    void Update()
    {
        //in monolog all the other updates are disabled 
        //Getting the mouse poisition for the flip
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        #region Flip Depending On Mouse Position
        if (mousePosition.x >= gameObject.transform.position.x)
        {
            right = true;
            Flip(right);
        }
        else
        {
            right = false;
            Flip(right);
        }
        #endregion

        #region runningCheck

        float a = 1;
        if (Input.GetKey(KeyCode.LeftShift) && isWalking == false)
        {
            a = 2;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        #endregion

        #region Walking Forward and Backward
        float temp = Input.GetAxisRaw("Horizontal");
        if (right)
        {
            if(temp > 0.05f)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * a * walkMulti;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsBackwards", false);
            }
            else if(temp < -0.05f)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed / 1.5f * a * walkMulti;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsBackwards", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            }
        }
        else
        {
            if (temp < -0.05f)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * a * walkMulti;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsBackwards", false);
            }
            else if (temp > 0.05f)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed / 1.5f * a * walkMulti;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsBackwards", true);
            }
            else
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
                animator.SetBool("IsMoving", false);
            }
            
        }
        #endregion


    }


    private void FixedUpdate()
    {
        //Movement
        rb2d.velocity = new Vector2(horizontalMove, 0);
    }

    #region Flip Function
    //Taking a bool from update to change transform.localScale for x
    void Flip(bool faceRight)
    {
        if( faceRight)
        {
            gameObject.transform.localScale = new Vector3(1f,1f,1f);
        }
        else if( !faceRight )
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);

        }
    }
    #endregion
}
