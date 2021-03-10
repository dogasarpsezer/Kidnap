using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public static int direction = 1;
    int realDirection;

    void Start()
    {
        realDirection = direction;
        StartCoroutine(lifeSpan());

        lifeSpan();
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * realDirection );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
           if( !collision.gameObject.CompareTag("Player") )
                Destroy(gameObject);
 
    }

    IEnumerator lifeSpan() {

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
} 
