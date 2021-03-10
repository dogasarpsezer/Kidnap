using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    public VideStoryBoard changeFunction;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fix());
        }
    }
    IEnumerator Fix() 
    {
        while (changeFunction.coolDown) 
        {
            yield return new WaitForSeconds(0.2f);
        }
        changeFunction.ChangeButton();
        Destroy(gameObject);
    }
}
