using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightTrigger : MonoBehaviour
{
    bool isDone = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDone)
        {
            StartCoroutine("greenSwitch");
            isDone = true;
        }
    }
    private void Start()
    {
        redLight.SetActive(true);
        yellowLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
        collider.SetActive(true);
    }

    public GameObject redLight,yellowLight,greenLight,collider;
    IEnumerator greenSwitch() 
    {
        yield return new WaitForSeconds(2f);
        redLight.SetActive(false);
        yellowLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yellowLight.SetActive(false);
        greenLight.SetActive(true);
        collider.SetActive(false);
    }
}
