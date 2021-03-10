using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAndDoorMechanics : MonoBehaviour
{
    #region Game Objects & Variables
    [Header("Game Objects of Keys (for door object)")]
    public GameObject yellowKey;
    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;
    public GameObject purpleKey;
    public GameObject orangeKey;
    [Header("The keys that open this door (for door object)")]
    public bool yellow;
    public bool red;
    public bool blue;
    public bool green;
    public bool purple;
    public bool orange;
    public int numberOfKeysToOpen;
    [Header("Open Door (for door object)")]
    public GameObject openDoor;
    public GameObject closedDoor;
    int counter = 0;
    public AudioSource audioSource;
    public AudioSource forDoor;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( gameObject.CompareTag("Key") && collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            gameObject.SetActive(false);
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if( gameObject.CompareTag("Door") && collision.gameObject.CompareTag("Player"))
        {
            #region Long Conditionals To Determine The Right Keys
            if ( yellow && !yellowKey.activeInHierarchy)
            {
                counter++;
            }
            if (blue && !blueKey)
            {
                counter++;
            }
            if (green && !greenKey)
            {
                counter++;
            }
            if (purple && !purpleKey)
            {
                counter++;
            }
            if (orange && !orangeKey)
            {
                counter++;
            }
            if (red && !redKey)
            {
                counter++;
            }
            #endregion
            if( counter == numberOfKeysToOpen)
            {
                forDoor.Play();
                closedDoor.SetActive(false);
                openDoor.SetActive(true);
            }
        }
    }
}
