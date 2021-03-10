using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GuardMechanics : MonoBehaviour
{
    #region Variables
    [Header("Max & Min Values for Guard Movement")]
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    [Header("Guard Variables")]
    public Rigidbody2D myrb2d;
    Vector2 guardBoundariesMax;
    Vector2 guardBoundariesMin;
    public float moveSpeed;
    public AudioClip huhClip;
    public AudioClip uhhClip;
    public AudioSource audioSource;
    public Transform target;
    public Vector3 a;
    public GameObject[] array;
    int area = 3;
    float dummyRotationX;
    public bool isStoped = false;
    float time = 0;
    public float timeDelay = 3.5f;
    Transform dummy;
    public float[] angleX;
    bool isBug = false;
    public int accuracy;
    #endregion

    private void Start()
    {
       #region Initialization
        guardBoundariesMax = new Vector2(maxX,maxY);
        guardBoundariesMin = new Vector2(minX,minY);
        myrb2d.velocity = new Vector2(0,moveSpeed);
        #endregion
    }

    private void FixedUpdate()
    {
        
       if (isStoped)
        {
            #region Turn Back & Move Mechanics
            time = time + 1f * Time.deltaTime;
            if (time >= timeDelay)
            {
                isStoped = false;
                gameObject.transform.rotation = Quaternion.Euler(angleX[area],90f,90f);
                time = 0;
                if( area == 0)
                {
                    myrb2d.velocity = new Vector2(0,moveSpeed);

                }
                else if( area == 1)
                {
                    myrb2d.velocity = new Vector2(0,-moveSpeed);

                }
                else if( area == 2)
                {
                    myrb2d.velocity = new Vector2(moveSpeed,0);
 
                }
                else if( area == 3)
                {
                    myrb2d.velocity = new Vector2(-moveSpeed, 0);
                }
            }
            #endregion

        }
        else
        {
            #region Guard Movement & Rotation
            if (gameObject.transform.position.x >= guardBoundariesMax.x && gameObject.transform.position.y <= guardBoundariesMin.y)
            {
                gameObject.transform.rotation = Quaternion.Euler(-90f, 90f, 90f);
                myrb2d.velocity = new Vector2(0, moveSpeed);
                
            }
            else if (gameObject.transform.position.x <= guardBoundariesMin.x && gameObject.transform.position.y >= guardBoundariesMax.y)
            {
                gameObject.transform.rotation = Quaternion.Euler(90f, 90f, 90f);
                myrb2d.velocity = new Vector2(0, -moveSpeed);
                

            }
            else if (gameObject.transform.position.y >= guardBoundariesMax.y && gameObject.transform.position.x >= guardBoundariesMax.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(-180f, 90f, 90f);
                myrb2d.velocity = new Vector2(-moveSpeed, 0);
                

            }
            else if (gameObject.transform.position.y <= guardBoundariesMin.y && gameObject.transform.position.x <= guardBoundariesMin.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 90f);
                myrb2d.velocity = new Vector2(moveSpeed, 0);
               

            }
            #endregion
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Flashlight Detection & Rotation Angle Index Finder
        if (collision.gameObject.CompareTag("FlashlightPlayer"))
        {
            myrb2d.velocity = new Vector2(0, 0);
            audioSource.PlayOneShot(huhClip);
            if (gameObject.transform.rotation.Compare(Quaternion.Euler(-270f,180f,180f),accuracy))
            {
                area = 1;
                Debug.Log(area);
            }
            else if (gameObject.transform.rotation.Compare(Quaternion.Euler(-90f, 90f, 90f), accuracy))
            {
                area = 0;
                Debug.Log(area);
            }
            else if (gameObject.transform.rotation.Compare(Quaternion.Euler(0f, 89.99999f, 89.99999f), accuracy))
            {
                area = 2;
                Debug.Log(area);
            }
            else if (gameObject.transform.rotation.Compare(Quaternion.Euler(-180f, 180f, 180f), accuracy))
            {
                area = 3;
                Debug.Log(area);
            }
            else
            {
                Debug.Log("sa");
            }
            
            gameObject.transform.LookAt(target, a);
            isStoped = true;
        }
        #endregion

    }

}