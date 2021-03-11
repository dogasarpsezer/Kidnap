using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public float moveSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    public CapsuleCollider2D mainCol;
    bool crouch = false;
    public BoxCollider2D crouchCol;
    public CapsuleCollider2D crouchBody;
    public bool isMonolog;


    
    void Update()
    { 
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (Input.GetKeyDown("w"))
        {
            jump = true;
        }
        if( !isMonolog)
        {
            if (Input.GetKeyDown("s"))
            {
                crouch = true;
                mainCol.isTrigger = true;
                crouchCol.isTrigger = false;
                crouchBody.isTrigger = false;
            }
            else if (Input.GetKeyUp("s"))
            {
                crouch = false;
                mainCol.isTrigger = false;
                crouchCol.isTrigger = true;
                crouchBody.isTrigger = true;
            }
        }
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }

}
