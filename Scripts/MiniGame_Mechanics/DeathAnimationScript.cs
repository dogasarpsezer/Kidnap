using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathAnimationScript : MonoBehaviour
{
    float time = 0;
    float timeToFadeIn = 2.2f;
    public Image blood;
    public GameObject player;
    public AudioClip deathSound;
    public AudioClip gunShot;
    public AudioSource auidoSource;

    private void Start()
    {
        auidoSource.PlayOneShot(gunShot);
        auidoSource.PlayDelayed(0.25f);
        StartCoroutine(FadeTime(PlayerCollider.youDed));
        FadeTime(PlayerCollider.youDed);
    }
    IEnumerator FadeTime(bool isDead)
    {
        if (isDead)
        {
            for( time = 0; time < timeToFadeIn;time += 0.1f)
            {
                blood.color = new Color(1f, 1f, 1f, time / 2.2f);
                yield return new WaitForSeconds(0.1f);
            }
            player.SetActive(false);
        }
        
    }
}
