using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public static bool youDed = false;
    public GameObject bloodImage;
    public AudioSource audioSource;
    public AudioClip foundYou;
    public AudioClip gunShot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("dedsa");
            youDed = true;
            bloodImage.SetActive(true);
            audioSource.PlayOneShot(foundYou);
            audioSource.PlayOneShot(gunShot);
        }
    }
}
