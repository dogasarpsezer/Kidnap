using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int offset;
    public bool skill = true;
    public int hand;

     void Start()
    {
        
    }

    void Update()
    {

        //Following the cursor
         if (Camera.main.ScreenToWorldPoint(Input.mousePosition).z < 180f && !skill)
         {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
         }
         //Right hand while facing right
        else if( skill && hand == 1 && CharacterController2D.status == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90 );
        }
         //Left hand while facing right
         else if( skill && hand == -1 && CharacterController2D.status == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
         //Right hand while facing left
        else if (skill && hand == 1 && CharacterController2D.status == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
         //Left hand while facing left
        else if (skill && hand == -1 && CharacterController2D.status == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        if ( Input.GetKeyDown("q"))
        {
            skill = true;
        }

    }
}
