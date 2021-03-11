using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    #region Varibales
    public Rigidbody2D myrb2d;
    public GameObject flashlight;
    public float moveSpeed;
    float horizontalMove;
    float verticalMove;

    #endregion

    private void Start()
    {

    }

    void Update()
    {
        //Getting the axis input
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        #region FlashLight Activation
        if ( Input.GetMouseButton(0))
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }
        #endregion
        
    }
    private void FixedUpdate()
    {
        if( !PlayerCollider.youDed)
        {
            #region Movement & Rotation
            if (verticalMove == 0f && horizontalMove != 0)
            {
                myrb2d.velocity = new Vector2(moveSpeed * horizontalMove, 0);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -90f * horizontalMove);
            }
            else if (horizontalMove == 0f && verticalMove != 0)
            {
                myrb2d.velocity = new Vector2(0, moveSpeed * verticalMove);
                if (verticalMove > 0)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (verticalMove < 0)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 180f);
                }

            }
            else
            {
                myrb2d.velocity = new Vector2(0, 0);
            }
            #endregion
        }
        else
        {
            myrb2d.velocity = new Vector2(0f, 0f);
        }

    }
}
